using System;
using UnityEngine;

namespace Enemies
{
    public class WaveController : MonoBehaviour
    {
        public EnemySpawner Spawner;
        public GameObject canvasContinue;


        /// <summary>
        /// Minimmun enemies per round
        /// </summary>
        public int minEnemies = 10;

       
        /// <summary>
        /// Cuantos bichos extra spawnear
        /// </summary>
        private float spwnPts;

       
        public int winRound = 2;

        /// <summary>
        /// How Enemy increase per round
        /// </summary>
       
        public float spawnProgression = 0.7f;

        public AnimationCurve spawnTimeProgression;

        private Wave _currentWave;
        private int _round = 1;
        private int _enemiesAlive;


        private bool _roundActiveAux;

        private bool _roundActive
        {
            get => _roundActiveAux;
            set
            {
                _roundActiveAux = value;

                OnRoundActive?.Invoke(this, _roundActive);
            }
        }


        [SerializeField] public EnemyConfiguration enemiconfig;

        private WaveGenerator _generator;

        public event EventHandler<int> OnRoundChange;
        public event EventHandler<float> OnWaveCreated;
        public event EventHandler<bool> OnRoundActive;

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
            _generator = new WaveGenerator(enemiconfig, spawnProgression, minEnemies);
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }


        [ContextMenu("Comenzar la ronda")]
        public void StartWave()
        {
            if (_currentWave == null || !_roundActive)
            {
                SceneController._instance.round = _round;
                WorldController.Instance.UpdateWeights();
                _currentWave = _generator.GenerateWave(_round, spwnPts);
                float totalHp = 0;
                foreach (var enemy in _currentWave.Enemies)
                {
                    totalHp += enemy.health;
                }

                OnWaveCreated?.Invoke(this, totalHp);
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
                GameObject.Find("AudioFunctions").GetComponent<AudioSettings>().LowerVolumeStartRound();
                AudioManager.Instance?.Play("Finalizar Ronda");
                AudioManager.Instance?.Stop("Partida");
                AudioManager.Instance?.Play("Chill Theme");
            }
        }

        public void EndRound()
        {
            _roundActive = false;
            round++;
            if (_round == winRound)
            {
                canvasContinue.SetActive(true);
                //SceneController._instance.GoToWinScene();
            }

            /*Debug.Log("Fin de la ronda. \n" +
                      "round:  " + _round);*/
            //Lee la curva y le pasa el numerito
            Spawner.UpdateSpawnDelay(spawnTimeProgression.Evaluate(round));
        }

        public void SetSpawnPoints(float pts)
        {
            spwnPts = pts;
        }
    }
}