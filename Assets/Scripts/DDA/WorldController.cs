using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
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
    private float _numWalls;
    [HideInInspector] public float NumWalls
    {
        get => _numWalls;
        set
        {
            _numWalls = value;
            float total = _numWalls + _numTurrets;
            OnStructChanged?.Invoke(this, total);
        }
    }

    private float _numTurrets;
    [HideInInspector] public float NumTurrets
    {
        get => _numTurrets;
        set
        {
            _numTurrets = value;
            float total = _numWalls + _numTurrets;
            OnStructChanged?.Invoke(this, total);
        }
    }
    [HideInInspector] public float _numTraps;
    
    [HideInInspector]
    public int _realNumWalls, _realNumTurrets, _realNumTraps;

    private int round = 1;

    public event EventHandler<float> OnStructChanged; 

    /// <summary>
    /// List of all the in-game structures.
    /// </summary>
    private List<BuildStats> _structs;

    /// <summary>
    /// Amount of rounds gone without any base damage.
    /// </summary>
    private int _streak = 0;

    public AnimationCurve playerSkillCurve;
    public AnimationCurve diffDistCurve;
    public AnimationCurve structsCorrection;
    public AnimationCurve streakCurve;


    public NavMeshAgent refAgent;

    public static WorldController Instance;
    
    

    public String GetTotalStructs()
    {
        if (_structs != null)
            return _structs.Count.ToString();
        return "Not initialized";
    }

    public float[] GetWeights()
    {
        return _weights;
    }

    public void SetPlayerSkill(float skill)
    {
        this._playerSkill = skill;
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

    private void Aux(object sender, int i)
    {
        round = i;
    }
    private void OnDestroy()
    {
        WaveController._instance.OnRoundChange -= Aux;
    }
    private void Start()
    {
        WaveController._instance.OnRoundChange += Aux;
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
                NumWalls += 1 + (stats.lvl - 1) * 0.5f;
                _realNumWalls++;
                break;
            case BuildingSO.BuildingType.Trap:
                _numTraps += 1 + (stats.lvl - 1) * 0.5f;
                _realNumTraps++;
                break;
            case BuildingSO.BuildingType.Turret:
                NumTurrets += 1 + (stats.lvl - 1) * 0.5f;
                _realNumTurrets++;
                break;
        }
    }

    public void RemoveStruct(BuildStats stats)
    {
        _structs.Remove(stats);
        switch (stats.type)
        {
            case BuildingSO.BuildingType.Wall:
                NumWalls -= 1 + (stats.lvl - 1) * 0.5f;
                _realNumWalls--;
                break;
            case BuildingSO.BuildingType.Trap:
                _numTraps -= 1 + (stats.lvl - 1) * 0.5f;
                _realNumTraps--;
                break;
            case BuildingSO.BuildingType.Turret:
                NumTurrets -= 1 + (stats.lvl - 1) * 0.5f;
                _realNumTurrets--;
                break;
        }
    }

    /// <summary>
    /// Function in charge of modifying the different enemy weights:
    /// _weights[0] --> DonDiablo
    /// _weights[1] --> Golem
    /// _weights[2] --> BadBunny
    /// _weights[3] --> Boomfinn
    /// _weights[4] --> Dragon
    /// </summary>
    public void UpdateWeights()
    {
        _weights = new float[] {0.6f, 0.2f, 0.1f, 0.0f, 0.1f};
        
        float skillCorrection = playerSkillCurve.Evaluate(_playerSkill / 10);
        _weights[0] -= skillCorrection;
        _weights[2] += skillCorrection;

        CheckDists();
        float distCorrection = diffDistCurve.Evaluate((_distReal - _distManh) / 100);
        _weights[1] += distCorrection;
        for (int i = 0; i < _weights.Length; i++)
        {
            if (i != 3 && i != 1 && i != 4)
            {
                _weights[i] -= distCorrection / 2;
                CheckIfNegative(ref _weights[i], ref _weights[1]);
            }
        }

        float structCorrection = structsCorrection.Evaluate((float)(_realNumTurrets + _realNumWalls) / 10);
        _weights[1] += structCorrection;
        if (_weights[1] < 0)
        {
            structCorrection += Math.Abs(_weights[1]);
            _weights[1] = 0;
        }
        for (int i = 0; i < _weights.Length; i++)
        {
            if (i != 3 && i != 1 && i != 4)
            {
                _weights[i] -= structCorrection / 2;
                CheckIfNegative(ref _weights[i], ref _weights[1]);
            }
        }

        float streakCorrection = streakCurve.Evaluate(_streak);
        _weights[4] += streakCorrection;
        _weights[0] -= streakCorrection / 2;
        CheckIfNegative(ref _weights[0], ref _weights[4]);
        _weights[2] -= streakCorrection / 2;
        CheckIfNegative(ref _weights[2], ref _weights[4]);


        if (_weights[1] > 0.25f)
        {
            _weights[3] = _weights[1] - 0.35f;
            _weights[1] = 0.35f;
        }
        if (round < 5)
        {
            _weights[0] += _weights[2];
            _weights[2] = 0;
        }
    }

    private void CheckIfNegative(ref float val1, ref float val2)
    {
        if (val1 < -0.00000001f)
        {
            val2 += val1;
            val1 = 0;
        }
    }

    public void CheckDists()
    {
        _distReal = refAgent.remainingDistance;
        _distManh = Vector3.Distance(EnemyGoal.instance.getPosition(), refAgent.transform.position);
    }

    public void UpdtateStreake(bool streak)
    {
        if (streak)
        {
            _streak++;
        }
        else
        {
            _streak = 0;
        }
    }

    public string GetWeight(int idx)
    {
        try
        {
            return _weights[idx].ToString();
        }
        catch
        {
            return "Not initialized";
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

        current_tab = GUILayout.Toolbar(current_tab, new string[] {"Structs", "Weights"});

        switch (current_tab)
        {
            case 0:
                scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(150));

                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("Num Walls");
                EditorGUILayout.LabelField(script.NumWalls.ToString());
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("Num Traps");
                EditorGUILayout.LabelField(script._numTraps.ToString());
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("Num Turrets");
                EditorGUILayout.LabelField(script.NumTurrets.ToString());
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("Real Walls");
                EditorGUILayout.LabelField(script._realNumWalls.ToString());
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("Real Traps");
                EditorGUILayout.LabelField(script._realNumTraps.ToString());
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("Real Turrets");
                EditorGUILayout.LabelField(script._realNumTurrets.ToString());
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("TotalStructs");
                EditorGUILayout.LabelField(script.GetTotalStructs());
                EditorGUILayout.EndHorizontal();
                break;
            case 1:
                scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(170));
                
                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("Diablos");
                EditorGUILayout.LabelField(script.GetWeight(0));
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("Golems");
                EditorGUILayout.LabelField(script.GetWeight(1));
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("BadBunny");
                EditorGUILayout.LabelField(script.GetWeight(2));
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("BoomFins");
                EditorGUILayout.LabelField(script.GetWeight(3));
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("Dragons");
                EditorGUILayout.LabelField(script.GetWeight(4));
                EditorGUILayout.EndHorizontal();
                break;
        }

        

        EditorGUILayout.EndScrollView();
    }
}
#endif