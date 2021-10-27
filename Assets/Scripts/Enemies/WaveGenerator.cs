using UnityEngine;

namespace Enemies
{
    public class WaveGenerator
    {
        private int levelDificulty;
        private int _minEnemies;
        private Enemy[] _enemies;
        

        public WaveGenerator(EnemyConfiguration config, int lvlDif, int min)
        {
            _minEnemies = min;
            levelDificulty = lvlDif;
            _enemies = config.enemies;
        }

        public Wave GenerateWave(int round)
        {
            //Generate the wave
            Wave wave = new Wave();
            
            //Compute the amount of enemies int this wave
            int enemyAmount = _minEnemies + (round * levelDificulty * 30)/100;

            for (int i = 0; i < enemyAmount; i++)
            {
                Enemy enemyToAdd = _enemies[Random.Range(0, _enemies.Length)];
                enemyToAdd.PrintStats();
                wave.Enemies.Add(enemyToAdd);
            }
            return wave;
        }
    }
}