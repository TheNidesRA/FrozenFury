﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
        
        
        public int gridWidth = 10;
        public int gridHeight = 10;
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


        private void Update()
        {
            // if (Input.GetMouseButtonDown(0))
            // {
            //     PlaceBuilding();
            // }

            // if (Input.GetKeyDown(KeyCode.R))
            // {
            //     Rotate();
            // }


            // if (Input.GetKeyDown(KeyCode.Alpha1))
            // {
            //     ChangeBuild(0);
            // }

            // if (Input.GetKeyDown(KeyCode.Alpha2))
            // {
            //     ChangeBuild(1);
            // }
            //
            // if (Input.GetKeyDown(KeyCode.Alpha3))
            // {
            //     ChangeBuild(2);
            // }
            //
            // if (Input.GetKeyDown(KeyCode.Alpha0))
            // {
            //     DeselectObjectType();
            // }


            // if (Input.GetMouseButtonDown(1))
            // {
            //     RemoveBuild();
            // }
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

        private GridObject GetMouseGridObject()
        {
            return _grid.GetObjectValue(GetMousePosition());
        }

        private void ChangeBuild(InputAction.CallbackContext callbackContext)
        {
  
            //Debug.Log("Bot: "+callbackContext.control.displayName);

            int i = int.Parse(callbackContext.control.displayName);
            i = i - 1;
            _buildingSO = _buildingsList[i];
            Debug.Log(_buildingSO.ToString());
            RefreshSelectedObjectType();
        }

        public void changeBuild(int build)
        {
            _buildingSO = _buildingsList[build];
            Debug.Log(_buildingSO.ToString());
            RefreshSelectedObjectType();
        }
        

        private void Rotate(InputAction.CallbackContext callbackContext)
        {
            //Debug.Log(_dir);
            _dir = BuildingSO.GetNextDir(_dir);
        }


        private void PlaceBuilding(InputAction.CallbackContext callbackContext)
        {
            if (_buildingSO != null)
            {
                Vector2Int mouseGridPosition = GetMouseGridPosition();
            
                Debug.Log(mouseGridPosition);

                List<Vector2Int> buildingPositions = _buildingSO.GetGridPositionList(mouseGridPosition, _dir);

                if (CanBuild(buildingPositions))
                {
                    Build(mouseGridPosition, buildingPositions);
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
            
            _grid.GetXZ(playerPosition,out int x, out int z);
            
            foreach (var buildPosition in buildingPositions)
            {
                if (!_grid.GetObjectValue(buildPosition.x, buildPosition.y).CanBuild() || buildPosition.y==z || buildPosition.x==x )
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