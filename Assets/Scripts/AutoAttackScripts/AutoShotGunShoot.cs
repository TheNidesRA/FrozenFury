using System;
using System.Security.Cryptography;
using UnityEngine;

namespace AutoAttackScripts
{
    public class AutoShotGunShoot : AutoShoot
    {
        private int _timeAlive = 4;

        public override float Damage
        {
            get => damage;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
                damage = value;
            }
        }

        public Transform attackPointLeft;
        public Transform attackPointRight;

        protected override void ShootEnemy(GameObject enemy)
        {
            Damage = 5;
            if (enemy == null) return;

            //calculate direction from the shot points to the enemy
            var enemyPos = enemy.transform.position;
            var positionLeft = attackPointLeft.position;
            var positionRight = attackPointRight.position;


            var directionShootLeft = enemyPos - positionLeft;

            var directionShootRight = enemyPos - positionRight;

            //instantiate bullets
            var firstBullet = Instantiate(bullet, positionLeft, Quaternion.identity);
            var secondBullet = Instantiate(bullet, positionRight, Quaternion.identity);

            Destroy(firstBullet, _timeAlive);
            Destroy(secondBullet, _timeAlive);

            //add forces to bullets
            firstBullet.GetComponent<Rigidbody>()
                .AddForce(directionShootLeft.normalized * shootForce, ForceMode.Impulse);
            secondBullet.GetComponent<Rigidbody>()
                .AddForce(directionShootRight.normalized * shootForce, ForceMode.Impulse);
        }
    }
}