using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildObject", menuName = "Build/DefaultBuild")]
public class BuildingSO : ScriptableObject
{
    public static Dir GetNextDir(Dir dir)
    {
        switch (dir)
        {
            default: return Dir.Down;
            case Dir.Down: return Dir.Left;
            case Dir.Left: return Dir.Up;
            case Dir.Up: return Dir.Right;
            case Dir.Right: return Dir.Down;
        }
    }


    public int GetRotationAngle(Dir dir)
    {
        switch (dir)
        {
            default: return 0;
            case Dir.Down: return 0;
            case Dir.Left: return 90;
            case Dir.Up: return 180;
            case Dir.Right: return 270;
        }
    }

    public Vector2Int GetRotationOffset(Dir dir)
    {
        switch (dir)
        {
            default: return new Vector2Int(0, 0);
            case Dir.Down: return new Vector2Int(0, 0);
            case Dir.Left: return new Vector2Int(0, width);
            case Dir.Up: return new Vector2Int(width, heigth);
            case Dir.Right: return new Vector2Int(heigth, 0);
        }
    }


    public enum Dir
    {
        Down,
        Left,
        Up,
        Right,
    }

    public enum BuildingType
    {
        Turret,
        Wall,
        Trap
    }


    public int width;
    public int heigth;
    public float initDamage;
    public float initHealth;
    public float initAttackSpeed = 1;
    public int initGoldCostLevel;
    public int GoldCostRepair;
    public int MAXHEALT;
    public int MAXDAMAGE;
    public float MAXATTACKSPEED;
    public int MAXGOLDCOSTLEVEL;
    public string name;
    public Transform prefab;
    public Transform visual;
    public Transform canvasVisual;

    public int goldCost = 1;
    public BuildingType type;

    public List<Vector2Int> GetGridPositionList(Vector2Int offset, Dir dir)
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>();


        switch (dir)
        {
            default:
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < heigth; y++)
                    {
                        gridPositionList.Add(offset + new Vector2Int(x, y));
                    }
                }

                break;
            case Dir.Down:
            case Dir.Up:

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < heigth; y++)
                    {
                        gridPositionList.Add(offset + new Vector2Int(x, y));
                    }
                }

                break;

            case Dir.Left:
            case Dir.Right:

                for (int x = 0; x < heigth; x++)
                {
                    for (int y = 0; y < width; y++)
                    {
                        gridPositionList.Add(offset + new Vector2Int(x, y));
                    }
                }

                break;
        }


        return gridPositionList;
    }


    public Vector2 GetCenter(Dir dir)
    {
        switch (dir)
        {
            default:
                return new Vector2(width / 2.0f, heigth / 2.0f);


            case Dir.Down:
            case Dir.Up:

                return new Vector2(width / 2.0f, heigth / 2.0f);


            case Dir.Left:
            case Dir.Right:

                return new Vector2(heigth / 2.0f, width / 2.0f);
        }
    }


    public override string ToString()
    {
        return name;
    }
}