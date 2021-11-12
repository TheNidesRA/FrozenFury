using System.Collections;
using System.Collections.Generic;
using Enemies;
using TheKiwiCoder;
using UnityEngine;

public class BunnyChaseGoal : ActionNode
{
    private Vector3 _objective;

    protected override void OnStart()
    {
        _objective = EnemyGoal.instance.getPosition();
        context.agent.SetDestination(EnemyGoal.instance.transform.position); 
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}