using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;


namespace AutoAttackScripts
{
    public class turretBulletScript : MonoBehaviour
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
            if (_enemy.OnHit(3))
            {
                _enemy.Die();
            }
        }
    }
}