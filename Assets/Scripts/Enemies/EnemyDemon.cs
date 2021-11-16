using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemyDemon : Enemy
    {
        [SerializeField]
        public float _triggerDistance;

        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = gameObject.GetComponent<NavMeshAgent>();
        }
        
        public void Attack(Transform target)
        {
            _agent.isStopped = true;
            // StartCoroutine( "EnterAttackMode");
        }

        // private IEnumerator EnterAttackMode()
        // {
        //     // Debug.Log("EnemyDemon ha atacado al señor heladero.");
        //     // if (!PlayerStats._instance.OnHit(Damage))
        //     // {
        //     //     StopAttack();
        //     //     StopCoroutine("EnterAttackMode");
        //     // }
        //     // yield return new WaitForSeconds(AtackSpeed);
        // }

        public void StopAttack()
        {
            // StopCoroutine("EnterAttackMode");
            _agent.isStopped = false;
        }
    }
}