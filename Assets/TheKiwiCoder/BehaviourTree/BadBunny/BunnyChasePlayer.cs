using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

public class BunnyChasePlayer : ActionNode
{
    private Transform player;
    protected override void OnStart()
    {
        player = PlayerStats._instance.gameObject.transform;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        context.agent.SetDestination(player.position);
        return State.Success;
    }
}
