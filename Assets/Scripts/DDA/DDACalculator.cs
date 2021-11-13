using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Enemies
{
    /// <summary>
    /// Class in charged of regulating the enemies stat multipliers.
    /// </summary>
    public class DDACalculator : MonoBehaviour
    {
        /// <summary>
        /// Variable used to read the enemies initialStats
        /// </summary>
        public EnemyConfiguration enemyConfig;
        
        /// <summary>
        /// This stack will dynamically store those enemies who reach their goal.
        /// </summary>
        private Stack<WinnerStats> _winners;

        /// <summary>
        /// List with the enemies stats which will be used to keep track of them during the game
        /// </summary>
        private Dictionary<string, EnemyStats> _enemyStats;

        /// <summary>
        /// Variable with the global difficulty for the game.
        /// </summary>
        private float _globalDiff;

        /// <summary>
        /// Array containing the different variables which will be taken into account to compute the game difficulty
        ///     _diffVariables[0] --> EnemyStats
        ///     _diffVariables[1] --> EnemySpawn
        ///     _diffVariables[2] --> EnemyGold
        /// </summary>
        private float[] _diffVariables = new float[3]{1,1,1};
        
        /// <summary>
        ///Array containing the multipliers for the difficulty variables..
        ///     _diffVariables[0] --> StatsMult
        ///     _diffVariables[1] --> SpawnMult
        ///     _diffVariables[2] --> GoldMult 
        /// </summary>
        private float[] _diffMultipliers = new float[3]{1,1,1};
        
        /// <summary>
        /// Class in charged of modifying the different difficulty variables
        /// </summary>
        private MultiplierManager _multManager;

        private StatCalculator _statCalculator;

        private float _roundMaxHp = 0;

        public static DDACalculator instance { get; private set; }

        public AnimationCurve Curve;
        
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }

            _winners = new Stack<WinnerStats>();
            _enemyStats = new Dictionary<string, EnemyStats>();
            _multManager = new MultiplierManager();
            _statCalculator = new StatCalculator();
            
            foreach (var enemy in enemyConfig.enemies)
            {
                EnemyStats stats = new EnemyStats(enemy.Id, enemy.health, enemy.damage, enemy.speed, enemy.armor,
                    enemy.atackSpeed, enemy.gold);
                _enemyStats.Add(stats.id, stats);
            }
        }

        private void Start()
        {
            WaveController._instance.OnRoundChange += EndRoundFunction;
            WaveController._instance.OnWaveCreated += (sender, f) => { _roundMaxHp = f; };
        }

        private void EndRoundFunction(object sender, int e)
        {
            float totalBaseDmg = 0;
            float totalEnemyHp = 0;
            int winnersCount = _winners.Count;
            for (int i = 0; i < winnersCount; i++)
            {
                WinnerStats stats = _winners.Pop();
                totalBaseDmg += stats.baseDmg;
                totalEnemyHp += stats.hp;
            }

            float skill = PlayerSkillCalculator.Instance.ComputeSkill();
            _multManager.UpdateWithGlobalHealth(_diffVariables, _diffMultipliers, (int)totalBaseDmg);
            _multManager.UpdateWithWinnersHealth(_roundMaxHp, totalEnemyHp,
                                                        _diffMultipliers, ref _globalDiff);
            _multManager.UpdateWIthPlayerSkill(skill, _diffMultipliers);
            
            _statCalculator.UpdateVariables(_diffVariables, _diffMultipliers);
            
            
            Debug.Log("Base damage recived: " + totalBaseDmg + 
                      " \n Total enemy health: " + totalEnemyHp + " / " + _roundMaxHp);
        }

        /// <summary>
        /// Function called when an enemy reaches the Van
        /// </summary>
        /// <param name="enemy"></param>
        public void AddWinner(Enemy enemy)
        {
            WinnerStats winner = new WinnerStats(enemy.Id, enemy.health, enemy.baseDamage);
            _winners.Push(winner);
            Debug.Log(winner.id + " reached the Van with " + winner.hp + " HP.");
        }

        public string GetPointsTxt(int idx)
        {
            switch (idx)
            {
                case 0:
                    return "Status Points";
                case 1:
                    return "Spawn Points";
                case 2:
                    return "Gold Points";
                default:
                    return "OutOfBounds";
            }
        }
        public float GetPoints(int idx)
        {
            return _diffVariables[idx];
        }

        public string GetMultsTxt(int idx)
        {
            switch (idx)
            {
                case 0:
                    return "Status Multipliers";
                case 1:
                    return "Spawn Multipliers";
                case 2:
                    return "Gold Multipliers";
                default:
                    return "OutOfBounds";
            }
        }
        public float GetMults(int idx)
        {
            return _diffMultipliers[idx];
        }

        public float GetGlobalDiff()
        {
            return _globalDiff;
        }
    }
    
    #if UNITY_EDITOR
    [CustomEditor(typeof(DDACalculator))]
    class DDAEditor : Editor
    {
        private Vector2 scroll;
        private int current_tab;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = (DDACalculator)target;
            if(script == null) return;
            
            EditorGUILayout.Space();

            current_tab = GUILayout.Toolbar(current_tab, new string[] {"pts", "mult", "res"});

            switch (current_tab)
            {
                case 0:
                    scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(150));
                    
                    for (int i = 0; i < 3; i++)
                    {
                        EditorGUILayout.BeginHorizontal("box");
                        EditorGUILayout.LabelField(script.GetPointsTxt(i));
                        EditorGUILayout.LabelField((script.GetPoints(i) / script.GetMults(i)).ToString());
                        EditorGUILayout.EndHorizontal();
                    }
                    
                    EditorGUILayout.BeginHorizontal("box");
                    EditorGUILayout.LabelField(nameof(script.GetGlobalDiff));
                    EditorGUILayout.LabelField(script.GetGlobalDiff().ToString());
                    EditorGUILayout.EndHorizontal();
                    
                    EditorGUILayout.EndScrollView();
                    
                    break;
                    
                case 1:
                    scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(150));
                    
                    for (int i = 0; i < 3; i++)
                    {
                        EditorGUILayout.BeginHorizontal("box");
                        EditorGUILayout.LabelField(script.GetMultsTxt(i));
                        EditorGUILayout.LabelField(script.GetMults(i).ToString());
                        EditorGUILayout.EndHorizontal();
                    }
                    
                    EditorGUILayout.EndScrollView();
                    
                    break;
                
                case 2:
                    scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(150));
                    
                    for (int i = 0; i < 3; i++)
                    {
                        EditorGUILayout.BeginHorizontal("box");
                        EditorGUILayout.LabelField(script.GetPointsTxt(i));
                        EditorGUILayout.LabelField(script.GetPoints(i).ToString());
                        EditorGUILayout.EndHorizontal();
                    }
                    
                    EditorGUILayout.BeginHorizontal("box");
                    EditorGUILayout.LabelField(nameof(script.GetGlobalDiff));
                    EditorGUILayout.LabelField(script.GetGlobalDiff().ToString());
                    EditorGUILayout.EndHorizontal();
                    
                    EditorGUILayout.EndScrollView();
                    
                    break;
            }
        }
    }
}
#endif
