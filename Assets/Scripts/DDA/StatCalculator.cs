using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class StatCalculator
    {
        public void UpdateVariables(float[] vars, float[] mults)
        {
            for (int i = 0; i < vars.Length; i++)
            {
                if (vars[i] <= 0)
                {
                    vars[i] = 0.1f;
                }

                if (mults[i] <= 0)
                {
                    mults[i] = 0.1f;
                }
                vars[i] *= mults[i];
                
            }
        }

        public List<EnemyStats> UpdateStats(List<EnemyStats> statsList, Dictionary<string, EnemyStats>initStats,
                                    float[] vars, float diff)
        {
            List<EnemyStats> returnList = new List<EnemyStats>();
            
            foreach (var stats in statsList)
            {
                if(!initStats.TryGetValue(stats.id, out EnemyStats init))
                {
                    Debug.LogError("No matching id during de StatCalculator process.");
                    break;
                }

                // var enemyStats = stats;
                //Calculate the new states using the difficulty variables over the initial enemy stats.
                var enemyStats = init;
                
                enemyStats.hp = init.hp * vars[0] + diff * 2;
                if (enemyStats.hp < init.hp)
                {
                    enemyStats.hp = init.hp;
                }
                enemyStats.dmg = init.dmg * vars[0] * 0.1f + diff * 0.1f;
                if (enemyStats.dmg < init.dmg)
                {
                    enemyStats.dmg = init.dmg;
                }
                // enemyStats.atackSpd = init.atackSpd + (vars[0] * diff * 0.05f);
                enemyStats.gold = init.gold * vars[2] * 0.1f - diff * 0.2f;
                if (enemyStats.gold < init.gold)
                {
                    enemyStats.gold = init.gold;
                }
                returnList.Add(enemyStats);
            }

            return returnList;
        }

        private bool CheckIfNegative(float val)
        {
            return val < 0;
        }
        
    }
}