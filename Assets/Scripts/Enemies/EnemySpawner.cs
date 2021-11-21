using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Enemies
{
    /// <summary>
    /// This class will be the consumer of the Factory pattern and will contain both
    /// the EnemyFactory and the EnemyConfiguation
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyConfiguration enemyConfiguration;
        private EnemyFactory _enemyFactory;
        public Vector3 SpawnPoint;
        public Vector3 AirSapwnPoint;
        public float spawnDelay = 2.0f;

        private void Awake()
        {
            _enemyFactory = new EnemyFactory(Instantiate(enemyConfiguration));
        }

        public void StartRound(List<Enemy> enemies)
        {
            StartCoroutine(SpawnWave(enemies));
        }

        public void UpdateEnemyPrefabs(List<EnemyStats> updatedValues)
        {
            _enemyFactory.UpdateEnemyStats(updatedValues);
        }


        private IEnumerator SpawnWave(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                yield return new WaitForSeconds(spawnDelay);
                if (enemy.Id.Equals("Dragon") || enemy.Id.Equals("Pascual"))
                {
                    _enemyFactory.Create(enemy.Id, AirSapwnPoint);
                }
                else
                {
                    _enemyFactory.Create(enemy.Id, SpawnPoint);
                }

                // Debug.Log("Enemigo creado");
            }
        }

        public void UpdateSpawnDelay(float newDelay)
        {
            spawnDelay = newDelay;
        }
    }
}