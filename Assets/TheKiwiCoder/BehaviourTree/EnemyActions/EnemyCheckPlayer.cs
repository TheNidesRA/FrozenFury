using Enemies;
using TheKiwiCoder;
using UnityEngine;


public class EnemyCheckPlayer : ActionNode
{
    protected override void OnStart()
    {
    
        context.enemy.NODOACTUAL = "EnemyGoesToGoal";


    }

    protected override void OnStop()
    {
        //throw new System.NotImplementedException();
    }

    protected override State OnUpdate()
    {
//        Debug.Log(PlayerStats._instance != null);
        return PlayerStats._instance != null ? State.Success : State.Failure;
    }
}