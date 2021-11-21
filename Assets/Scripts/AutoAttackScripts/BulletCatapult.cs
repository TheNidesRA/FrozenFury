using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace AutoAttackScripts
{
    public class BulletCatapult : BulletScript
    {

        public float _radius = 10;


        override
        public void OnCollisionEnter(Collision other)
        {
            Vector3 explosionPos = this.transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, _radius);

            foreach (Collider hit in colliders)
            {

                if (hit.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    if (enemy.OnHit(bulletDamage))
                    {
                        PlayerSkillCalculator.Instance.UpdateRoundKills();
                        enemy.Die();
                    }

                }
            }

          

            Destroy(gameObject);
        }


    }
}
