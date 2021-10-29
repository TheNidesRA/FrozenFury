using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;


namespace AutoAttackScripts
{
    public class turretBulletScript : MonoBehaviour
    {
        //public AutoShoot turret;
        private GameObject _enemyToRemove;
        private Enemy _enemy;

        // private void Start()
        // {
        //     turret = GameObject.Find("TorretaA").GetComponentInChildren<AutoShoot>();
        // }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag($"Enemy")) return;
            _enemyToRemove = other.gameObject;
            _enemy = _enemyToRemove.GetComponent<Enemy>();
            Destroy(gameObject);
        }


        private void OnDestroy()
        {
            if (_enemyToRemove == null) return;
            if (_enemy.OnHit(3))
            {
                //turret.RemoveEnemy(_enemyToRemove);
                _enemy.Die();
            }
        }
    }
}