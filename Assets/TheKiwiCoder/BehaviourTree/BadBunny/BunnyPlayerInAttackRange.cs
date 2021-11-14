using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

public class BunnyPlayerInAttackRange : ActionNode
{
    public float threshold = 10.0f;
    private Transform player;

    protected override void OnStart()
    {
        player = PlayerStats._instance.transform;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return Vector3.Distance(context.agent.transform.position, player.position) < threshold
            ? State.Success
            : State.Failure;
    }
}