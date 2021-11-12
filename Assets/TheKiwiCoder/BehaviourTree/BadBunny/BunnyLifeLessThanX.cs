using Enemies;
using TheKiwiCoder;

public class BunnyLifeLessThanX : ActionNode
{
    private Enemy _enemy;
    private float _threshold;
    public float percentageLife = 0.2f;

    protected override void OnStart()
    {
        _enemy = context.agent.GetComponent<Enemy>();
        _threshold = _enemy.Health * percentageLife;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return _enemy.Health < _threshold ? State.Success : State.Failure;
    }
}