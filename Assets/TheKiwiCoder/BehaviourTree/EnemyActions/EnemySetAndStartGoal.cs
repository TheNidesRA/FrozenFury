using Enemies;
using TheKiwiCoder;

public class EnemySetAndStartGoal : ActionNode
{
    protected override void OnStart()
    {
        context.enemy.actionTarget = EnemyGoal.instance.gameObject;
        context.agent.SetDestination(context.enemy.actionTarget.transform.position);
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}
