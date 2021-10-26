//Class from https://www.youtube.com/watch?v=waEsGu--9P8&list=PLzDRvYVwl53uhO8yhqxcyjDImRjO9W722

using System;
using GridSystem;
using UnityEngine;

public class Grid<TGridObject>
{
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;

    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private int _width; //Number of colums
    private int _height; //Number of rows
    private float _cellSize; //Size of cell 

    public float cellSize => _cellSize; //Getter

    private Vector3 _originPosition; //Where the grid is located
    private TGridObject[,] _gridArray; //Matrix of elemnts
    private TextMesh[,] _debugTextArray;
    private bool _debug = false;

    public Grid(int width, int height, float cellSize,
        Func<Grid<TGridObject>, int, int, TGridObject> createFunc,
        Vector3 originPosition = default(Vector3))
    {
        this._width = width;
        this._height = height;
        this._cellSize = cellSize;
        this._originPosition = originPosition;
        Debug.Log("Origin" + originPosition);
        _gridArray = new TGridObject[width, height];
        _debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < _gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < _gridArray.GetLength(1); z++)
            {
                _gridArray[x, z] = createFunc(this, x, z);
            }
        }
        
        _debug = true;
        if (_debug)
        {
            for (int x = 0; x < _gridArray.GetLength(0); x++)
            {
                for (int z = 0; z < _gridArray.GetLength(1); z++)
                {
                    // _debugTextArray[x, z] = TextWorldUtil.CreateWorldText(_gridArray[x, z]?.ToString(), null,
                    //     GetWorldPosition(x, z) + new Vector3(cellSize, 0, cellSize) * 0.5f,
                    //     new Vector3(1, 0, 0), 90, 20,
                    //     Color.white, TextAnchor.MiddleCenter);
                 
                    Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.white, 100f);
                }
            }
            
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
        }
    }

    
    /// <summary>
    /// Returns grid position cell in world coordinates
    /// </summary>
    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * _cellSize + _originPosition;
    }
    
    
    /// <summary>
    /// Returns grid position cell in world coordinates
    /// </summary>
    public Vector3 GetWorldPosition(Vector2Int position)
    {
        return new Vector3(position.x, 0, position.y) * _cellSize + _originPosition;
    }

    
    
    /// <summary>
    /// Translate world position into cell position
    /// </summary>
    public void GetXZ(Vector3 worldPosition, out int x, out int z)
    {
        x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
        z = Mathf.FloorToInt((worldPosition - _originPosition).z / _cellSize);
        
        // if (x < 0 || z < 0 || x >= _width || z >= _height)
        // {
        //     Debug.Log("Fuera de los limites");
        //     x = 0;
        //     z = 0;
        // }
    }

    /// <summary>
    /// Put an object into a cell
    /// </summary>
    public void SetObject(int x, int z, TGridObject value)
    {
        if (x >= 0 && z >= 0 && x < _width && z < _height)
        {
            _gridArray[x, z] = value;
            _debugTextArray[x, z].text = value.ToString();
            TriggerGridObjectChanged(x, z);
        }
    }


    public void TriggerGridObjectChanged(int x, int y)
    {
        OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEventArgs {x = x, y = y});
    }
    
    /// <summary>
    /// Put an object into a cell
    /// </summary>
    public void SetValue(Vector3 worldPosition, TGridObject value)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        SetObject(x, z, value);
    }
    /// <summary>
    /// Get object value from a cell
    /// </summary>
    public TGridObject GetObjectValue(int x, int z)
    {
        if (x >= 0 && z >= 0 && x < _width && z < _height)
        {
            return _gridArray[x, z];
        }
        else
        {
            return default(TGridObject); // = null
        }
    }

    /// <summary>
    /// Get object value from a cell
    /// </summary>
    public TGridObject GetObjectValue(Vector3 worldPosition)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        return GetObjectValue(x, z);
    }

    /// <summary>
    /// Put the coordinates inside of the grid
    /// </summary>
    public Vector2Int ClampIntoGridPosition(Vector2Int gridPosition)
    {
        return new Vector2Int(
            Mathf.Clamp(gridPosition.x, 0, _width - 1),
            Mathf.Clamp(gridPosition.y, 0, _height - 1)
        );
    }
}