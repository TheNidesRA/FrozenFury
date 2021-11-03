using System;
using System.Collections;
using Enemies;
using UnityEngine;

namespace AutoAttackScripts
{
    public class BulletScript : MonoBehaviour
    {
        private GameObject _enemyToRemove;
        private Enemy _enemy;
        
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
            if (!_enemy.OnHit(PlayerStats._instance.Damage)) return;
            _enemy.Die();
        }
    }
}