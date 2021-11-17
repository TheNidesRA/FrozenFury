using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

public class EnemyChasePlayer : ActionNode
{
    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "EnemyChasePlayer";
        

    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        context.enemy.actionTarget = PlayerStats._instance.gameObject;
        context.agent.SetDestination(context.enemy.actionTarget.transform.position);
        float distancia = Vector3.Distance(context.transform.position, context.agent.destination);
        if (distancia <= context.enemy.attackRange)
        {
            Debug.Log("Hemos llegao");
            return State.Success;
        }

       // Debug.Log("chasseando"+" Distansia: "+ distancia);
        // Vector3 targetPostition = new Vector3(context.enemy.actionTarget.transform.position.x,
        //     context.transform.position.y,
        //     context.enemy.actionTarget.transform.position.z);
        //context.transform.LookAt(targetPostition);
       // Debug.Log("De camino y tal");

        return State.Running;
        
    }
}