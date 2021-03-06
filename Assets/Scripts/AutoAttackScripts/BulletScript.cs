using Enemies;
using UnityEngine;

namespace AutoAttackScripts
{
    public class BulletScript : MonoBehaviour
    {
        private GameObject _enemyToRemove;
        private Enemy _enemy;
        private bool _bulletFromPlayer;
        public float bulletDamage;
        private BuildingSO _buildingInfo;

        public bool BulletFromPlayer
        {
            get => _bulletFromPlayer;
            set => _bulletFromPlayer = value;
        }

        public BuildingSO BuildingInfo
        {
            get => _buildingInfo;
            set => _buildingInfo = value;
        }

        public virtual void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag($"Enemy"))
            {
                _enemyToRemove = other.gameObject;
                _enemy = _enemyToRemove.GetComponent<Enemy>();
            }

            PlayerSkillCalculator.Instance.kills++;

            Destroy(gameObject);
        }


        private void OnDestroy()
        {
            if (_enemyToRemove == null) return;

            if (_bulletFromPlayer)
            {
                if (!_enemy.OnHit(bulletDamage))
                    return;

                PlayerSkillCalculator.Instance.kills++;
            }
            else
            {
                if (!_enemy.OnHit(bulletDamage))
                    return;

                PlayerSkillCalculator.Instance.UpdateRoundKills();
            }

            _enemy.Die();
        }
    }
}