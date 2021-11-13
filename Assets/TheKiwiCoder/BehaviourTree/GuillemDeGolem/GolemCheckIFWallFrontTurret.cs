using TheKiwiCoder;
using UnityEngine;

public class GolemCheckIFWallFrontTurret : ActionNode
{
    public LayerMask targetMask;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        float distanceToTarget = Vector3.Distance(context.transform.position, context.enemy.targetPosition);
        
        Vector3 directionToTarget = (context.gameObject.transform.position - context.enemy.targetPosition).normalized;
        
        if (!Physics.Raycast(context.transform.position, directionToTarget,out RaycastHit info,distanceToTarget,targetMask))
        {
            if (info.collider.gameObject.TryGetComponent<PlacedBuild>(out PlacedBuild build))
            {
                context.enemy.auxActionTarget = context.enemy.actionTarget;
                context.enemy.actionTarget = build.gameObject;
                return State.Success;
            }
        }

        return State.Failure;
    }
}