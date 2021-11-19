using System.Collections;
using System.Collections.Generic;
using Enemies;
using TheKiwiCoder;
using UnityEngine;

public class EnemyFOV : ActionNode
{
    public LayerMask targetMask;

    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "EnemyFOV";
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        bool avistado = FieldOfViewCheck();

        // Debug.Log(avistado);
        if (!avistado)
        {
            Debug.Log("Exitazo Player Detected");
            return State.Failure;
        }

        Debug.Log("Exitazo Player Detected");
        return State.Success;
    }


    private bool FieldOfViewCheck()
    {
        Collider[] rangeChecks =
            Physics.OverlapSphere(context.transform.position, context.enemy.radioVision, targetMask);

        if (rangeChecks.Length != 0)
        {
            Vector3 directionToTarget =
                (PlayerStats._instance.transform.position - context.transform.position).normalized;
            float angulo = Vector3.Angle(context.transform.forward, directionToTarget);
//            Debug.Log(angulo);

            if (angulo < context.enemy.angleVision / 2)
            {
                //  Debug.Log("Torretilla en vision");

                context.enemy.actionTarget = rangeChecks[0].gameObject;

                return true;
            }
        }

        return false;
    }
}