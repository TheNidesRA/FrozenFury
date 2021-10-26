using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Enemies
{
    /// <summary>
    /// This class will be in charge of instantiating the corresponding enemy prefab,
    ///<param name="_enmyConfiguration"> Will look for the prefab which to instantiate.</param>
    /// </summary>
    public class EnemyFactory
    {
        private EnemyConfiguration _enemyConfiguration;

        public EnemyFactory(EnemyConfiguration enemyConfiguration)
        {
            _enemyConfiguration = enemyConfiguration;
        }

        public Enemy Create(string id, Vector3 pos)
        {
            var enemy = _enemyConfiguration.GetEnemyPrefabById(id);
            enemy.PrintStats();
            return Object.Instantiate(enemy, pos, quaternion.identity);
        }

        public void UpdateEnemyStats(float[]mult)
        {
            _enemyConfiguration.UpdatePrefabStats(mult);
        }
    }
}