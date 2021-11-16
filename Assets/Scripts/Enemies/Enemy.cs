using System;
using TheKiwiCoder;
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

        #region EventosBossy

        public event Action<GameObject> OnEnemyDeath;
        public event Action<GameObject> OnHealthChanged;

        #endregion

        private float initSpeed;

        public string Id => id;

        public float health;
        public float damage;
        public float speed;
        public float armor;
        public float attackSpeed;
        public float baseDamage;
        public float gold;
        public bool invencibilidadTrampa = false;
        public float tiempoInvencibilidad = 5f;


        public bool afecctedTrap = false;


        //Cosas añadidads por mi elnidas para lo del bt

        public Vector3 targetPosition;
        public GameObject actionTarget;
        public GameObject auxActionTarget;
        public float angleVision;
        public float radioVision;

        public string NODOACTUAL;

        private void Awake()
        {
            actionTarget = null;
            auxActionTarget = null;
        }

        private void OnEnable()
        {
            initSpeed = speed;
            NavMeshAgent.speed = speed;
        }

        public void UpdateStats(EnemyStats stats)
        {
            health = stats.hp;
            damage = stats.dmg;
            speed = stats.speed;
            armor = stats.armor;
            attackSpeed = stats.atackSpd;
            
            PrintStats();
        }

        public void InitializeStats()
        {
            health = _initStats.initHp;
            damage = _initStats.initDmg;
            speed = _initStats.initSpd;
            armor = _initStats.initArm;
            attackSpeed = _initStats.initAtkSpd;
            baseDamage = _initStats.initBaseDamage;
            gold = _initStats.gold;
        }


        public bool OnHit(float dmg)
        {
            health -= dmg;
            OnHealthChanged?.Invoke(gameObject);
            return health <= 0;
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        public void OnSlow(float slowDown)
        {
            if (!afecctedTrap)
            {
                speed = speed * slowDown;
                NavMeshAgent.speed = speed;
            }

            afecctedTrap = true;
        }

        public void OnResetSlow()
        {
            speed = initSpeed;
            NavMeshAgent.speed = speed;
            afecctedTrap = false;
        }

        public void OnHitTrap(float dmg)
        {
            if (!invencibilidadTrampa && health > 0)
            {
                health -= dmg;
                StartCoroutine(OnInvencible());
            }


            if (health <= 0)
            {
                Die();
            }
        }

        public IEnumerator OnInvencible()
        {
            invencibilidadTrampa = true;
            yield return new WaitForSeconds(tiempoInvencibilidad);
            invencibilidadTrampa = false;
        }


        private void OnDestroy()
        {
            OnEnemyDeath?.Invoke(gameObject);
            PlayerStats._instance.gold += gold;
            WaveController._instance.EnemyDeath();
        }

        public void PrintStats()
        {
            Debug.Log(Id+ "\nHP: " + health + " // Dmg: " + damage + " // Spd: " + speed + " // Arm: " + armor +
                      " // AtkSpd: " + attackSpeed);
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
            yield return new WaitForSeconds(attackSpeed);
            isAttacking = false;
        }
    }
}