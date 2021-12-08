using System;
using UnityEngine;
using UnityEngine.AI;


namespace Enemies
{
    public class EnemyGolem : Enemy
    {
        private NavMeshPathStatus s;
        


        private void Start()
        {
            NavMeshAgent.updateRotation = true;
            // targetPosition=EnemyGoal.instance.transform.position;
            // actionTarget = EnemyGoal.instance.gameObject;
            // NavMeshAgent.destination = EnemyGoal.instance.transform.position;
            timeWaitAnim = 1.10f;
        }

        private void Update()
        {
            s = this.NavMeshAgent.pathStatus;
        }
    }
}