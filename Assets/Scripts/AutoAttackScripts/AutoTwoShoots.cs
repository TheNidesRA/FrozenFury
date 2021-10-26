using System;
using UnityEngine;

namespace AutoAttackScripts
{
    public class AutoTwoShoots : AutoShoot
    {
        private int _timeAlive = 4;
        public override float Damage
        {
            get => 2;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
                damage = 2;
            }
        }

        protected override void ShootEnemy(GameObject enemy)
        {
            if (enemy == null) return;
            //calculate direction from the attackpoint to the enemy
            Vector3 directionShoot = enemy.transform.position - attackPoint.position;

            //instantiate bullet
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
            Destroy(currentBullet, _timeAlive);
            //add forces to bullet
            currentBullet.GetComponent<Rigidbody>().AddForce(directionShoot.normalized * shootForce, ForceMode.Impulse);

            StartCoroutine(nameof(WaitToShootBulletBurst), enemy);
        }
    }
}