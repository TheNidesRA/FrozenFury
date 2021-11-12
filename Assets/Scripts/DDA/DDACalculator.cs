using System;
using System.Collections.Generic;
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

        private float _roundMaxHp = 0;

        public static DDACalculator instance { get; private set; }
        
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
            _multManager.UpdateWithGlobalHealth(_diffVariables, _diffMultipliers, (int)totalBaseDmg);
            _multManager.UpdateWithWinnersHealth(_roundMaxHp, totalEnemyHp,
                                                        _diffMultipliers, ref _globalDiff);
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
    }
}