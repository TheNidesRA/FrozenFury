using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDiablillo : Enemy
{
    public NavMeshPathStatus s;

    private void Update()
    {
        s = NavMeshAgent.pathStatus;
    }
}
