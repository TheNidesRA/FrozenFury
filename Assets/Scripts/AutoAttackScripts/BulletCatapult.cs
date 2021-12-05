using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace AutoAttackScripts
{
    public class BulletCatapult : BulletScript
    {
        public float _radius = 10;


        public override void OnCollisionEnter(Collision other)
        {
            Debug.Log(other.collider.gameObject.name);
            Debug.Log("Buenas");
            var explosionPos = transform.position;
            var colliders = Physics.OverlapSphere(explosionPos, _radius);

            foreach (var hit in colliders)
            {
                if (!hit.gameObject.TryGetComponent<Enemy>(out Enemy enemy)) continue;
                if (!enemy.OnHit(bulletDamage)) continue;
                PlayerSkillCalculator.Instance.UpdateRoundKills();
                enemy.Die();
            }


            Destroy(gameObject);
        }
    }
}