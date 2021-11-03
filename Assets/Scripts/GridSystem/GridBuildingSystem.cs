using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace GridSystem
{
    public class GridBuildingSystem : MonoBehaviour
    {
        //Its like a Singelton instance so other objects can use his functions
        public static GridBuildingSystem Instance { get; private set; }

        public GameObject player;

        private InputPlayer _control;

        [SerializeField] private bool _destroyOnPlace = false;

        /// <summary>
        /// called when _buildingSO is changed
        /// </summary>
        public event EventHandler OnSelectedChanged; //Callback 

        public event EventHandler OnObjectPlaced;

        public event EventHandler OnObjectSetPosition;
        public event EventHandler OnObjectRemovePosition;

        public event EventHandler OnClickOutOfObject;

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


        public bool buildMenu = false;
        public bool enableBuildMove = true;

        private PlacedBuild _currentPlaceBuild;
        private Vector2Int _ActualBuildPosition;

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
                (Grid<GridObject> global, int x, int z) => new GridObject(global, x, z), Vector3.zero);

            _buildingSO = null;
            _control.Building.LeftClick.performed += PlaceBuilding;
            _control.Building.Build1.performed += ChangeBuild;
            _control.Building.Build2.performed += ChangeBuild;
            _control.Building.Build3.performed += ChangeBuild;
            _control.Building.RigthClick.performed += RemoveBuild;
            _control.Building.UndoSelection.performed += DeselectObjectType;
            _control.Building.Rotate.performed += Rotate;
            _control.Building.Confirm.performed += Confirm;
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


   
        private void RemoveBuild(InputAction.CallbackContext callbackContext)
        {
            GridObject gridObject = GetMouseGridObject();

            PlacedBuild placedBuild = gridObject.GetPlaceBuild();

            if (placedBuild != null)
            {
                placedBuild.DestroySelf();

                List<Vector2Int> buildingPositions = placedBuild.GetGridPositionList();

                foreach (var buildPosition in buildingPositions)
                {
                    _grid.GetObjectValue(buildPosition.x, buildPosition.y).ClearPlacedBuild();
                }
            }
        }


        public void RemoveBuild(PlacedBuild build)
        {
            Debug.Log("asd");

            if (build != null)
            {
                build.DestroySelf();

                List<Vector2Int> buildingPositions = build.GetGridPositionList();

                foreach (var buildPosition in buildingPositions)
                {
                    _grid.GetObjectValue(buildPosition.x, buildPosition.y).ClearPlacedBuild();
                }
            }
        }


        private GridObject GetMouseGridObject()
        {
            return _grid.GetObjectValue(GetMousePosition());
        }

        private void ChangeBuild(InputAction.CallbackContext callbackContext)
        {
            //Debug.Log("Bot: "+callbackContext.control.displayName);

            int i = int.Parse(callbackContext.control.displayName);
            i = i - 1;
            BuildingSO targetBuild = _buildingsList[i];
            if (PlayerStats._instance.gold >= targetBuild.goldCost)
            {
                _buildingSO = targetBuild;
                Debug.Log(_buildingSO.ToString());
                RefreshSelectedObjectType();
            }
        }

        public void changeBuild(int build)
        {
            BuildingSO targetBuild = _buildingsList[build];
            if (PlayerStats._instance.gold >= targetBuild.goldCost)
            {
                _buildingSO = targetBuild;
                Debug.Log(_buildingSO.ToString());
                RefreshSelectedObjectType();
            }
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
            OnObjectSetPosition?.Invoke(this, EventArgs.Empty);
        }


        private void PlaceBuilding(InputAction.CallbackContext callbackContext)
        {
            if (_buildingSO != null && !buildMenu)
            {
                Vector2Int mouseGridPosition = GetMouseGridPosition();

                Debug.Log(mouseGridPosition);

                List<Vector2Int> buildingPositions = _buildingSO.GetGridPositionList(mouseGridPosition, _dir);

                if (CanBuild(buildingPositions))
                {
                    SetMousePosition(mouseGridPosition);
                }
                else
                {
                    Debug.Log("No se puede !!! :)");
                }
            }
            else
            {
                GridObject gridObject = GetMouseGridObject();

                if (gridObject != null)
                {
                    PlacedBuild placedBuild = gridObject.GetPlaceBuild();


                    if (placedBuild != null)
                    {
                        Debug.Log("Cogiendo el aux");
                        placedBuild.IsClicked();
                    }
                    else
                    {
                        //OnClickOutOfObject?.Invoke(this,EventArgs.Empty);
                    }
                }
            }
        }


        private void Confirm(InputAction.CallbackContext callbackContext)
        {
            List<Vector2Int> buildingPositions = _buildingSO.GetGridPositionList(_ActualBuildPosition, _dir);

            if (CanBuild(buildingPositions))
            {
                buildMenu = false;
                enableBuildMove = true;


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
                Debug.Log("No se puede !!! :)");
            }
        }

        public void Confirm()
        {
            List<Vector2Int> buildingPositions = _buildingSO.GetGridPositionList(_ActualBuildPosition, _dir);

            if (CanBuild(buildingPositions))
            {
                Build(_ActualBuildPosition, buildingPositions);
                PlayerStats._instance.gold -= _buildingSO.goldCost;
                RemoveFixedMouse();
                if (_destroyOnPlace)
                {
                    _buildingSO = null;
                    RefreshSelectedObjectType();
                }
            }
            else
            {
                Debug.Log("No se puede !!! :)");
            }
        }

        private void RemoveFixedMouse()
        {
            _ActualBuildPosition = new Vector2Int();
            buildMenu = false;
            enableBuildMove = true;
            OnObjectRemovePosition?.Invoke(this, EventArgs.Empty);
        }


        public void Mover()
        {
            buildMenu = false;
            enableBuildMove = true;
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


            foreach (var buildingPosition in buildingPositions)
            {
                _grid.GetObjectValue(buildingPosition.x, buildingPosition.y).SetPlacedBuild(placedBuild);
            }

            //callback
            OnObjectPlaced?.Invoke(this, EventArgs.Empty);
        }

        private bool CanBuild(List<Vector2Int> buildingPositions)
        {
            Vector3 playerPosition = player.transform.position;

            _grid.GetXZ(playerPosition, out int x, out int z);

            Debug.Log("X: " + x + " Y: " + z);

            foreach (var buildPosition in buildingPositions)
            {
                if (!_grid.GetObjectValue(buildPosition.x, buildPosition.y).CanBuild() ||
                    (buildPosition.y == z && buildPosition.x == x))
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