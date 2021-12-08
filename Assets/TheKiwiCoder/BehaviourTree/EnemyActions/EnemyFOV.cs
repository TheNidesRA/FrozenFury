using System.Collections;
using System.Collections.Generic;
using Enemies;
using TheKiwiCoder;
using UnityEngine;

public class EnemyFOV : ActionNode
{
    public LayerMask targetMask;
    [SerializeField] private Sprite ActionImage;

    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "EnemyFOV";
        context.enemy.ActionImage.sprite = ActionImage;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        bool avistado = FieldOfViewCheck();

        // Debug.Log(avistado);
        return !avistado ? State.Failure : State.Success;
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
            //Debug.Log(angulo);
            //Debug.Log("Almenos hay algo ");
            if (angulo < context.enemy.angleVision / 2)
            {
                context.enemy.actionTarget = rangeChecks[0].gameObject;

                return true;
            }
        }

        return false;
    }
}