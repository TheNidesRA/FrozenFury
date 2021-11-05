using Enemies;
using UnityEngine;

namespace AutoAttackScripts
{
    public class BulletScript : MonoBehaviour
    {
        private GameObject _enemyToRemove;
        private Enemy _enemy;
        private bool _bulletFromPlayer;

        public bool BulletFromPlayer
        {
            get => _bulletFromPlayer;
            set => _bulletFromPlayer = value;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag($"Enemy"))
            {
                _enemyToRemove = other.gameObject;
                _enemy = _enemyToRemove.GetComponent<Enemy>();
            }

            Destroy(gameObject);
        }


        private void OnDestroy()
        {
            if (_enemyToRemove == null) return;
            if (_bulletFromPlayer)
            {
                if (!_enemy.OnHit(PlayerStats._instance.Damage))
                    return;
            }
            else
            {
                if (!_enemy.OnHit(3))
                    return;
            }

            _enemy.Die();
        }
    }
}