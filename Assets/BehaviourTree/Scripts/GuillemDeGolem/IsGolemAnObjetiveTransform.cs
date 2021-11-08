using System.Collections;
using System.Collections.Generic;
using Enemies;
using Nodes;
using TheKiwiCoder;
using UnityEngine;

public class IsGolemAnObjetiveTransform : ActionNode
{
    [SerializeField] private Transform _objetive;

    protected override void OnStart()
    {
        _objetive = EnemyGoal.instance?.transform;
    }

    protected override void OnStop()
    {
       // throw new System.NotImplementedException();
    }

    protected override State OnUpdate()
    {
        return (_objetive == null) ? State.Failure : State.Success;
    }
}