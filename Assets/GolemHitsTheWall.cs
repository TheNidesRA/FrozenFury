using System.Collections;
using System.Collections.Generic;
using Enemies;
using GridSystem;
using UnityEngine;
using TheKiwiCoder;

public class GolemHitsTheWall : ActionNode
{
    private EnemyGolem _enemyGolem;
    
    protected override void OnStart()
    {
        _enemyGolem = context.gameObject.GetComponent<EnemyGolem>();
        Debug.Log(_enemyGolem.buildObjetive);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate()
    {
        Collider[] colliders = new  Collider[1];
        Physics.OverlapSphereNonAlloc(context.transform.position, 5f, colliders,LayerMask.GetMask("Muros"));
        if (colliders[0].TryGetComponent<PlacedBuild>(out PlacedBuild placedBuild))
        {
            if (placedBuild == _enemyGolem.buildObjetive)
            {
                Debug.Log("hit!!!!");
                GridBuildingSystem.Instance.RemoveBuild(placedBuild);
                return State.Success;
            }

            Debug.Log("MISS DE HIT :(");
            return State.Failure;
        }
        else
        {
            Debug.Log("MISS DE HIT :(");
            return State.Failure;
        }
        
        
        
    }
}
