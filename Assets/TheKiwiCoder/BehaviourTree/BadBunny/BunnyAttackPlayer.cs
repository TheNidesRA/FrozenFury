using Enemies;
using TheKiwiCoder;
using UnityEngine;

public class BunnyAttackPlayer : ActionNode
{
    private Enemy _enemy;

    protected override void OnStart()
    {
        _enemy = context.gameObject.GetComponent<Enemy>();
        context.agent.isStopped = true;
        _enemy.Attack();
    }

    protected override void OnStop()
    {
        context.agent.isStopped = false;
    }

    protected override State OnUpdate()
    {
        return _enemy.IsAttackFinished() ? State.Success : State.Running;
    }
}