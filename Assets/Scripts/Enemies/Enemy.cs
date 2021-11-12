using System;
using UnityEngine;
using UnityEngine.AI;

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
        public NavMeshAgent NavMeshAgent;

        public event Action<GameObject> OnEnemyDeath;

        public string Id => id;

        public float health;
        public float damage;
        public float speed;
        public float armor;
        public float atackSpeed;
        public float baseDamage;
        public float gold;

      

        private void OnEnable()
        {
            NavMeshAgent.speed=speed;
        }

        public void UpdateStats(float[] mult)
        {
            health = health * mult[0];
            damage = damage * mult[1];
            speed = speed * mult[2];
            armor = armor * mult[3];
            atackSpeed = atackSpeed * mult[4];
        }

        public virtual void InitializeStats()
        {
            health = _initStats.initHp;
            damage = _initStats.initDmg;
            speed = _initStats.initSpd;
            armor = _initStats.initArm;
            atackSpeed = _initStats.initAtkSpd;
            baseDamage = _initStats.initBaseDamage;
            gold = _initStats.gold;
        }


        public bool OnHit(float dmg)
        {
            health -= dmg;

            return health <= 0;
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
            Debug.Log("HP: " + health + " // Dmg: " + damage + " // Spd: " + speed + " // Arm: " + armor +
                      " // AtkSpd: " + atackSpeed);
        }
    }
}