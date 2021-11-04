using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    /// <summary>
    /// Class in charged of regulating the enemies stat multipliers.
    /// </summary>
    public class DDACalculator : MonoBehaviour
    {
        private Stack<EnemyStats> _winners;
        public string[] enemyTypes;

        /// <summary>
        ///  Contains the multipliers for the DDA:
        ///  multiplier[0] -> Health
        ///  multiplier[1] -> Damage
        ///  multiplier[2] -> Speed
        ///  multiplier[3] -> Armor
        ///  multiplier[4] -> Atack Speed
        /// </summary>
        private float[] _multipliers;

        /// <summary>
        /// Function called when a enemy reach the endPoint
        /// </summary>
        /// <param name="rs">Enemy remaining stats</param>
        public void UpdateRemainingStats(EnemyStats rs)
        {
            _winners.Push(rs);
        }

        public Stack<string> CalculateRoundEnemies()
        {
            Stack<string> roundEnemies = new Stack<string>();
            return null;
        }

        public void ComputeAdjustment(int round)
        {
            if (round == 1) return;


            int enemyWinners = _winners.Count;
            float remainingHp = 0;
            foreach (var stats in _winners)
            {
                remainingHp += stats.hp;
            }


        }
    }
}