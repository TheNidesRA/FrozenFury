using System;
using System.Collections;
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
            currentBullet.GetComponent<Rigidbody>().velocity = CalculateLaunchData(currentEnemy).initialVelocity;
            
                 
        }

        LaunchData CalculateLaunchData(GameObject currentEnemy)
        {
            float displacementY = currentEnemy.transform.position.y - attackPoint.position.y;
            Vector3 displacementXZ = new Vector3(currentEnemy.transform.position.x - attackPoint.position.x , 0, currentEnemy.transform.position.z - attackPoint.position.z);
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
            /*
            //define the distance x and y first
            Vector3 distance_x_z = directionShoot;
            distance_x_z.Normalize();
            distance_x_z.y = 0;

            //creating a float that represents our distance 
            float sy = directionShoot.y;
            float sxz = directionShoot.magnitude;


            //calculating initial x velocity
            //Vx = x / t
            float Vxz = sxz / 1f;

            ////calculating initial y velocity
            //Vy0 = y/t + 1/2 * g * t
            float Vy = sy / 1f + 0.5f * Mathf.Abs(Physics.gravity.y) * 1f;

            Vector3 result = distance_x_z * Vxz;
            result.y = Vy;*/


            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);


            AssignShooterOfTheBullet(new List<GameObject>() { currentBullet });
            //currentBullet.GetComponent<Rigidbody>().velocity = result;
            Launch(enemy,currentBullet);
            //currentBullet.transform.position = Vector3.Lerp(attackPoint.position, enemy.transform.position, lerpRatio);
            //add forces to bullet
            //currentBullet.GetComponent<Rigidbody>().AddForce(directionShoot.normalized * shootForce, ForceMode.Impulse);
            Destroy(currentBullet, bulletTimeAlive);

                

            


            //instantiate bullet



            /* _timer += Time.deltaTime;

             if (_timer > lerpTime)
             {
                 _timer = lerpTime;
             }
             float lerpRatio = _timer / lerpTime;
             Vector3 positionOffset = lerpCurve.Evaluate(lerpRatio) * lerpOffset;

             currentBullet.transform.position = Vector3.Lerp(attackPoint.position, enemy.transform.position, lerpRatio);

             //add forces to bullet
             currentBullet.GetComponent<Rigidbody>().AddForce(result*currentBullet.GetComponent<Rigidbody>().mass, ForceMode.Impulse);*/
        }


        override
        public void RotatePlayerToEnemy(GameObject enemy)
        {
            if (enemy == null) return;
       

            Vector3 lookVector = enemy.transform.position - player.transform.position;
            lookVector.y = enemy.transform.position.y;
            Quaternion rot = Quaternion.LookRotation(lookVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);


            /*
            player.transform.LookAt(enemy.transform);
            player.transform.Rotate(Mathf.Clamp(enemy.transform.rotation.x,minX,maxX), enemy.transform.rotation.y, enemy.transform.rotation.z);
            Vector3 lookPosition = new Vector3(0f, enemy.transform.position.y, enemy.transform.position.z);
            Quaternion.LookRotation(lookPosition);
            if (player.transform.rotation.x > 0 || player.transform.rotation.x < 0)
            {
                 player.transform.Rotate(0, enemy.transform.rotation.y, enemy.transform.rotation.z);
            }else{
                 player.transform.LookAt(enemy.transform);
            }
            player.transform.Rotate(enemy.transform.rotation.x - 40, 0, 0);
            _objectiveDirection =
                Quaternion.LookRotation((enemy.transform.position - player.transform.position).normalized);
            player.transform.rotation =
                Quaternion.Slerp(player.transform.rotation, _objectiveDirection, Time.deltaTime * turnSpeed);
            */
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