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
        private List<EnemyStats> _enemyStats;
        
        

        private MultiplierManager _multManager;
        

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
            _enemyStats = new List<EnemyStats>();
            
            foreach (var enemy in enemyConfig.enemies)
            {
                EnemyStats stats = new EnemyStats(enemy.Id, enemy.Health, enemy.Damage, enemy.Speed, enemy.Armor,
                    enemy.AtackSpeed, enemy.gold);
                _enemyStats.Add(stats);
            }

            WaveController._instance.OnRoundChange += EndRoundFunction
                ;
        }

        private void EndRoundFunction(object sender, int e)
        {
            Debug.Log("Aquí se calcularían los ajustes de dificultad.");
        }

        /// <summary>
        /// Function called when an enemy reaches the Van
        /// </summary>
        /// <param name="enemy"></param>
        public void AddWinner(Enemy enemy)
        {
            WinnerStats winner = new WinnerStats(enemy.Id, enemy.Health);
            _winners.Push(winner);
            Debug.Log(winner.id + " reached the Van with " + winner.hp + " HP.");
        }
    }
}