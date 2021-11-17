using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

public class EnemyHitCooldown : ActionNode
{
    private float duration;
    float startTime;

    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "EnemyHitCooldown";
        duration = 1/context.enemy.attackSpeed;
        startTime = Time.time;
        Debug.Log("Hay que esperar : "+ duration);
        context.animator.SetBool("Attack", true);
        context.animator.SetFloat("SpeedMult", context.enemy.attackSpeed);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (Time.time - startTime > duration) {
//            Debug.Log("Trigger attack false");
            context.animator.SetBool("Attack", false);
            return State.Success;
        }
        return State.Running;
    }
}
