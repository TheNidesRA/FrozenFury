using TheKiwiCoder;
using UnityEngine;

public class BunnyExistPlayer : ActionNode
{
    PlayerStats player;

    protected override void OnStart()
    {
        player = PlayerStats._instance;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        //not using player != null because that is expensive in Unity
        Debug.Log("Esto se ejecuta");
        return !ReferenceEquals(player, null) ? State.Success : State.Failure;
    }
}