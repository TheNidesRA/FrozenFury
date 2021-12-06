using TheKiwiCoder;
using UnityEditor;
using UnityEngine;


public class GolemFOVTurret : ActionNode
{
    public LayerMask targetMask;
    [SerializeField] private Sprite ActionImage;


    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "GolemFOVTurret";
        context.enemy.ActionImage.sprite = ActionImage;
        //   Debug.Log("AQUI MIRANDO ");
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        //  Debug.Log("Echando el ojito y tal");
        if (FieldOfViewCheck())
        {
            Debug.Log("Exitazo hay torretilla toca salir y tal");
            return State.Failure;
        }

        return State.Success;
    }

    private bool FieldOfViewCheck()
    {
        Collider[] rangeChecks =
            Physics.OverlapSphere(context.transform.position, context.enemy.radioVision, targetMask);

        if (rangeChecks.Length != 0)
        {
            float minDistance = float.MaxValue;
            GameObject target = null;
            foreach (var collider in rangeChecks)
            {
                Vector3 directionToTarget = (collider.transform.position - context.transform.position).normalized;
                float angulo = Vector3.Angle(context.transform.forward, directionToTarget);
                if (angulo < context.enemy.angleVision / 2)
                {
                    Debug.Log("Angulo : " + angulo);
                    float distance =
                        Vector3.Distance(context.transform.position, collider.gameObject.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        target = collider.gameObject;
                    }
                }
            }

            if (!ReferenceEquals(target, null))
            {
                context.enemy.actionTarget = target;
                return true;
            }

            // //  Handles.color = Color.black;
            // // Debug.DrawLine(context.transform.position, directionToTarget);
            //
            // Debug.Log(" Angulo :"+angulo);
            // if (angulo < context.enemy.angleVision/2)
            // {
            //     Debug.Log("Torretilla en vision");
            //
            //     context.enemy.actionTarget = target;
            //
            //     return true;
            // }
        }

        return false;
    }


    // private bool FieldOfViewCheck()
    // {
    //     Collider[] rangeChecks = Physics.OverlapSphere(context.transform.position, context.enemy.radioVision, targetMask);
    //
    //     if (rangeChecks.Length != 0)
    //     {
    //         Transform target = rangeChecks[0].transform;
    //         // Vector3 directionToTarget = (target.position - context.transform.position).normalized;
    //         Vector3 directionToTarget = (context.agent.destination - context.transform.position).normalized;
    //         if (Vector3.Angle(context.transform.forward, directionToTarget) < context.enemy.radioVision / 2)
    //         {
    //             Debug.Log("Torretilla en vision");
    //             //float distanceToTarget = Vector3.Distance(context.transform.position, target.position);
    //             context.enemy.actionTarget = rangeChecks[0].gameObject;
    //             // if (!Physics.Raycast(context.transform.position, directionToTarget, distanceToTarget,
    //             //     obstructionMask))
    //             //     canSeePlayer = true;
    //             // else
    //             //     canSeePlayer = false;
    //             return true;
    //         }
    //         // else
    //         //     canSeePlayer = false;
    //     }
    //
    //     return false;
    //     // else if (canSeePlayer)
    //     //     canSeePlayer = false;
    // }
}