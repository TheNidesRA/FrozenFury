using System;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class WaveGenerator
    {
        private float _roundProgression;
        private int _minEnemies;
        private Enemy[] _enemies;

        public WaveGenerator(EnemyConfiguration config, float lvlDif, int min)
        {
            _minEnemies = min;
            _roundProgression = lvlDif;
            _enemies = config.enemies;
        }

        public Wave GenerateWave(int round, float spwnPts)
        {
            //Generate the wave
            Wave wave = new Wave();
            
            //Compute the amount of enemies int this wave
            float enemyAmount = _minEnemies + (round * _roundProgression) + spwnPts;

            float[] weights = WorldController.Instance.GetWeights();

            float numDemons = enemyAmount * weights[0];
            float numGolems = enemyAmount * weights[1];
            float numBunnys = enemyAmount * weights[2];
            float numBoomfins = enemyAmount * weights[3];
            float numDragons = enemyAmount * weights[4];
            for (int i = 0; i < _enemies.Length; i++)
            {
                switch (_enemies[i].Id)
                {
                    case "Diablillo":
                        for (int j = 0; j < numDemons; j++)
                        {
                            Enemy enemyToAdd = _enemies[i];
                            // enemyToAdd.PrintStats();
                            wave.Enemies.Add(enemyToAdd);
                        }
                        break;
                        
                    case "Golem":
                        for (int j = 0; j < numGolems; j++)
                        {
                            Enemy enemyToAdd = _enemies[i];
                            // enemyToAdd.PrintStats();
                            wave.Enemies.Add(enemyToAdd);
                        }
                        break;
                    
                    case "Pascual":
                        for (int j = 0; j < numBunnys; j++)
                        {
                            Enemy enemyToAdd = _enemies[i];
                            // enemyToAdd.PrintStats();
                            wave.Enemies.Add(enemyToAdd);
                        }
                        break;
                    
                    case "Boomffing":
                        for (int j = 0; j < numBoomfins; j++)
                        {
                            Enemy enemyToAdd = _enemies[i];
                            // enemyToAdd.PrintStats();
                            wave.Enemies.Add(enemyToAdd);
                        }
                        break;
                        
                    case "Dragon":
                        for (int j = 0; j < numDragons; j++)
                        {
                            Enemy enemyToAdd = _enemies[i];
                            // enemyToAdd.PrintStats();
                            wave.Enemies.Add(enemyToAdd);
                        }
                        break;

                }
                
            }
            return wave;
        }
    }
}