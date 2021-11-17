using System.Collections;
using System.Collections.Generic;
using Enemies;
using TheKiwiCoder;
using UnityEngine;

public class BoomfingCheckGolemTarget : ActionNode
{
    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "BoomfingCheckGolemTarget";
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        Collider[] golems = Physics.OverlapSphere(context.gameObject.transform.position, 100f, LayerMask.GetMask("Enemy"));

        foreach (var golem in golems)
        {
            if (golem.TryGetComponent(out EnemyGolem enemyGolem))
            {
                if (enemyGolem.actionTarget.TryGetComponent(out PlacedBuild build))
                {
                    if (build != null)
                    {
                        context.enemy.actionTarget = build.gameObject;
                        Debug.Log("Vamos a ayudar al pana golem: "+ build.name);
                        return State.Success;
                    }
                }
            }
        }
        Debug.Log("Fracaso no hay golem con target");
        return State.Failure;
    }
}
