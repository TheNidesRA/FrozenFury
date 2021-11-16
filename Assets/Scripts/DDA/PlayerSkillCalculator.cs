using System;
using UnityEngine;

namespace Enemies
{
    public class PlayerSkillCalculator : MonoBehaviour 
    {
        public float roundKills = 0;
        public float playerLvl = 1;
        public float maxStructLvl = 1;
        public float roundDeaths = 0;
        public float round = 1;
        
        public float rangeMin = 1;
        public float rangeMax = 1;

        public int maxRoundKills = 100;
        public int maxPlayerLvl = 50;
        public int maxValMaxStructLvl = 40;
        public int maxRoundDeaths = 5;
        public int maxRound = 100;
        

        public static PlayerSkillCalculator Instance;
        private void Awake()
        {
            rangeMin = (playerLvl + maxStructLvl + roundKills) / (round * (1 + (roundDeaths * 0.7f)));
            rangeMax = (maxPlayerLvl + maxValMaxStructLvl + maxRoundKills) / (maxRound * (1 + (maxRoundDeaths * 0.7f)));
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            
            WaveController._instance.OnRoundChange += (sender, i) => { round = i; };
            PlayerStats._instance.OnLvlChanged += (sender, i) => { playerLvl = i; };
            PlayerStats._instance.OnDeathEvent += (sender, b) => { roundDeaths++; };
        }

        public float ComputeSkill()
        {
            float skill = 0;

            skill = (playerLvl + maxStructLvl + roundKills) / (round * (1 + (roundDeaths * 0.7f)));

            float convertedSkill = (skill - rangeMin) * 10 / (rangeMax - rangeMin);
            
            return convertedSkill;
        }
    }
}