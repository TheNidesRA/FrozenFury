using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

public class EnemyHitsPlayer : ActionNode
{
    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "EnemyHitsPlayer";
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        // if (context.enemy.actionTarget == null)
        // {
        //     Debug.Log("Referencia null se ha ido a la pota");
        //     return State.Success;
        // }

        Collider[] colliders =
            Physics.OverlapSphere(context.transform.position, context.enemy.attackRange ,
                LayerMask.GetMask("Player"));

        if (colliders.Length > 0)
        {
            
            PlayerStats._instance.Health -= context.enemy.damage;
            Debug.Log(" Vida : " +  PlayerStats._instance.Health);
        }
        else
        {
            Debug.Log("Fracaso absoluto");
        }
      
        
      

        //   Debug.Log("MISS DE HIT :(");
        return State.Success;
    }
}