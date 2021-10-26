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
        private InputPlayer testController;
        [SerializeField] private EnemyConfiguration enemyConfiguration;
        private EnemyFactory _enemyFactory;
        public Vector3 SpawnPoint;
        private void Awake()
        {
            testController = new InputPlayer();
            _enemyFactory = new EnemyFactory(Instantiate(enemyConfiguration));
            
        }

        private void Start()
        {
            testController.Test.SpawnEnemy.performed += testSpawn;
        }

        private void testSpawn(InputAction.CallbackContext obj)
        {
            // StartRound();
        }

        private void OnEnable()
        {
            testController.Enable();
        }

        
        private void OnDisable()
        {
            testController.Disable();
        }

        public void StartRound(List<Enemy> enemies)
        {
            StartCoroutine(SpawnWave(enemies));
        }


        private IEnumerator SpawnWave(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                yield return new WaitForSeconds(0.5f);
                _enemyFactory.Create(enemy.Id, SpawnPoint);
                Debug.Log("Enemigo creado");
            }
        }
    }
}