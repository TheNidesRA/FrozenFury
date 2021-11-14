using System.Collections;
using System.Collections.Generic;
using Enemies;
using Nodes;
using TheKiwiCoder;
using UnityEngine;

public class IsGolemAnObjetiveTransform : ActionNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        
        if (!ReferenceEquals(EnemyGoal.instance,null))
        {
            Debug.Log("Existo");
//            context.agent.destination = EnemyGoal.instance.getPosition();
            return State.Success;
        }
        else
        {
            Debug.Log("fasllo");
            return State.Failure;
        }
    }
}