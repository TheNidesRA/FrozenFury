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

        public event Action<float> OnHealthChanged = delegate {  };
        

        //APARTADO DE MOVIMIENTO (TEMPORAL)
        private Transform _enemyTransform; //Se puede borrar
        private EnemySpawner _spawner;
        private Vector3 _objective;
        private Vector3 _direction;
        //FIN DE APARTADO DE MOVIMIENTO
        public string Id => id;

        public float Health, HpMult = 1.5f;
        public float Damage, DmgMult = 1.5f;
        public float Speed, SpdMult = 1.5f;
        public float Armor, ArmMult = 1.5f;
        public float AtackSpeed, AtkSpMult = 1.5f;

        private void Start()
        {
            InitializeStats();
        }

        public void UpdateStats(float[]mult)
        {
            Health = Health * mult[0];
            Damage = Damage * mult[1];
            Speed = Speed * mult[2];
            Armor = Armor * mult[3];
            AtackSpeed = AtackSpeed * mult[4];
        }

        public void InitializeStats()
        {
            Health = _initStats.InitHp;
            Damage = _initStats.InitDmg;
            Speed = _initStats.InitSpd;
            Armor = _initStats.InitArm;
            AtackSpeed = _initStats.InitAtkSpd;
        }


        public bool OnHit(float dmg)
        {
            Health -= dmg;
            
            if (Health <= 0) return true;

            OnHealthChanged(Health);
            
            return false;
        }

        public RemainingStats GetReaminingStats()
        {
            RemainingStats rStats = new RemainingStats(Health, Damage, Speed);
            return rStats;
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            WaveController._instance.EnemyDeath();
        }

        public void PrintStats()
        {
            Debug.Log("HP: " + Health + " // Dmg: " + Damage + " // Spd: " + Speed + " // Arm: " + Armor + " // AtkSpd: " + AtackSpeed);
        }
    }
}


