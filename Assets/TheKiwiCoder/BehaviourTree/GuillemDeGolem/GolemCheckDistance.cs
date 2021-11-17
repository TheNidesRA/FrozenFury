using System.Collections;
using System.Collections.Generic;
using Enemies;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;

public class GolemCheckDistance : ActionNode
{

    public float threasole = 2f;

    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "GolemCheckDistance";
        
        //throw new System.NotImplementedException();
    }

    protected override void OnStop()
    {
        // throw new System.NotImplementedException();
    }

    protected override State OnUpdate()
    {
        // Debug.Log(name);
        //  Debug.Log("aaa?");
        if (context.agent.hasPath)
        {
            // Debug.Log("Cositas");
            if (context.agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                float realDistance = Enemy.GetPathRemainingDistance(context.agent);
                float manDistance = Vector3.Distance(context.agent.destination, context.transform.position);
                float difference = realDistance - manDistance;

                if (difference >= threasole)
                {
                     // Debug.Log("A reventar se ha dixo");
                    return State.Success;
                }

                // Debug.Log("No renta");
                return State.Failure;
            }

            if (context.enemy.actionTarget == EnemyGoal.instance.gameObject && context.agent.pathStatus==NavMeshPathStatus.PathPartial)
            {
                // Debug.Log("No es completo");
                return State.Success;
            }
            // Debug.Log("No es pero tampoco es al objetivo que esta pasando ??");
            return State.Failure;
           
        }

         // Debug.Log("No tiene path");
        return State.Failure;
    }
}