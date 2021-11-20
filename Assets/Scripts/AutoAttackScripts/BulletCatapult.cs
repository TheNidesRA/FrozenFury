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

            //CODIGO DE VFX DE LA EXPLOSIÓN
            /*GameObject explosion = Instantiate(_vfxExplode, transform.position, Quaternion.identity);
            explosion.transform.parent = this.transform;*/

            foreach (Collider hit in colliders)
            {

                if (hit.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    if (enemy.OnHit(BuildingInfo.damage))
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
