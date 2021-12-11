using TheKiwiCoder;
using UnityEngine;

public class EnemySetPlayerAsTargetAndStartChase : ActionNode
{


    protected override void OnStart()
    {
        Debug.Log("SDas");
        context.enemy.actionTarget = PlayerStats._instance.gameObject;
        context.agent.SetDestination(context.enemy.actionTarget.transform.position);
        context.enemy.NODOACTUAL = "EnemySetPlayerAsTargetAndStartChase";
        context.enemy.speed = 40;
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}
