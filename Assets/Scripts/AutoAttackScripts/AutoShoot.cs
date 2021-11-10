using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace AutoAttackScripts
{
    /// <summary>
    /// Because we have different type of shoots, It's interesting to have an abstract shooting class from where
    /// the rest of the shooting classes will inherit. Right now, mostly everything is done in this abstract class
    /// because detecting the enemies is the same no matter which type of shoot you are making, but the method
    /// Shoot() is what will be changing in the other classes. This can be updated/change in the future.
    /// </summary>
    public class AutoShoot : MonoBehaviour
    {
        [Header("Choose type of shooting")] public ShootingStyle typeOfShooting = ShootingStyle.Normal;
        [Header("Time to destroy bullet")] public float bulletTimeAlive = 4.0f;

        /// <summary>
        /// This changes the velocity of the projectile
        /// </summary>
        [Header("This changes the velocity of the projectile")]
        public float shootForce;

        /// <summary>
        /// variable to store the distance between the player and the center of the collider which should be where
        /// the player is.
        /// </summary>
        private float localDistance = 0;

        private Vector3 vectorAb;

        private bool shooting = false;

        private bool isPlayer;

        private GameObject enemyToLook;

        private bool enemySighted = false;

        private Animator characterAnimator;

        #region AnimationVariables

        private bool[] quadrant;

        private bool LeftUp;
        private bool LeftDown;
        private bool RightUp;
        private bool RightDown;

        #endregion

        public bool Shooting
        {
            get => shooting;
            set => shooting = value;
        }

        public enum ShootingStyle
        {
            Normal,
            Burst,
            Shotgun,
        }

        /// <summary>
        /// Dictionary to store the enemies that are inside of the "Hit Area"
        /// </summary>
        private Dictionary<GameObject, float> _enemies;

        [Header("The GameObject which will be shot")]
        public GameObject bullet;

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

        [Header("Change the shotgun bullets spreed")]
        public int shotgunBulletSeparation = 40;

        /// <summary>
        /// The point from where you shoot.
        /// </summary>
        [Header("The point from where you shoot.")]
        public Transform attackPoint;

        [Header("Position of the player")] public Transform player;

        private Quaternion _objectiveDirection = Quaternion.identity;

        /// <summary>
        /// velocity to turn.
        /// </summary>
        [Header("Velocity to turn")] public float turnSpeed = 100.0f;

        /// <summary>
        /// Add the building information if this is assigned to a building
        /// </summary>
        [Header("Assign this only if this is a building")]
        public BuildingSO buildinginfo;

        private void Start()
        {
            _enemies = new Dictionary<GameObject, float>();
            StartCoroutine(nameof(AimEnemy));
            isPlayer = transform.parent.gameObject.CompareTag("Player");
            if (isPlayer)
                characterAnimator = GetComponentInParent<Animator>();
            quadrant = new bool[2];
        }


        private void OnCollisionExit(Collision other)
        {
            //You have to check whether your collision is with an enemy or with another object
            if (!other.gameObject.CompareTag($"Enemy")) return;
            _enemies.Remove(other.gameObject);
            enemySighted = false;

            if (isPlayer)
                //Enemy out of sight, trigger for animation blend tree
                characterAnimator.SetBool("EnemySighted", enemySighted);

            Debug.Log("Dejamos de detectar enemigo");
        }


        private void OnCollisionEnter(Collision other)
        {
            //Check what object is making the collision
            if (!other.gameObject.CompareTag($"Enemy")) return;
            _enemies.Add(other.gameObject, Vector3.Distance(other.gameObject.transform.position, transform.position));
            other.gameObject.GetComponent<Enemy>().OnEnemyDeath += RemoveEnemy;
            enemySighted = true;

            if (isPlayer)
                //Enemy detected, trigger for animation blend tree
                characterAnimator.SetBool("EnemySighted", enemySighted);
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
            //When there's enemy inside the shooting area, we'll rotate to the closest one
            RotatePlayerToEnemy(enemyToLook);
        }

        private IEnumerator AimEnemy()
        {
            while (true)
            {
                if (_enemies.Count == 0)
                {
                    nuevoPlayerMovement.controlMovimiento = true;
                }

                //we wait until there's enemies in the dictionary
                if (!(_enemies.Count > 0))
                {
                    yield return new WaitUntil(() => _enemies.Count > 0);
                    if (isPlayer)
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

                        //We retrieve the enemy the player will look to
                        enemyToLook = enemy.Key;
                        //We start shooting the enemy
                        ShootEnemy(enemy.Key);
                    }
                }

                //we wait timeBetweenShooting amount of time
                yield return new WaitForSeconds(timeBetweenShooting);
                Shooting = false;
            }
        }

        protected virtual void ShootEnemy(GameObject enemy)
        {
            if (enemy == null) return;
            switch (typeOfShooting)
            {
                case ShootingStyle.Normal:
                    ShootingNormal(enemy);
                    break;
                case ShootingStyle.Burst:
                    ShootingBurst(enemy);
                    break;
                case ShootingStyle.Shotgun:
                    ShootingShotgun(enemy);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("typeofShooting", "Invalid type of shooting");
            }
        }

        public virtual void RotatePlayerToEnemy(GameObject enemy)
        {
            if (enemy == null)
            {
                enemySighted = false;
                if (isPlayer)
                    characterAnimator.SetBool("EnemySighted", enemySighted);
                return;
            }

            if (isPlayer)
            {
                _objectiveDirection =
                    Quaternion.LookRotation((enemy.transform.position - player.transform.position).normalized);
                player.transform.rotation =
                    Quaternion.Slerp(player.transform.rotation, _objectiveDirection, Time.deltaTime * turnSpeed);
                vectorAb = enemy.transform.position - transform.position;
                CheckQuadrant(vectorAb.normalized);
                PlayerEnemyQuadrant(quadrant);
            }
            else
            {
                player.transform.LookAt(enemy.transform);
            }
        }

        private void RemoveEnemy(GameObject enemy)
        {
            _enemies.Remove(enemy);
            enemy.GetComponent<Enemy>().OnEnemyDeath -= RemoveEnemy;
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
            var position = attackPoint.position;
            Vector3 directionShoot = enemy.transform.position - position;

            //instantiate bullet
            GameObject currentBullet = Instantiate(bullet, position, Quaternion.identity);
            AssignShooterOfTheBullet(new List<GameObject>() { currentBullet });
            //add forces to bullet
            currentBullet.GetComponent<Rigidbody>().AddForce(directionShoot.normalized * shootForce, ForceMode.Impulse);
        }

        [ContextMenu("StopStartShooting")]
        public virtual void StartStopShooting()
        {
            if (IsShootingEnabled())
            {
                GetComponent<Collider>().enabled = false;
                if (isPlayer)
                    nuevoPlayerMovement.controlMovimiento = true;
                ClearEnemies();
            }
            else
            {
                GetComponent<Collider>().enabled = true;
            }
        }

        protected bool IsShootingEnabled()
        {
            var component = GetComponent<Collider>();
            return component.enabled;
        }

        protected void ClearEnemies()
        {
            _enemies.Clear();
        }

        private void ShootingNormal(GameObject enemy)
        {
            PlayerStats._instance.Damage = 3;
            //calculate direction from the attackpoint to the enemy
            var position = attackPoint.position;
            Vector3 directionShoot = enemy.transform.position - position;

            //instantiate bullet
            GameObject currentBullet = Instantiate(bullet, position, Quaternion.identity);
            AssignShooterOfTheBullet(new List<GameObject>() { currentBullet });
            Destroy(currentBullet, bulletTimeAlive);

            //add forces to bullet
            currentBullet.GetComponent<Rigidbody>().AddForce(directionShoot.normalized * shootForce, ForceMode.Impulse);
        }

        private void ShootingBurst(GameObject enemy)
        {
            PlayerStats._instance.Damage = 2;
            ShootBulletBurst(enemy);
            StartCoroutine(nameof(WaitToShootBulletBurst), enemy);
        }

        private void ShootingShotgun(GameObject enemy)
        {
            PlayerStats._instance.Damage = 5;
            var enemyPos = enemy.transform.position;
            var position = attackPoint.position;

            Vector3 straightShootDirection = enemyPos - position;
            Vector3 leftShootPropagation = (enemyPos - Vector3.forward * shotgunBulletSeparation) - position;
            Vector3 rightShootPropagation = (enemyPos + Vector3.forward * shotgunBulletSeparation) - position;

            //instantiate bullets
            var firstBullet = Instantiate(bullet, position, Quaternion.identity);
            var secondBullet = Instantiate(bullet, position, Quaternion.identity);
            var thirdBullet = Instantiate(bullet, position, Quaternion.identity);

            AssignShooterOfTheBullet(new List<GameObject>() { firstBullet, secondBullet, thirdBullet });
            Destroy(firstBullet, bulletTimeAlive);
            Destroy(secondBullet, bulletTimeAlive);
            Destroy(thirdBullet, bulletTimeAlive);

            //add forces to the bullets
            firstBullet.GetComponent<Rigidbody>()
                .AddForce(straightShootDirection.normalized * shootForce, ForceMode.Impulse);
            secondBullet.GetComponent<Rigidbody>()
                .AddForce(leftShootPropagation.normalized * shootForce, ForceMode.Impulse);
            thirdBullet.GetComponent<Rigidbody>()
                .AddForce(rightShootPropagation.normalized * shootForce, ForceMode.Impulse);
        }

        /// <summary>
        /// Assigns the shooter of the bullet to the bullet, if it's not the player who's shooting it will assign
        /// to the bullet that the shooter is a structure and the structure information
        /// </summary>
        /// <param name="bullets"></param>
        protected void AssignShooterOfTheBullet(List<GameObject> bullets)
        {
            if (isPlayer)
            {
                foreach (var bullet in bullets)
                {
                    bullet.GetComponent<BulletScript>().BulletFromPlayer = true;
                }
            }
            else
            {
                foreach (var bullet in bullets)
                {
                    bullet.GetComponent<BulletScript>().BuildingInfo = buildinginfo;
                    bullet.GetComponent<BulletScript>().BulletFromPlayer = false;
                }
            }
        }

        #region AnimationQuadrantsMethods

        private void CheckQuadrant(Vector3 vector)
        {
            if (vector.x > 0)
            {
                quadrant[0] = true;
            }
            else
            {
                quadrant[0] = false;
            }

            if (vector.z > 0)
            {
                quadrant[1] = true;
            }
            else

            {
                quadrant[1] = false;
            }
        }

        private void PlayerEnemyQuadrant(IReadOnlyList<bool> quadrants)
        {
            switch (quadrants[0])
            {
                case true when quadrants[1]:
                    LeftDown = true;
                    LeftUp = false;
                    RightDown = false;
                    RightUp = false;
                    break;
                case true when !quadrants[1]:
                    LeftDown = false;
                    LeftUp = true;
                    RightDown = false;
                    RightUp = false;
                    break;
                case false when quadrants[1]:
                    LeftDown = false;
                    LeftUp = false;
                    RightDown = true;
                    RightUp = false;
                    break;
                case false when !quadrants[1]:
                    LeftDown = false;
                    LeftUp = false;
                    RightDown = false;
                    RightUp = true;
                    break;
            }

            characterAnimator.SetBool("FirstQuad", LeftUp);
            characterAnimator.SetBool("SecondQuad", RightUp);
            characterAnimator.SetBool("ThirdQuad", LeftDown);
            characterAnimator.SetBool("FourthQuad", RightDown);

        }

        #endregion
    }
}