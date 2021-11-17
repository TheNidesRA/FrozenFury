using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTargetCompleted : ActionNode
{
 
    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "EnemyTargetCompleted";
    }

    protected override void OnStop()
    {
        // if (state == State.Failure)
        // {
        //     Debug.Log("fALLIDO REINICIAMOS TARGET Y TAL");
        //     context.agent.ResetPath();
        //     context.enemy.actionTarget = null;
        // }
     
    }

    protected override State OnUpdate()
    {
        if ( context.agent.pathStatus==NavMeshPathStatus.PathComplete)
        {
          // Debug.Log("Perfecto");
            return State.Success;
        }
        // Debug.Log("No es completo");
        return State.Failure;
    }
}
