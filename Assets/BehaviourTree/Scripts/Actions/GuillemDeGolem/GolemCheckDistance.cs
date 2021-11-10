using System.Collections;
using System.Collections.Generic;
using Enemies;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;

public class GolemCheckDistance : ActionNode
{
    private EnemyGolem _enemyGolem;
    public float threasole = 2f;

    protected override void OnStart()
    {
        
        _enemyGolem = context.gameObject.GetComponent<EnemyGolem>();
        //throw new System.NotImplementedException();
    }

    protected override void OnStop()
    {
        // throw new System.NotImplementedException();
    }

    protected override State OnUpdate()
    {
        Debug.Log(name);
        Debug.Log("aaa?");
        if (context.agent.hasPath)
        {
            if (context.agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                float realDistance = EnemyGolem.GetPathRemainingDistance(context.agent);
                float manDistance = Vector3.Distance(context.agent.destination, context.transform.position);
                float difference = realDistance - manDistance;

                if (difference >= threasole)
                {
                    Debug.Log("A reventar se ha dixo");
                    return State.Success;
                }
                else
                {
                    Debug.Log("No renta");
                    return State.Failure;
                }
            }

           Debug.Log("No es completo");
            return State.Failure;
        }
        else
        {
            Debug.Log("No tiene path");
            return State.Failure;
        }
    }
}