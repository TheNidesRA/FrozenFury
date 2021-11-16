using System;
using System.Collections;
using System.Collections.Generic;
using GridSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class WorldController : MonoBehaviour
{
    /// <summary>
    /// Variable in charged of controlling the distance between the
    /// spawn and the goal (taking the obstacles into account)
    /// </summary>
    private float _distReal;

    /// <summary>
    /// Variable in charged of controlling the Manhatam distance
    /// between the spawn and the enemy goal
    /// </summary>
    private float _distManh;

    /// <summary>
    /// This stat will compute the player skill and will be given by the DDA
    /// </summary>
    private float _playerSkill;

    /// <summary>
    /// This array contains the preference each enemy has for the next wave.
    /// The correspondence will be as followed:
    /// _weights[0] --> DonDiablo
    /// _weights[1] --> Golem
    /// _weights[2] --> BadBunny
    /// _weights[3] --> Boomfinn
    /// _weights[4] --> Dragon
    /// </summary>
    private float[] _weights;

    /// <summary>
    /// Variables will take into account the amount of each structure type.
    /// </summary>
    public float _numWalls, _numTurrets, _numTraps;

    /// <summary>
    /// List of all the in-game structures.
    /// </summary>
    private List<BuildStats> _structs;

    public NavMeshAgent refAgent;

    public static WorldController Instance;

    public String GetTotalStructs()
    {
        if (_structs != null)
            return _structs.Count.ToString();
        return "Not initialized";
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        _structs = new List<BuildStats>();
    }

    public void LevelUpgrade(BuildStats stats)
    {
        BuildStats aux = new BuildStats(-1);
        foreach (var structure in _structs)
        {
            if (structure.type == stats.type)
            {
                if (structure.lvl == stats.lvl - 1)
                {
                    aux = structure;
                    break;
                }
            }
        }

        if (aux.lvl != -1)
        {
            RemoveStruct(aux);
            AddStruct(stats);
        }
    }

    public void AddStruct(BuildStats stats)
    {
        _structs.Add(stats);
        switch (stats.type)
        {
            case BuildingSO.BuildingType.Wall:
                _numWalls += 1 + (stats.lvl - 1) * 0.5f;
                break;
            case BuildingSO.BuildingType.Trap:
                _numTraps += 1 + (stats.lvl - 1) * 0.5f;
                break;
            case BuildingSO.BuildingType.Turret:
                _numTurrets += 1 + (stats.lvl - 1) * 0.5f;
                break;
        }
    }

    public void RemoveStruct(BuildStats stats)
    {
        _structs.Remove(stats);
        switch (stats.type)
        {
            case BuildingSO.BuildingType.Wall:
                _numWalls -= 1 + (stats.lvl - 1) * 0.5f;
                break;
            case BuildingSO.BuildingType.Trap:
                _numTraps -= 1 + (stats.lvl - 1) * 0.5f;
                break;
            case BuildingSO.BuildingType.Turret:
                _numTurrets -= 1 + (stats.lvl - 1) * 0.5f;
                break;
        }
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(WorldController))]
class WorldControllerEditor : Editor
{
    private Vector2 scroll;
    private int current_tab;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (WorldController) target;
        if (script == null) return;

        EditorGUILayout.Space();


        scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(150));

        EditorGUILayout.BeginHorizontal("box");
        EditorGUILayout.LabelField("Num Walls");
        EditorGUILayout.LabelField(script._numWalls.ToString());
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal("box");
        EditorGUILayout.LabelField("Num Traps");
        EditorGUILayout.LabelField(script._numTraps.ToString());
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal("box");
        EditorGUILayout.LabelField("Num Turrets");
        EditorGUILayout.LabelField(script._numTurrets.ToString());
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal("box");
        EditorGUILayout.LabelField("TotalStructs");
        EditorGUILayout.LabelField(script.GetTotalStructs());
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndScrollView();
    }
}
#endif