using TheKiwiCoder;
using UnityEngine;

public class BunnyBuffOff : ActionNode
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
//        Debug.Log("OFF");
        _enemyPascual.boost.SetActive(false);
        return State.Success;
    }
}
