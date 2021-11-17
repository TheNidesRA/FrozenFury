using Enemies;
using TheKiwiCoder;
using UnityEngine;

public class EnemySetAndStartGoal : ActionNode
{
    protected override void OnStart()
    {
        context.enemy.actionTarget = EnemyGoal.instance.gameObject;
        context.agent.SetDestination(context.enemy.actionTarget.transform.position);
        context.agent.isStopped = false;
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        Debug.Log("Via libre a x la salida");
        return State.Success;
    }
}
