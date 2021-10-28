using System;
using System.Collections;
using Enemies;
using UnityEngine;

namespace AutoAttackScripts
{
    public class BulletScript : MonoBehaviour
    {
        public AutoShoot player;
        private GameObject _enemyToRemove;
        private Enemy _enemy;

        private void Start()
        {
            player = GameObject.Find("Player").GetComponentInChildren<AutoShoot>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag($"Enemy"))
            {
                _enemyToRemove = other.gameObject;
                _enemy = _enemyToRemove.GetComponent<Enemy>();
                Destroy(gameObject);
            }
        }


        private void OnDestroy()
        {
            if (_enemyToRemove == null) return;
            if (!_enemy.OnHit(player.Damage)) return;
            //player.RemoveEnemy(_enemyToRemove);
            _enemy.Die();
        }
    }
}