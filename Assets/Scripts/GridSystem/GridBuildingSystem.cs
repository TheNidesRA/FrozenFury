using System;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace GridSystem
{
    public class GridBuildingSystem : MonoBehaviour
    {
        //Its like a Singelton instance so other objects can use his functions
        public static GridBuildingSystem Instance { get; private set; }

        //public GameObject player;

        private InputPlayer _control;

        [SerializeField] private bool _destroyOnPlace = false;

        /// <summary>
        /// called when _buildingSO is changed
        /// </summary>
        public event EventHandler OnSelectedChanged; //Callback 

        public event EventHandler OnObjectPlaced;
        public event EventHandler OnObjectSetPosition;
        public event EventHandler OnObjectRemovePosition;
        public event EventHandler OnMissSetPosition;
        public event EventHandler OnClickOutOfObject;
        public event EventHandler<PlacedBuild> OnBuildSelected;


        /// <summary>
        /// current Build
        /// </summary>
        private BuildingSO _buildingSO;

        /// <summary>
        /// current Build
        /// </summary>
        public BuildingSO buildingSo => _buildingSO;

        private Grid<GridObject> _grid; //Scene grid
        private BuildingSO.Dir _dir = BuildingSO.Dir.Down;
        [SerializeField] private List<BuildingSO> _buildingsList;

        public Vector3 startPoint;
        public bool buildMenu = false;
        public bool enableBuildMove = true;

        private PlacedBuild _currentPlaceBuild;
        private Vector2Int _ActualBuildPosition;


        private bool _enableBuild = true;


        public int gridWidth = 15;
        public int gridHeight = 9;
        public float cellSize = 10f;


        private void OnEnable()
        {
            _control.Enable();
        }

        private void OnDisable()
        {
            _control.Disable();
        }


        private void Awake()
        {
            Instance = this;
            _control = new InputPlayer();

            _grid = new Grid<GridObject>(gridWidth, gridHeight, cellSize,
                (Grid<GridObject> global, int x, int z) => new GridObject(global, x, z), startPoint);

            _buildingSO = null;
            _control.Building.LeftClick.canceled += PlaceBuilding;
            _control.Building.Build1.performed += ChangeBuild;
            _control.Building.Build2.performed += ChangeBuild;
            _control.Building.Build3.performed += ChangeBuild;
            _control.Building.RigthClick.performed += RemoveBuild;
            _control.Building.UndoSelection.performed += DeselectObjectType;
            _control.Building.Rotate.performed += Rotate;
            _control.Building.Confirm.performed += Confirm;
            _control.Building.SelectBuid.performed += SearchBuilding;
        }


        /// <summary>
        /// Object placed in grid
        /// </summary>
        public class GridObject
        {
            private Grid<GridObject> _grid;
            private int x;
            private int z;
            private PlacedBuild _placedBuild = null; //Build in cell

            public GridObject(Grid<GridObject> grid, int x, int z)
            {
                _grid = grid;
                this.x = x;
                this.z = z;
            }

            public void SetPlacedBuild(PlacedBuild placedBuild)
            {
                _placedBuild = placedBuild;
                _grid.TriggerGridObjectChanged(x, z);
            }

            public void ClearPlacedBuild()
            {
                _placedBuild = null;
                _grid.TriggerGridObjectChanged(x, z);
            }

            public PlacedBuild GetPlaceBuild()
            {
                return _placedBuild;
            }

            public bool CanBuild()
            {
                return _placedBuild == null;
            }

            public override string ToString()
            {
                return x + " , " + z + "\n" + _placedBuild;
            }
        }

        private void Start()
        {
            WaveController._instance.OnRoundActive += Instance_OnRoundActive;
        }

        private void RemoveBuild(InputAction.CallbackContext callbackContext)
        {
            GridObject gridObject = GetMouseGridObject();
            if (gridObject == null) return;
            PlacedBuild placedBuild = gridObject.GetPlaceBuild();

            if (placedBuild != null)
            {
                placedBuild.DestroySelf();

                List<Vector2Int> buildingPositions = placedBuild.GetGridPositionList();

                foreach (var buildPosition in buildingPositions)
                {
                    _grid.GetObjectValue(buildPosition.x, buildPosition.y).ClearPlacedBuild();
                }

                BuildStats stats = new BuildStats(placedBuild.level, placedBuild.BuildingSo.type);
                WorldController.Instance.RemoveStruct(stats);
            }
        }


        private void Instance_OnRoundActive(object sender, bool value)
        {
            if (value)
            {
             
                _buildingSO = null;
                buildMenu = false;
                enableBuildMove = false;
                _enableBuild = false;
                RefreshSelectedObjectType();
            }
            else
            {
                buildMenu = false;
                enableBuildMove = true;
                _enableBuild = true;
                RefreshSelectedObjectType();
            }
        }


        public void RemoveBuild(PlacedBuild build)
        {
//            Debug.Log("asd");

            if (build != null)
            {
                build.DestroySelf();

                List<Vector2Int> buildingPositions = build.GetGridPositionList();

                foreach (var buildPosition in buildingPositions)
                {
                    _grid.GetObjectValue(buildPosition.x, buildPosition.y).ClearPlacedBuild();
                }

                BuildStats stats = new BuildStats(build.level, build.BuildingSo.type);
                WorldController.Instance.RemoveStruct(stats);
            }
        }


        private GridObject GetMouseGridObject()
        {
            return _grid.GetObjectValue(GetMousePosition());
        }

        private void ChangeBuild(InputAction.CallbackContext callbackContext)
        {
            //Debug.Log("Bot: "+callbackContext.control.displayName);

            if (!_enableBuild) return;

            int i = int.Parse(callbackContext.control.displayName);
            i = i - 1;
            BuildingSO targetBuild = _buildingsList[i];
            if (PlayerStats._instance.gold >= targetBuild.goldCost)
            {
                _buildingSO = targetBuild;
                // Debug.Log(_buildingSO.ToString());
                RefreshSelectedObjectType();
            }
        }

        public void changeBuild(int build)
        {
            if (!_enableBuild) return;
            
            BuildingSO targetBuild = _buildingsList[build];
            if (PlayerStats._instance.gold >= targetBuild.goldCost)
            {
                _buildingSO = targetBuild;
                // Debug.Log(_buildingSO.ToString());
                RefreshSelectedObjectType();
            }
        }


        public void removeSelectedBuild()
        {
            _buildingSO = null;
            buildMenu = false;
            enableBuildMove = true;
            RefreshSelectedObjectType();
        }

        private void Rotate(InputAction.CallbackContext callbackContext)
        {
            //Debug.Log(_dir);
            _dir = BuildingSO.GetNextDir(_dir);
        }

        public void Rotate()
        {
            //Debug.Log(_dir);
            _dir = BuildingSO.GetNextDir(_dir);
        }


        private void SetMousePosition(Vector2Int mouseGridPosition)
        {
            _ActualBuildPosition = mouseGridPosition;
            buildMenu = true;
            enableBuildMove = false;

            //ComunicacionGridCanvas._instance.SetBuildPosition();
            OnObjectSetPosition?.Invoke(this, EventArgs.Empty);
        }

        private void SearchBuilding(InputAction.CallbackContext callbackContext)
        {
            // Debug.Log("Buscando y talll");
            if (_buildingSO != null && !buildMenu)
            {
            }
            else
            {
                GridObject gridObject = GetMouseGridObject();

                if (gridObject != null)
                {
                    PlacedBuild placedBuild = gridObject.GetPlaceBuild();
                    if (placedBuild != null)
                    {
                        // Debug.Log("Cogiendo el aux");
                        OnBuildSelected?.Invoke(this, placedBuild);
                    }
                    else
                    {
                        //OnClickOutOfObject?.Invoke(this,EventArgs.Empty);
                    }
                }
            }
        }


        private void PlaceBuilding(InputAction.CallbackContext callbackContext)
        {
            if (_buildingSO != null && !buildMenu)
            {
                Vector2Int mouseGridPosition = GetMouseGridPosition();

                if (mouseGridPosition == new Vector2Int(-1, -1))
                    return;

//                Debug.Log(mouseGridPosition);

                List<Vector2Int> buildingPositions = _buildingSO.GetGridPositionList(mouseGridPosition, _dir);

                if (CanBuild(buildingPositions))
                {
                    //ComunicacionGridCanvas._instance.StartBuilding();
                    SetMousePosition(mouseGridPosition);
                }
                else
                {
                    OnMissSetPosition?.Invoke(this, EventArgs.Empty);
                    // Debug.Log("No se puede !!! :)");
                }
            }
        }


        private void Confirm(InputAction.CallbackContext callbackContext)
        {
            if (_buildingSO == null) return;
            List<Vector2Int> buildingPositions = _buildingSO.GetGridPositionList(_ActualBuildPosition, _dir);

            if (CanBuild(buildingPositions))
            {
                buildMenu = false;
                enableBuildMove = true;

                if (_buildingSO.type == BuildingSO.BuildingType.Wall)
                {
                    SearchWallNeighbour(buildingPositions);
                }

                Build(_ActualBuildPosition, buildingPositions);
                PlayerStats._instance.gold -= _buildingSO.goldCost;
                if (_destroyOnPlace)
                {
                    _buildingSO = null;
                    RefreshSelectedObjectType();
                }
            }
            else
            {
                OnMissSetPosition?.Invoke(this, EventArgs.Empty);
                // Debug.Log("No se puede !!! :)");
            }
        }

        public void Confirm()
        {
            List<Vector2Int> buildingPositions = _buildingSO.GetGridPositionList(_ActualBuildPosition, _dir);

            if (CanBuild(buildingPositions))
            {
                Build(_ActualBuildPosition, buildingPositions);
                PlayerStats._instance.gold -= _buildingSO.goldCost;


                if (_buildingSO.type == BuildingSO.BuildingType.Wall)
                {
                    SearchWallNeighbour(buildingPositions);
                }

                RemoveFixedMouse();


                if (_destroyOnPlace)
                {
                    _buildingSO = null;
                    RefreshSelectedObjectType();
                }
                //  ComunicacionGridCanvas._instance.FinishBuilding();
            }
            else
            {
                OnMissSetPosition?.Invoke(this, EventArgs.Empty);
                // Debug.Log("No se puede !!! :)");
            }
        }

        private void RemoveFixedMouse()
        {
            _ActualBuildPosition = new Vector2Int();
            buildMenu = false;
            enableBuildMove = true;
            //ComunicacionGridCanvas._instance.EnableBuildMoving();
            OnObjectRemovePosition?.Invoke(this, EventArgs.Empty);
        }


        public void Mover()
        {
            buildMenu = false;
            enableBuildMove = true;
            // ComunicacionGridCanvas._instance.EnableBuildMoving();
            OnObjectRemovePosition?.Invoke(this, EventArgs.Empty);
        }


        private void Build(Vector2Int buildPosition, List<Vector2Int> buildingPositions)
        {
            Vector2Int rotationOffset = _buildingSO.GetRotationOffset(_dir);
            Vector3 placedBuildWorldPosition = _grid.GetWorldPosition(buildPosition) +
                                               new Vector3(rotationOffset.x, 0, rotationOffset.y) *
                                               _grid.cellSize;

            PlacedBuild placedBuild =
                PlacedBuild.Create(placedBuildWorldPosition, buildPosition, _dir, _buildingSO);
            // Debug.Log("Se ha construido un edificio tipo: "  + placedBuild.BuildingSo.type);

            foreach (var buildingPosition in buildingPositions)
            {
                _grid.GetObjectValue(buildingPosition.x, buildingPosition.y).SetPlacedBuild(placedBuild);
            }

            // placedBuild.BuildingSo.type.
            BuildStats stats = new BuildStats(placedBuild.level, placedBuild.BuildingSo.type);

            WorldController.Instance.AddStruct(stats);
            //callback
            OnObjectPlaced?.Invoke(this, EventArgs.Empty);
        }


        private void SearchWallNeighbour(List<Vector2Int> buildingPositions)
        {
            // Debug.Log(_dir);
            foreach (var buildPosition in buildingPositions)
            {
                if (_dir == BuildingSO.Dir.Down || _dir == BuildingSO.Dir.Up)
                {
                    GridObject g1 = _grid.GetObjectValue(buildPosition.x, buildPosition.y + 1);
                    GridObject g2 = _grid.GetObjectValue(buildPosition.x, buildPosition.y - 1);


                    // Debug.Log(g1);
                    // Debug.Log(g2);

                    if (g1?.GetPlaceBuild() != null)
                    {
                        if (g1.GetPlaceBuild().BuildingSo.type == BuildingSO.BuildingType.Wall)
                        {
                            // Debug.Log("EHHHH MURITOO A LA DERECHA");
                        }
                    }

                    if (g2?.GetPlaceBuild() != null)
                    {
                        if (g2.GetPlaceBuild().BuildingSo.type == BuildingSO.BuildingType.Wall)
                        {
                            // Debug.Log("EHHHH MURITOO A LA IZQUIERDA");
                        }
                    }


                    if (buildPosition == _ActualBuildPosition)
                    {
                        GridObject g3 = _grid.GetObjectValue(buildPosition.x - 1, buildPosition.y);

                        if (g3?.GetPlaceBuild() != null)
                        {
                            if (g3.GetPlaceBuild().BuildingSo.type == BuildingSO.BuildingType.Wall &&
                                (g3.GetPlaceBuild().dir != BuildingSO.Dir.Down &&
                                 g3.GetPlaceBuild().dir != BuildingSO.Dir.Up))
                            {
                                // Debug.Log("EHHHH MURITOO Esquina construccion");
                            }
                        }
                    }
                    else
                    {
                        GridObject g3 = _grid.GetObjectValue(buildPosition.x + 1, buildPosition.y);
                        //    Debug.Log(_ActualBuildPosition +" || " + buildPosition);
                        if (g3?.GetPlaceBuild() != null)
                        {
                            if (g3.GetPlaceBuild().BuildingSo.type == BuildingSO.BuildingType.Wall &&
                                (g3.GetPlaceBuild().dir != BuildingSO.Dir.Down &&
                                 g3.GetPlaceBuild().dir != BuildingSO.Dir.Up))
                            {
                                //    Debug.Log("EHHHH MURITOO Esquina NOOOO construccion");
                            }
                        }
                    }
                }
                else
                {
                    GridObject g1 = _grid.GetObjectValue(buildPosition.x + 1, buildPosition.y);
                    GridObject g2 = _grid.GetObjectValue(buildPosition.x - 1, buildPosition.y);


                    if (g1?.GetPlaceBuild() != null)
                    {
                        if (g1.GetPlaceBuild().BuildingSo.type == BuildingSO.BuildingType.Wall)
                        {
                            // Debug.Log("EHHHH MURITOO A LA DERECHA");
                        }
                    }

                    if (g2?.GetPlaceBuild() != null)
                    {
                        if (g2.GetPlaceBuild().BuildingSo.type == BuildingSO.BuildingType.Wall)
                        {
                            // Debug.Log("EHHHH MURITOO A LA IZQUIERDA");
                        }
                    }


                    if (buildPosition == _ActualBuildPosition)
                    {
                        GridObject g3 = _grid.GetObjectValue(buildPosition.x, buildPosition.y - 1);

                        if (g3?.GetPlaceBuild() != null)
                        {
                            if (g3.GetPlaceBuild().BuildingSo.type == BuildingSO.BuildingType.Wall &&
                                (g3.GetPlaceBuild().dir != BuildingSO.Dir.Right &&
                                 g3.GetPlaceBuild().dir != BuildingSO.Dir.Left))
                            {
                                // Debug.Log("EHHHH MURITOO Esquina construccion");
                            }
                        }
                    }
                    else
                    {
                        GridObject g3 = _grid.GetObjectValue(buildPosition.x, buildPosition.y + 1);

                        if (g3?.GetPlaceBuild() != null)
                        {
                            if (g3.GetPlaceBuild().BuildingSo.type == BuildingSO.BuildingType.Wall &&
                                (g3.GetPlaceBuild().dir != BuildingSO.Dir.Right &&
                                 g3.GetPlaceBuild().dir != BuildingSO.Dir.Left))
                            {
                                // Debug.Log("EHHHH MURITOO Esquina NOOOO construccion");
                            }
                        }
                    }
                }
            }
        }


        private bool CanBuild(List<Vector2Int> buildingPositions)
        {
            //Vector3 playerPosition = player.transform.position;

            //_grid.GetXZ(playerPosition, out int x, out int z);

            // Debug.Log("X: " + x + " Y: " + z);

            foreach (var buildPosition in buildingPositions)
            {
                GridObject g = _grid.GetObjectValue(buildPosition.x, buildPosition.y);
                if (g == null) return false;
                if (!g.CanBuild()) //||
                    //(buildPosition.y == z && buildPosition.x == x))
                    //  || (buildPosition.x))
                {
                    return false;
                }
            }


            return true;
        }


        private void DeselectObjectType(InputAction.CallbackContext callbackContext)
        {
            _buildingSO = null;

            RefreshSelectedObjectType();
        }

        private void RefreshSelectedObjectType()
        {
            OnSelectedChanged?.Invoke(this, EventArgs.Empty);
        }


        private Vector3 GetMousePosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(_control.Building.MousePosition.ReadValue<Vector2>());
            LayerMask mask = LayerMask.GetMask("MouseColliderMask");
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mask))
            {
                return raycastHit.point;
            }
            else
            {
                return Vector3.zero;
            }
        }


        public Vector2Int GetMouseGridPosition()
        {
            Vector3 mousePosition = GetMousePosition();
            //Debug.Log("posicion mundo: "+ mousePosition);

            _grid.GetXZ(mousePosition, out int x, out int z);


            Vector2Int placedObjectOrigin = new Vector2Int(x, z);

            if (x < 0 || z < 0 || x >= gridWidth || z >= gridHeight)
            {
                // Debug.Log("Fuera de los limites");
                return new Vector2Int(-1, -1);
            }


            placedObjectOrigin = _grid.ClampIntoGridPosition(placedObjectOrigin);

            return placedObjectOrigin;
        }


        public Vector3 GetMouseWorldSnappedPosition()
        {
            // Vector3 mousePosition = GetMousePosition(Input.mousePosition);
            // _grid.GetXZ(mousePosition, out int x, out int z);

            Vector2Int mousePos = GetMouseGridPosition();

            if (_buildingSO != null)
            {
                Vector2Int rotationOffset = _buildingSO.GetRotationOffset(_dir);
                Vector3 placedObjectWorldPosition = _grid.GetWorldPosition(mousePos) +
                                                    new Vector3(rotationOffset.x, 0, rotationOffset.y) *
                                                    _grid.cellSize;
                return placedObjectWorldPosition;
            }
            else
            {
                return default;
            }
        }


        public Vector3 GetMouseWorldSnappedPositionV2()
        {
            // Vector3 mousePosition = GetMousePosition(Input.mousePosition);
            // _grid.GetXZ(mousePosition, out int x, out int z);

            Vector2Int mousePos = GetMouseGridPosition();

            if (_buildingSO != null)
            {
                Vector2Int rotationOffset = _buildingSO.GetRotationOffset(_dir);
                Vector3 placedObjectWorldPosition = _grid.GetWorldPosition(_ActualBuildPosition) +
                                                    new Vector3(rotationOffset.x, 0, rotationOffset.y) *
                                                    _grid.cellSize;
                return placedObjectWorldPosition;
            }
            else
            {
                return default;
            }
        }


        public Quaternion GetPlacedObjectRotation()
        {
            if (_buildingSO != null)
            {
                return Quaternion.Euler(0, _buildingSO.GetRotationAngle(_dir), 0);
            }
            else
            {
                return Quaternion.identity;
            }
        }


        public BuildingSO GetPlacedObjectTypeSO()
        {
            return _buildingSO;
        }
    }
}