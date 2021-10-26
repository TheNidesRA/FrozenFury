using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace AutoAttackScripts
{
    /// <summary>
    /// Because we have different type of shoots, It's interesting to have an abstract shooting class from where
    /// the rest of the shooting classes will inherit. Right now, mostly everything is done in this abstract class
    /// because detecting the enemies is the same no matter which type of shoot you are making, but the method
    /// Shoot() is what will be changing in the other classes. This can be updated/change in the future.
    /// </summary>
    public abstract class AutoShoot : MonoBehaviour
    {
        /// <summary>
        /// variable to store the distance between the player and the center of the collider which should be where
        /// the player is.
        /// </summary>
        [SerializeField] private float localDistance = 0;

        [SerializeField] private bool shooting = false;

        public bool Shooting
        {
            get => shooting;
            set => shooting = value;
        }

        /// <summary>
        /// Dictionary to store the enemies that are inside of the "Hit Area"
        /// </summary>
        private Dictionary<GameObject, float> _enemies;

        [Header("The GameObject which will be shot")]
        public GameObject bullet;

        /// <summary>
        /// This changes the velocity of the projectile
        /// </summary>
        [Header("This changes the velocity of the projectile")]
        public float shootForce;

        /// <summary>
        /// Time to wait until the Shoot() method is executed
        /// </summary>
        [Header("Time to wait until the Shoot() method is executed")]
        public float timeBetweenShooting = 0.5f;

        /// <summary>
        /// Time between you shoot the second bullet of the Shoot() method, this is only used in AutoTwoShoots.
        /// </summary>
        [Header("Time between you shoot the second bullet of the Shoot() method, this is only used in AutoTwoShoots")]
        public float timeToShootBulletBurst = 0.2f;

        /// <summary>
        /// The point from where you shoot.
        /// </summary>
        [Header("The point from where you shoot.")]
        public Transform attackPoint;

        [Header("Position of the player")] public Transform player;

        [SerializeField] private Quaternion _objectiveDirection = Quaternion.identity;

        /// <summary>
        /// velocity to turn.
        /// </summary>
        [Header("Velocity to turn")] public float turnSpeed = 100.0f;

        /// <summary>
        /// Damage effect when hit, if not changed it will be 1 by default
        /// </summary>
        [SerializeField] protected float damage = 1;

        public abstract float Damage { set; get; }

        // Start is called before the first frame update
        private void Start()
        {
            _enemies = new Dictionary<GameObject, float>();
            StartCoroutine(nameof(AimEnemy));
        }


        private void OnCollisionExit(Collision other)
        {
            //You have to check whether your collision is with an enemy or with another object
            if (!other.gameObject.CompareTag($"Enemy")) return;
            _enemies.Remove(other.gameObject);
        }


        private void OnCollisionEnter(Collision other)
        {
            //Check what object is making the collision
            if (!other.gameObject.CompareTag($"Enemy")) return;
            _enemies.Add(other.gameObject, Vector3.Distance(other.gameObject.transform.position, transform.position));
        }

        //This method will be used to update the dictionary of enemies
        private void OnCollisionStay(Collision other)
        {
            //check if the collision is made with an enemy
            if (!other.gameObject.CompareTag($"Enemy")) return;

            //calculate the distance between the enemy and the position from where the player shoots
            localDistance = Vector3.Distance(other.gameObject.transform.position, transform.position);

            //assign the distance to the enemy
            _enemies[other.gameObject] = localDistance;
        }

        private IEnumerator AimEnemy()
        {
            while (true)
            {
              
                //we wait until there's enemies in the dictionary
                if (!(_enemies.Count > 0))
                {   
                    yield return new WaitUntil(() => _enemies.Count > 0);
                    nuevoPlayerMovement.controlMovimiento = false;
                }
                //we retrieve the minimum distance of the dictionary
                var minDistance = _enemies.Min(distance => distance.Value);

                foreach (var enemy in _enemies)
                {
                    //when searching for equals using floats, there could be little difference (1.444444 != 1.444445), that's
                    //why whe check if the subtraction is less than 0.1 -> Abs(1.444444 - 1.444445) = 0.000001 and that's true
                    if (Math.Abs(enemy.Value - minDistance) < 0.001f)
                    {
                        if (Shooting) continue;
                        Shooting = true;
                        // Debug.Log("Shooting " + enemy.Key);

                        //We rotate the player to face the enemy
                        RotatePlayerToEnemy(enemy.Key);
                        //We start shooting the enemy
                        ShootEnemy(enemy.Key);
                    }
                }

                //we wait timeBetweenShooting amount of time
                yield return new WaitForSeconds(timeBetweenShooting);
                Shooting = false;
            }
        }

        protected abstract void ShootEnemy(GameObject enemy);

        public virtual void RotatePlayerToEnemy(GameObject enemy)
        {
            if (enemy == null)
            {
                nuevoPlayerMovement.controlMovimiento = true;
                return;
            }
            _objectiveDirection =
                Quaternion.LookRotation((enemy.transform.position - player.transform.position).normalized);
            player.transform.rotation =
                Quaternion.Slerp(player.transform.rotation, _objectiveDirection, Time.deltaTime * turnSpeed);
        }

        public void RemoveEnemy(GameObject enemy)
        {
            _enemies.Remove(enemy);
        }

        protected IEnumerator WaitToShootBulletBurst(GameObject enemy)
        {
            yield return new WaitForSeconds(timeToShootBulletBurst);
            ShootBulletBurst(enemy);
            yield return new WaitForSeconds(timeToShootBulletBurst);
            ShootBulletBurst(enemy);
        }


        private void ShootBulletBurst(GameObject enemy)
        {
            if (enemy == null) return;
            //calculate direction from the attackpoint to the enemy
            Vector3 directionShoot = enemy.transform.position - attackPoint.position;

            //instantiate bullet
            GameObject secondBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

            //add forces to bullet
            secondBullet.GetComponent<Rigidbody>().AddForce(directionShoot.normalized * shootForce, ForceMode.Impulse);
        }
    }
}