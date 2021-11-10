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
       // Debug.Log(_enemyGolem.buildObjetive);
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        Debug.Log(name);
        Collider[] colliders =
            Physics.OverlapSphere(context.transform.position, 10f, LayerMask.GetMask("Muros"));

        foreach (var VARIABLE in colliders)
        {
            if (VARIABLE.TryGetComponent<PlacedBuild>(out PlacedBuild placedBuild))
            {
                // if (placedBuild.Equals(_enemyGolem.buildObjetive) )
                // {
               // Debug.Log("hit!!!!");
                GridBuildingSystem.Instance.RemoveBuild(placedBuild);
                return State.Success;
                // }

                Debug.Log("MISS DE HIT :(");
                return State.Failure;
            }
        }

     //   Debug.Log("MISS DE HIT :(");
        return State.Failure;
    }
}