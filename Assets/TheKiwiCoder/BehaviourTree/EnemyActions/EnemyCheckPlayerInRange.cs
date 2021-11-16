using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class EnemyCheckPlayerInRange : ActionNode
{
    
    
    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "EnemyCheckPlayerInRange";
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        Debug.Log((Vector3.Distance(context.agent.transform.position, PlayerStats._instance.transform.position) < context.enemy.AttackRange));
        return Vector3.Distance(context.agent.transform.position, PlayerStats._instance.transform.position) < context.enemy.AttackRange
            ? State.Success
            : State.Failure;
    }
}
