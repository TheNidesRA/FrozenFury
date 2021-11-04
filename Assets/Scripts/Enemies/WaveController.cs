﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Enemies
{
    public class WaveController : MonoBehaviour
    {
        public EnemySpawner Spawner;
        /// <summary>
        /// Minimmun enemies per round
        /// </summary>
        public int minEnemies = 10;

        /// <summary>
        /// Global level difficulty
        /// </summary>
        public int winRound = 2;
        public int levelDificulty = 10;
        private Wave _currentWave;
        private int _round=1;
        private int _enemiesAlive;
        private bool _roundActive;
        [SerializeField] public EnemyConfiguration enemiconfig;
        
        private WaveGenerator _generator;

        public event EventHandler<int> OnRoundChange;
        public int round
        {
            get => _round;
            set
            {
                _round = value;
                OnRoundChange?.Invoke(this, _round);
            }
        }
        
        
        //Lo hacemos singletone
        public static WaveController _instance { get; private set; }

        private void Awake()
        {
            _generator = new WaveGenerator(enemiconfig, levelDificulty, minEnemies);
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

        #region TestInput

        

        
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
        #endregion

        [ContextMenu("Comenzar la ronda")]
        public void StartWave()
        {
            
            if (_currentWave == null || !_roundActive)
            {
                SceneController._instance.round = _round;
                _currentWave = _generator.GenerateWave(_round);
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
            round++;
            if(_round == winRound){SceneController._instance.GoToWinScene();}
            Debug.Log("Fin de la ronda. \n" +
                      "round:  " + _round);
        }
    }
}