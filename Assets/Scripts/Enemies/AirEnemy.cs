using System;
using UnityEngine;

namespace Enemies
{
    public class AirEnemy : Enemy
    {
        private void Start()
        {
            NavMeshAgent.destination = EnemyGoal.instance.getPosition();
        }
    }
}