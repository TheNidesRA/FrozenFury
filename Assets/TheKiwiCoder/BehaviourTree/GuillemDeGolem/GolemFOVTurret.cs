using TheKiwiCoder;
using UnityEngine;


public class GolemFOVTurret : ActionNode
{
    public float radius;
    [Range(0, 360)] public float angle;
    public LayerMask targetMask;


    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (FieldOfViewCheck())
            return State.Failure;

        return State.Success;
    }

    private bool FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(context.transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            // Vector3 directionToTarget = (target.position - context.transform.position).normalized;
            Vector3 directionToTarget = (context.agent.destination - context.transform.position).normalized;
            if (Vector3.Angle(context.transform.forward, directionToTarget) < angle / 2)
            {
                Debug.Log("Torretilla en vision");
                //float distanceToTarget = Vector3.Distance(context.transform.position, target.position);
                context.enemy.actionTarget = rangeChecks[0].gameObject;
                // if (!Physics.Raycast(context.transform.position, directionToTarget, distanceToTarget,
                //     obstructionMask))
                //     canSeePlayer = true;
                // else
                //     canSeePlayer = false;
                return true;
            }
            // else
            //     canSeePlayer = false;
        }

        return false;
        // else if (canSeePlayer)
        //     canSeePlayer = false;
    }
}