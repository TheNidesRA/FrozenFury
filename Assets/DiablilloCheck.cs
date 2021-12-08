using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class DiablilloCheck : ActionNode
{
    public LayerMask obstacles;
    protected override void OnStart()
    {
        context.enemy.actionTarget = PlayerStats._instance.gameObject;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        float distanceToTarget =
            Vector3.Distance(context.transform.position, context.enemy.actionTarget.transform.position);

        Vector3 directionToTarget =
            (context.enemy.actionTarget.transform.position - context.gameObject.transform.position).normalized;

        Debug.DrawRay(context.transform.position, directionToTarget * 1000f, Color.blue);

        if (Physics.Raycast(context.transform.position, directionToTarget, out RaycastHit info, distanceToTarget,
            obstacles))
        {
            if (info.collider.gameObject.TryGetComponent<PlacedBuild>(out PlacedBuild build))
            {
                // Debug.Log("Hay algo en medio");
                return State.Failure;
            }
        }
        // Debug.Log("No hay muro delante");
        return State.Success;
    }
}
