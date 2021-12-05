using System;
using System.Collections.Generic;
using UnityEngine;


namespace AutoAttackScripts
{
    public class cannonRotation : AutoShoot
    {
        public float h = 25;
        public float gravity = -9.8f;

        void Launch(GameObject currentEnemy, GameObject currentBullet)
        {
            Physics.gravity = Vector3.up * gravity;
            currentBullet.GetComponent<Rigidbody>().useGravity = true;
            Vector3 data = CalculateLaunchData(currentEnemy).initialVelocity;
            if (data.x is float.NaN)
            {
                return;
            }

            currentBullet.GetComponent<Rigidbody>().velocity =
                CalculateLaunchData(currentEnemy).initialVelocity + new Vector3(2, 0, 2);
            AudioManager.Instance?.PlayRandomHitSound();
        }

        LaunchData CalculateLaunchData(GameObject currentEnemy)
        {
            float displacementY = currentEnemy.transform.position.y - attackPoint.position.y;

            Vector3 displacementXZ = new Vector3(currentEnemy.transform.position.x - attackPoint.position.x, 0,
                currentEnemy.transform.position.z - attackPoint.position.z);

            float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
            Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
            Vector3 velocityXZ = displacementXZ / time;

            return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
        }

        struct LaunchData
        {
            public readonly Vector3 initialVelocity;
            public readonly float timeToTarget;

            public LaunchData(Vector3 initialVelocity, float timeToTarget)
            {
                this.initialVelocity = initialVelocity;
                this.timeToTarget = timeToTarget;
            }
        }

        protected override void ShootEnemy(GameObject enemy)
        {
            if (enemy == null) return;

            //calculate direction from the attackpoint to the enemy
            Vector3 directionShoot = enemy.transform.position - attackPoint.position;


            var position = this.transform.position;
            GameObject currentBullet =
                Instantiate(bullet, new Vector3(position.x, 5, position.z),
                    Quaternion.identity);


            AssignShooterOfTheBullet(new List<GameObject>() { currentBullet });
            //currentBullet.GetComponent<Rigidbody>().velocity = result;
            Launch(enemy, currentBullet);
            //currentBullet.transform.position = Vector3.Lerp(attackPoint.position, enemy.transform.position, lerpRatio);
            //add forces to bullet
            //currentBullet.GetComponent<Rigidbody>().AddForce(directionShoot.normalized * shootForce, ForceMode.Impulse);
            Destroy(currentBullet, bulletTimeAlive);
        }


        override
            public void RotatePlayerToEnemy(GameObject enemy)
        {
            if (enemy == null) return;


            Vector3 lookVector = (enemy.transform.position - player.transform.position).normalized;
            lookVector.y = 0;
            Quaternion rot = Quaternion.LookRotation(lookVector);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rot, Time.deltaTime * turnSpeed);
        }

        public override void StartStopShooting()
        {
            if (IsShootingEnabled())
            {
                GetComponent<Collider>().enabled = false;
                ClearEnemies();
            }
            else
            {
                GetComponent<Collider>().enabled = true;
            }
        }
    }
}