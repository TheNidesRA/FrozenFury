using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    /// <summary>
    /// This class will be in charge of storing and accessing the specified enemy prefabs.
    /// <param name="enemies"> Will store all the different enemies.</param>
    /// <param name="_idToEnemy"> Will sort them in a key-value structure.</param>
    /// </summary>
    [CreateAssetMenu(menuName = "Custom/Enemy configuration")]
    public class EnemyConfiguration:ScriptableObject
    {
        [SerializeField] public Enemy[] enemies;
        private Dictionary<string, Enemy> _idToEnemy;

        private void Awake()
        {
            _idToEnemy = new Dictionary<string, Enemy>(enemies.Length);
            foreach (var enemy in enemies)
            {
                enemy.InitializeStats();
                _idToEnemy.Add(enemy.Id, enemy);
            }
        }


        /// <summary>
        /// Function in charged of looking for the enemy prefab.
        /// </summary>
        /// <param name="id"></param>
        public Enemy GetEnemyPrefabById(string id)
        {
            if (!_idToEnemy.TryGetValue(id, out var enemyPrefab))
            {
                throw new Exception($"No enemy matching id {id} introduced");
            }

            return enemyPrefab;
        }

        /// <summary>
        /// Update the prefab stats using the introduced multipliers
        /// </summary>
        public void UpdatePrefabStats(List<EnemyStats> updatedValues)
        {
            foreach (var stats in updatedValues)
            {
                if (!_idToEnemy.TryGetValue(stats.id, out var enemy))
                {
                    Debug.LogError("Not matching Id found while updating the enemy stats.");
                    break;
                }
                enemy.UpdateStats(stats);
            }
        }
    }
}