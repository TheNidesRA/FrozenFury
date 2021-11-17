using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using TheKiwiCoder;

public class EnemyGoesToGoal : ActionNode
{
    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "EnemyGoesToGoal";
        context.agent.isStopped = false;
        context.enemy.targetPosition = EnemyGoal.instance.getPosition();
        context.enemy.actionTarget = EnemyGoal.instance.gameObject;
        context.agent.SetDestination(EnemyGoal.instance.getPosition());
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
       
        //Debug.Log("ENGA PAL COCHE");
      
       // Debug.Log("Estatus : " + context.agent.pathStatus);
        if(context.agent.hasPath)
        return State.Success;
        //Debug.Log("ENga pal cochesito");
   
        return State.Running;
    }
}