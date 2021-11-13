using System.Collections;
using System.Collections.Generic;
using Enemies;
using GridSystem;
using UnityEngine;
using TheKiwiCoder;

public class GolemHitsTheWall : ActionNode
{
 

    protected override void OnStart()
    {
     
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (ReferenceEquals(context.enemy.actionTarget, null))
            return State.Success;
        //Debug.Log(name);
        Collider[] colliders =
            Physics.OverlapSphere(context.transform.position, 10f, LayerMask.GetMask("Muros"));
            
        foreach (var VARIABLE in colliders)
        {
            if (VARIABLE.TryGetComponent<PlacedBuild>(out PlacedBuild placedBuild))
            {
                 if (placedBuild.gameObject.Equals(context.enemy.actionTarget) )
                 {
                
                     Debug.Log("hit al muro bueno!!!!");
               
                 }
                 if(placedBuild.BuildingSo.type==BuildingSO.BuildingType.Wall)
                GridBuildingSystem.Instance.RemoveBuild(placedBuild);
                return State.Running;
                Debug.Log("MISS DE HIT :(");
                return State.Failure;
            }
        }

     //   Debug.Log("MISS DE HIT :(");
        return State.Success;
    }
}