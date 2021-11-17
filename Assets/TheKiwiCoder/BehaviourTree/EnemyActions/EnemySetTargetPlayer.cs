using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

public class EnemySetTargetPlayer : ActionNode
{
    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "EnemySetTargetPlayer";
        context.agent.isStopped = false;
     
        context.enemy.actionTarget = PlayerStats._instance.gameObject;
        context.agent.SetDestination(PlayerStats._instance.transform.position);
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        // Debug.Log("Estatus : " + context.agent.pathStatus);
        if(context.agent.hasPath)
            return State.Success;
        //Debug.Log("ENga pal cochesito");
        return State.Running;
    }
}
