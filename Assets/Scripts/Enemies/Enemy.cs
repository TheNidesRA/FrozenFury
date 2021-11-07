using System;
using UnityEngine;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        /// <summary>
        /// Enemy id
        /// </summary>
        [SerializeField] protected string id;

        /// <summary>
        /// Initial enemy stats
        /// </summary>
        [SerializeField] private InitStats _initStats;

        /// <summary>
        /// Agente navmesh que moverá al enemigo
        /// </summary>
        private EnemyIA _ia;

        public event Action<GameObject> OnEnemyDeath;

        public string Id => id;

        public float Health;
        public float Damage;
        public float Speed;
        public float Armor;
        public float AtackSpeed;
        public float gold;

        private void Awake()
        {
            try
            {
                _ia = GetComponent<EnemyIA>();
            }
            catch (Exception e)
            {
                Debug.Log("You have´t add a EnemyIA component to the enemy prefab");
            }
        }

        private void OnEnable()
        {
            _ia.setSpeed(Speed);
        }

        public void UpdateStats(float[] mult)
        {
            Health = Health * mult[0];
            Damage = Damage * mult[1];
            Speed = Speed * mult[2];
            Armor = Armor * mult[3];
            AtackSpeed = AtackSpeed * mult[4];
        }

        public void InitializeStats()
        {
            Health = _initStats.initHp;
            Damage = _initStats.initDmg;
            Speed = _initStats.initSpd;
            Armor = _initStats.initArm;
            AtackSpeed = _initStats.initAtkSpd;
            gold = _initStats.gold;
        }


        public bool OnHit(float dmg)
        {
            Health -= dmg;

            return Health <= 0;
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            OnEnemyDeath?.Invoke(gameObject);
            PlayerStats._instance.gold += gold;
            WaveController._instance.EnemyDeath();
        }

        public void PrintStats()
        {
            Debug.Log("HP: " + Health + " // Dmg: " + Damage + " // Spd: " + Speed + " // Arm: " + Armor +
                      " // AtkSpd: " + AtackSpeed);
        }
    }
}