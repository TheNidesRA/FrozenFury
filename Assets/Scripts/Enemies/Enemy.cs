using System;
using System.Collections;
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

        private bool isAttacking = false;
        public event Action<GameObject> OnEnemyDeath;

        public string Id => id;

        public float Health;
        public float Damage;
        public float Speed;
        public float Armor;
        public float AtackSpeed;
        public float gold;


        private void OnEnable()
        {
            NavMeshAgent.speed = Speed;
        }

        public void UpdateStats(float[] mult)
        {
            Health = Health * mult[0];
            Damage = Damage * mult[1];
            Speed = Speed * mult[2];
            Armor = Armor * mult[3];
            AtackSpeed = AtackSpeed * mult[4];
        }

        public virtual void InitializeStats()
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

        public void Attack()
        {
            if (isAttacking) return;
            isAttacking = true;
            Debug.Log("Ataca!");
            StartCoroutine(nameof(StartAnimation));

        }

        public bool IsAttackFinished()
        {
            return !isAttacking;
        }

        private IEnumerator StartAnimation()
        {
            yield return new WaitForSeconds(AtackSpeed);
            isAttacking = false;
        }
    }
}