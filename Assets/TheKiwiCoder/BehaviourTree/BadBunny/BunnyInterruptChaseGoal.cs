using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

public class BunnyInterruptChaseGoal : ActionNode
{
    protected override void OnStart()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return PlayerStats._instance.Health > 0 ? State.Failure : State.Success;
    }
}