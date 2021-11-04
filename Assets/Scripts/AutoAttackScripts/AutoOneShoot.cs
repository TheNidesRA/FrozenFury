using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoAttackScripts
{
    public class AutoOneShoot : AutoShoot
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

        protected override void ShootEnemy(GameObject enemy)
        {
            Damage = 3;
            if (enemy == null) return;
            //calculate direction from the attackpoint to the enemy
            Vector3 directionShoot = enemy.transform.position - attackPoint.position;

            //instantiate bullet
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
            Destroy(currentBullet, _timeAlive);

            //add forces to bullet
            currentBullet.GetComponent<Rigidbody>().AddForce(directionShoot.normalized * shootForce, ForceMode.Impulse);
        }
    }
}