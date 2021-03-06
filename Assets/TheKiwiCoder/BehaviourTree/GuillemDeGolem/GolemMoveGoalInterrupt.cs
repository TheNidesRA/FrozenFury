

using Enemies;
using UnityEngine;
using TheKiwiCoder;

public class GolemMoveGoalInterrupt : ActionNode
{
    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "GolemMoveGoalInterrupt";
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        // Debug.Log(name);
        return Check();
    }


    private State Check()
    {
        Debug.Log("Viendo si hay que abortar");
        float distance = Vector3.Distance(context.agent.destination, context.transform.position);
        float diferencia = EnemyGolem.GetPathRemainingDistance(context.agent) - distance;

        if (diferencia >= 10)
        {
            Debug.Log("HAY QUE RECALCULAR");
            return State.Failure;
        }

        return State.Success;
    }
}
