using TheKiwiCoder;
using UnityEngine;

public class BunnyIsPlayerAlive : ActionNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        Debug.Log("Se ejecuto IsPlayerAlive");
        return PlayerStats._instance.Health > 0 ? State.Success : State.Failure;
    }
}