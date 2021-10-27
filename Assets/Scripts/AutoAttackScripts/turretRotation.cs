using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AutoAttackScripts
{
    public class turretRotation : AutoShoot
    {
        private int _timeAlive = 4;
        public override float Damage
        {
            get => 3;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
                damage = 3;
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
        }

        override
        public void RotatePlayerToEnemy(GameObject enemy)
        {
            if (enemy == null) return;
            player.transform.LookAt(enemy.transform);
             /*_objectiveDirection =
                 Quaternion.LookRotation((enemy.transform.position - player.transform.position).normalized);
             player.transform.rotation =
                 Quaternion.Slerp(player.transform.rotation, _objectiveDirection, Time.deltaTime * turnSpeed);*/
        }

        public override void StartStopShooting()
        {
            var component = GetComponent<Collider>();
            if (component.enabled)
            {
                component.enabled = false;
                ClearEnemies();
            }
            else
            {
                component.enabled = true;
            }
        }
    }
}
