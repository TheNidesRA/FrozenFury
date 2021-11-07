using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AutoAttackScripts
{
    public class cannonRotation : AutoShoot
    {
        private int _timeAlive = 4;
        private float lerpTime = 3f;
        private float _timer = 0f;
        private GameObject currentBullet;
        public AnimationCurve lerpCurve;
        public Vector3 lerpOffset;
        private float lerpRatio;

        protected override void ShootEnemy(GameObject enemy)
        {
            if (enemy == null) return;
            //calculate direction from the attackpoint to the enemy
            Vector3 directionShoot = enemy.transform.position - attackPoint.position;

          


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
            float Vy = sy/1f + 0.5f * Mathf.Abs(Physics.gravity.y) * 1f;
           
            Vector3 result = distance_x_z * Vxz;
            result.y = Vy;

            //instantiate bullet
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
            AssignShooterOfTheBullet(new List<GameObject>() { currentBullet });
            currentBullet.GetComponent<Rigidbody>().velocity = result;

            //currentBullet.transform.position = Vector3.Lerp(attackPoint.position, enemy.transform.position, lerpRatio);
            //add forces to bullet
            //currentBullet.GetComponent<Rigidbody>().AddForce(result * currentBullet.GetComponent<Rigidbody>().mass, ForceMode.Impulse);
            Destroy(currentBullet, _timeAlive);


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

      /*  public void FixedUpdate()
        {
            _timer += Time.deltaTime;

            if (_timer > lerpTime)
            {
                _timer = lerpTime;
            }
            lerpRatio = _timer / lerpTime;
            Vector3 positionOffset = lerpCurve.Evaluate(lerpRatio) * lerpOffset;
        }*/

        override
        public void RotatePlayerToEnemy(GameObject enemy)
        {
            if (enemy == null) return;

            player.transform.LookAt(enemy.transform);
            player.transform.Rotate(enemy.transform.rotation.x - 40, 0, 0);
            
            

            /*_objectiveDirection =
                Quaternion.LookRotation((enemy.transform.position - player.transform.position).normalized);
            player.transform.rotation =
                Quaternion.Slerp(player.transform.rotation, _objectiveDirection, Time.deltaTime * turnSpeed);*/
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