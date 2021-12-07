using System;
using System.Collections.Generic;
using AutoAttackScripts;
using GridSystem;
using UnityEngine;
using UnityEngine.AI;
#if UNITY_EDITOR
using UnityEditor;

#endif


public class PlacedBuild : MonoBehaviour
{
    protected NavMeshObstacle _navMeshObstacle;
    protected BuildingSO _buildingSo;
    public BuildingSO BuildingSo => _buildingSo;
    public Vector2Int origin => _origin;
    public BuildingSO.Dir dir => _dir;
    protected Vector2Int _origin;
    protected BuildingSO.Dir _dir;

    public const int MAXLEVEL = 15;

    public List<Transform> attactPoints;

    public GameObject Area = null;


    [SerializeField] protected float _initDamage;
    [SerializeField] protected float _initAttackSpeed;
    [SerializeField] protected float _initHealth;
    [SerializeField] protected int _initGoldCostLevel;


    [SerializeField] protected float _damage;
    [SerializeField] protected float _attackSpeed;
    [SerializeField] protected float _health;
    public float currentMaxHealth;
    [SerializeField] protected int _level;
    [SerializeField] protected int _goldCostLevel;
    [SerializeField] protected int _goldCostRepair;


    public AnimationCurve CurveHealth;
    public AnimationCurve CurveDamage;
    public AnimationCurve CurveAttackSpeed;
    public AnimationCurve CurveGoldLevelCost;

    public int MAXHEALTH;
    public int MAXDAMAGE;
    public float MAXATTACKSPEED;
    public int MAXGOLDCOSTLEVEL;


    public int level
    {
        get => _level;
        set
        {
            _level = value;
            Evaluate();
            BuildStats stats = new BuildStats(_level, _buildingSo.type);
            WorldController.Instance.LevelUpgrade(stats);
        }
    }

    public float damage
    {
        get { return _damage; }
        set
        {
            _damage = value;
            if (Shoot != null)
                Shoot.DamagePerBullet = damage;
        }
    }

    public float attackSpeed
    {
        get => _attackSpeed;
        set
        {
            _attackSpeed = value;
            if (Shoot != null)
                Shoot.shootsPerSecond = value;
        }
    }

    public int goldCostUpgrade
    {
        get => _goldCostLevel;
    }

    public int goldcostRepair
    {
        get => _goldCostRepair;
    }

    public float health
    {
        get { return _health; }
        set
        {
            if (value <= 0)
            {
                _health = 0;
                GridBuildingSystem.Instance.RemoveBuild(this);
            }

            _health = value;
            Debug.Log(_buildingSo.name + " vida restante : " + _health);
        }
    }


    public AutoShoot Shoot;

    public void Awake()
    {
    }


    protected void initStart()
    {
        CurveHealth = AnimationCurve.EaseInOut(1, _initHealth, MAXLEVEL, MAXHEALTH);
        CurveDamage = AnimationCurve.EaseInOut(1, _initDamage, MAXLEVEL, MAXDAMAGE);
        CurveAttackSpeed = AnimationCurve.EaseInOut(1, _initAttackSpeed, MAXLEVEL, MAXATTACKSPEED);
        CurveGoldLevelCost = AnimationCurve.EaseInOut(1, _initGoldCostLevel, MAXLEVEL, MAXGOLDCOSTLEVEL);

        level = 1;
    }

    private void Start()
    {
        initStart();
    }

    public static PlacedBuild Create(Vector3 worldPosition, Vector2Int origin, BuildingSO.Dir dir, BuildingSO building)
    {
        Transform placedBuildTransform = Instantiate(
            building.prefab,
            worldPosition,
            Quaternion.Euler(0, building.GetRotationAngle(dir), 0)
        );

        PlacedBuild placedBuild = placedBuildTransform.GetComponent<PlacedBuild>();
        placedBuild._buildingSo = building;
        placedBuild._dir = dir;

        // if (building.type != BuildingSO.BuildingType.Trap)
        // {
        //     // placedBuild._navMeshObstacle = placedBuildTransform.GetComponent<NavMeshObstacle>();
        //     // placedBuild._navMeshObstacle.enabled = true;
        // }

        placedBuild._initDamage = building.initDamage;
        placedBuild._initHealth = building.initHealth;
        placedBuild._initAttackSpeed = building.initAttackSpeed;
        placedBuild._initGoldCostLevel = building.initGoldCostLevel;
        placedBuild._goldCostRepair = building.GoldCostRepair;

        placedBuild.MAXDAMAGE = building.MAXDAMAGE;
        placedBuild.MAXHEALTH = building.MAXHEALT;
        placedBuild.MAXATTACKSPEED = building.MAXATTACKSPEED;
        placedBuild.MAXGOLDCOSTLEVEL = building.MAXGOLDCOSTLEVEL;


        // placedBuild.getValidAttacksPoints();
        return placedBuild;
    }


    private void Activate(object sender, EventArgs eventArgs)
    {
        _navMeshObstacle.enabled = true;
    }


    public void DestroySelf()
    {
        Destroy(gameObject);
    }


    public List<Vector2Int> GetGridPositionList()
    {
        return _buildingSo.GetGridPositionList(origin, dir);
    }


    private void Update()
    {
        foreach (var VARIABLE in attactPoints)
        {
            Debug.DrawRay(VARIABLE.position, VARIABLE.forward * 10f, Color.green);
        }
    }

    public List<Transform> getValidAttacksPoints()
    {
        List<Transform> valid = new List<Transform>();
        foreach (var VARIABLE in attactPoints)
        {
            RaycastHit hit;

            if (Physics.Raycast(VARIABLE.transform.position, VARIABLE.forward, out hit, 8f,
                1 << LayerMask.NameToLayer("Wall") |
                (1 << LayerMask.NameToLayer("Torreta") | (1 << LayerMask.NameToLayer("Muros")) |
                 (1 << LayerMask.NameToLayer("MuroPlayer")))))
            {
//                Debug.Log(VARIABLE.gameObject);

                //              Debug.Log("Punto blocked");

                VARIABLE.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            }
            else
            {
                valid.Add(VARIABLE);
            }
        }

        return valid;
    }

    [ContextMenu("Subir nivel")]
    /// <summary>
    ///  No usar
    /// </summary>
    public void UpdateLevel()
    {
        level++;
    }


    public void LevelingUp()
    {
        if (!(PlayerStats._instance.gold >= _goldCostLevel)) return;
        PlayerStats._instance.gold -= _goldCostLevel;
        level++;
    }

    private void Evaluate()
    {
        damage = CurveDamage.Evaluate(level);
        attackSpeed = CurveAttackSpeed.Evaluate(level);
        _health = CurveHealth.Evaluate(level);
        _goldCostLevel = (int)CurveGoldLevelCost.Evaluate(level);
        currentMaxHealth = CurveHealth.Evaluate(level);

        // Debug.Log("Nuevas estats para : " + _buildingSo.name + " Damage: " + _damage + " attacspeed: " + _attackSpeed +
        // "health : " + health + " /" + currentMaxHealth);
    }

    public void Repair()
    {
        if (!(PlayerStats._instance.gold >= _goldCostRepair)) return;
        if (_health == currentMaxHealth) return;
        PlayerStats._instance.gold -= _goldCostRepair;
        _health = currentMaxHealth;
    }


    public void ShowArea()
    {
        if (Area != null)
            Area.SetActive(true);
    }

    public void HideArea()
    {
        if (Area != null)
            Area.SetActive(false);
    }

    [ContextMenu("ValorZero")]
    public void ValorZero()
    {
        damage = 0;
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(PlacedBuild))]
class PlacedBuildEditor : Editor
{
    private Vector2 scroll;
    int current_tab = 0;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        float maxWidth = 200;
        float maxHeight = 300;

        var script = (PlacedBuild)target;
        if (script == null) return;

        EditorGUILayout.Space();


        current_tab = GUILayout.Toolbar(current_tab, new string[] { "Damage", "AttackSpeed", "Health", "Gold" });

        switch (current_tab)
        {
            case 0:
                scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(maxHeight));

                for (int i = 1; i <= PlacedBuild.MAXLEVEL; i++)
                {
                    EditorGUILayout.BeginHorizontal("box");
                    EditorGUILayout.LabelField("Level " + (i));
                    EditorGUILayout.LabelField(((int)script.CurveDamage.Evaluate(i)) + " Damage");
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndScrollView();

                break;
            case 1:
                scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(maxHeight));

                for (int i = 1; i <= PlacedBuild.MAXLEVEL; i++)
                {
                    EditorGUILayout.BeginHorizontal("box");
                    EditorGUILayout.LabelField("Level " + (i));
                    EditorGUILayout.LabelField(((int)script.CurveAttackSpeed.Evaluate(i)) + " AttackSpeed");
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndScrollView();
                break;
            case 2:
                scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(maxHeight));

                for (int i = 1; i <= PlacedBuild.MAXLEVEL; i++)
                {
                    EditorGUILayout.BeginHorizontal("box");
                    EditorGUILayout.LabelField("Level " + (i));
                    EditorGUILayout.LabelField(((int)script.CurveHealth.Evaluate(i)) + " Health");
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndScrollView();
                break;
            case 3:
                scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(maxHeight));

                for (int i = 1; i <= PlacedBuild.MAXLEVEL; i++)
                {
                    EditorGUILayout.BeginHorizontal("box");
                    EditorGUILayout.LabelField("Level " + (i));
                    EditorGUILayout.LabelField(((int)script.CurveGoldLevelCost.Evaluate(i)) + " GoldCost");
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndScrollView();
                break;
        }
    }
}
#endif