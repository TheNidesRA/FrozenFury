using TheKiwiCoder;
using UnityEngine;

public class BunnyBuffOn : ActionNode
{
    
    private bool cogido = false;
    private EnemyPascual _enemyPascual;
    protected override void OnStart()
    {
        if (!cogido)
        {
            _enemyPascual = context.gameObject.GetComponent<EnemyPascual>();
        }
       
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        _enemyPascual.boost.SetActive(true);
        return State.Success;
    }
}
