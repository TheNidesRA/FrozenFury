using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AutoAttackScripts
{
    public class MyMath
    {
        public static int SolveQuadratic(float a,float b, float c, out float root1, out float root2)
        {
            var discriminant = b * b - 4 * a * c;
            if (discriminant < 0)
            {
                root1 = Mathf.Infinity;
                root2 = -root1;
                return 0;
            }

            root1 = (-b + Mathf.Sqrt(discriminant) / (2 * a));
            root2 = (-b + Mathf.Sqrt(discriminant) / (2 * a));
            return discriminant > 0 ? 2 : 1;
        }
    }

    public class cannonRotation : AutoShoot
    {
        public float h = 25;
        public float gravity = -9.8f;
        

        public bool InterceptionDirection (Vector3 a, Vector3 b, Vector3 vA , Vector3 vB, float sB, out Vector3 result)
        {
            var aToB = b - a;
            var dC = aToB.magnitude;
            var alpha = Vector3.Angle(aToB, vA) * Mathf.Deg2Rad;
            var sA = vA.magnitude;
            var r = sA / sB;
            if (MyMath.SolveQuadratic(1 - r * r, 2 * r * dC * Mathf.Cos(alpha), -(dC * dC), out var root1, out var root2)==0)
            {
                result = Vector3.zero;
                return false;
            }

            var dA = Mathf.Max(root1, root2);
            var t = dA / sB;
            var c = a + vA * t;
            result = (c - b).normalized;
            return true;
        }
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