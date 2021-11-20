using System;
using UnityEngine;

namespace Enemies
{
    public class PlayerSkillCalculator : MonoBehaviour 
    {
        public float kills = 0;
        public float playerLvl = 1;
        public float stucts = 0;
        public float playerDeaths = 0;
        
        public float rangeMin = 1;
        public float rangeMax = 1;

        public int maxKills = 1000;
        public int maxPlayerLvl = 50;
        public int maxStructs = 50;
        public int maxDeaths = 50;

        private int round = 1;
        

        public static PlayerSkillCalculator Instance;
        private void Awake()
        {
            rangeMin = (playerLvl + stucts + kills) / (1 + maxDeaths);
            rangeMax = (maxPlayerLvl + maxStructs + maxKills) / (1 + playerDeaths);
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
            WorldController.Instance.OnStructChanged += (sender, f) => { stucts = f; };
            WaveController._instance.OnRoundChange += (sender, i) => { round = i; };
            PlayerStats._instance.OnLvlChanged += (sender, i) => { playerLvl = i; };
            PlayerStats._instance.OnDeathEvent += (sender, b) => { playerDeaths++; };
        }

        public float ComputeSkill()
        {
            float skill = 0;

            skill = (playerLvl + stucts + kills) / (1 + playerDeaths);

            //This formula converts the skill value into a range between 0 and 10
            float convertedSkill = (skill - rangeMin) * 10 / (rangeMax - rangeMin);
            
            Debug.Log("PlayerSkill = " + convertedSkill);
            return convertedSkill;
        }

        

        public void UpdateRoundKills()
        {
            kills++;
        }
    }
}