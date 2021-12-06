using Enemies;
using TheKiwiCoder;
using UnityEngine;

public class EnemyGoesToGoal : ActionNode
{

    public Sprite ActionImage;
    
    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "EnemyGoesToGoal";
        context.agent.isStopped = false;
        context.enemy.targetPosition = EnemyGoal.instance.getPosition();
        context.enemy.actionTarget = EnemyGoal.instance.gameObject;
        context.agent.SetDestination(EnemyGoal.instance.getPosition());
        context.animator.SetBool("Run", true);
        context.enemy.ActionImage.sprite = ActionImage;
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