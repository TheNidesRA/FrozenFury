using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

public class EnemyHitCooldown : ActionNode
{
    private float duration;
    float startTime;
    [SerializeField] private Sprite ActionImage;

    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "EnemyHitCooldown";
        duration = 1 / context.enemy.attackSpeed;
        startTime = Time.time;
        context.animator.SetBool("Attack", true);
        context.animator.SetFloat("SpeedMult", context.enemy.attackSpeed);
        context.enemy.ActionImage.sprite = ActionImage;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (Time.time - startTime > duration)
        {
//            Debug.Log("Trigger attack false");
            context.animator.SetBool("Attack", false);
            return State.Success;
        }

        return State.Running;
    }
}