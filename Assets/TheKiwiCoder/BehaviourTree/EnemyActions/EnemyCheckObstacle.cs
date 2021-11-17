using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

public class EnemyCheckObstacle : ActionNode
{
    public LayerMask obstacles;

    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "EnemyCheckObstacle";
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        float distanceToTarget = Vector3.Distance(context.transform.position, context.enemy.targetPosition);
        
        Vector3 directionToTarget = (context.gameObject.transform.position - context.enemy.targetPosition).normalized;
        
        if (Physics.Raycast(context.transform.position, directionToTarget,out RaycastHit info,distanceToTarget,obstacles))
        {
            if (info.collider.gameObject.TryGetComponent<PlacedBuild>(out PlacedBuild build))
            {
               Debug.Log("Hay algo en medio");
                return State.Failure;
            }
        }
        Debug.Log("No hay muro delante");
        return State.Success;
    }
}
