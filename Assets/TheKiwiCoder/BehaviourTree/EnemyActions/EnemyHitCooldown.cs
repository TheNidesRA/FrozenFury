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
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (Time.time - startTime > duration) {
            return State.Success;
        }
        return State.Running;
    }
}
