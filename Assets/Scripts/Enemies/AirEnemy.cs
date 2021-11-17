using System;
using UnityEngine;

namespace Enemies
{
    public class AirEnemy : Enemy
    {
        private void Start()
        {
            NavMeshAgent.SetDestination(EnemyGoal.instance.getPosition());
        }
    }
}