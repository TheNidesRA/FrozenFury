using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Enemies
{
    /// <summary>
    /// This class will be the consumer of the Factory pattern and will contain both
    /// the EnemyFactory and the EnemyConfiguation
    /// </summary>
    public class EnemySpawner:MonoBehaviour
    {
        
        [SerializeField] private EnemyConfiguration enemyConfiguration;
        private EnemyFactory _enemyFactory;
        public Vector3 SpawnPoint;
        public float spawnDelay = 2.0f;
        private void Awake()
        {
            _enemyFactory = new EnemyFactory(Instantiate(enemyConfiguration));
        }

        public void StartRound(List<Enemy> enemies)
        {
            StartCoroutine(SpawnWave(enemies));
        }


        private IEnumerator SpawnWave(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                yield return new WaitForSeconds(spawnDelay);
                _enemyFactory.Create(enemy.Id, SpawnPoint);
                Debug.Log("Enemigo creado");
            }
        }
    }
}