using TheKiwiCoder;
using UnityEngine;

public class GolemCheckIFWallFrontTurret : ActionNode
{
    public LayerMask targetMask;

    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "GolemCheckIfWallFrontTurret";
        Debug.Log("Checkeo si hay muritos delante");
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        float distanceToTarget = Vector3.Distance(context.transform.position, context.enemy.targetPosition);
        
        Vector3 directionToTarget = (context.gameObject.transform.position - context.enemy.targetPosition).normalized;
        
        if (Physics.Raycast(context.transform.position, directionToTarget,out RaycastHit info,distanceToTarget,targetMask))
        {
            if (info.collider.gameObject.TryGetComponent<PlacedBuild>(out PlacedBuild build))
            {
                context.enemy.auxActionTarget = context.enemy.actionTarget;
                context.enemy.actionTarget = build.gameObject;
                Debug.Log("Hay muro delante");
                return State.Success;
            }
        }
        Debug.Log("No hay muro delante");
        return State.Failure;
    }
}