﻿using System;
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
                yield return new WaitForSeconds(0.5f);
                _enemyFactory.Create(enemy.Id, SpawnPoint);
                Debug.Log("Enemigo creado");
            }
        }
    }
}