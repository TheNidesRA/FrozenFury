using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Enemies
{
    public class WaveController : MonoBehaviour
    {
        public EnemySpawner Spawner;
        public List<Wave> Waves;
        private Wave _currentWave;
        private int _round=0;
        private int _enemiesAlive;
        private bool _roundActive;
        
        
        //Lo hacemos singletone
        public static WaveController _instance { get; private set; }

        private void Awake()
        {
            testController = new InputPlayer();
            if (_instance != null && _instance!=this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }
        
        ////////////////////Test Input/////////////////////////////
        private InputPlayer testController;

        private void Start()
        {
            testController.Test.SpawnEnemy.performed += testSpawn;
        }

        private void testSpawn(InputAction.CallbackContext obj)
        {
            StartWave();
        }

        private void OnEnable()
        {
            testController.Enable();
        }

        
        private void OnDisable()
        {
            testController.Disable();
        }
        
        /////////////////Fin de Test Input///////////////////////////
        

        [ContextMenu("Comenzar la ronda")]
        public void StartWave()
        {
            if (_currentWave == null || !_roundActive)
            {
                if (_round > Waves.Count - 1) return;
                _currentWave = Waves[_round];
                Spawner.StartRound(_currentWave.Enemies);
                _enemiesAlive = _currentWave.Enemies.Count;
                _roundActive = true;
            }
        }

        public void EnemyDeath()
        {
            _enemiesAlive--;
            if (_enemiesAlive <= 0)
            {
                EndRound();
            }
        }

        public void EndRound()
        {
            _roundActive = false;
            _round++;
            Debug.Log("Fin de la ronda");
        }
    }
}