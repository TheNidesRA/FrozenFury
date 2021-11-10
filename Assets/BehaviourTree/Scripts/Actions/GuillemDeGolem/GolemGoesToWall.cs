using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using TheKiwiCoder;

public class GolemGoesToWall : ActionNode
{
    private EnemyGolem _enemyGolem;
    protected override void OnStart()
    {

        _enemyGolem = context.gameObject.GetComponent<EnemyGolem>();
        
        Debug.Log(_enemyGolem.objetive);
        
        context.agent.SetDestination(_enemyGolem.objetive);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        if (Vector3.Distance(context.transform.position, context.agent.destination) < 1f)
        {
            Debug.Log("Hemos llegao");
            return State.Success;
        }
        
        
        return State.Running;
    }
}
