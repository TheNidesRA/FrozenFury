using System.Collections;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;
using TheKiwiCoder;

public class GolemHitTheTurret : ActionNode
{
    protected override void OnStart() {
        context.enemy.NODOACTUAL = "GolemHitTheTurret";
        Debug.Log("Vamos a darle tremendo putiaso a la torretilla");
    }

    protected override void OnStop() {
        
    }

    protected override State OnUpdate() {
        if (ReferenceEquals(context.enemy.actionTarget, null))
        {
            Debug.Log("Referencia null se ha ido a la pota");
            return State.Success;
        }
            
        //Debug.Log(name);
        Collider[] colliders =
            Physics.OverlapSphere(context.transform.position, 10f, LayerMask.GetMask("Torreta"));
            
        foreach (var VARIABLE in colliders)
        {
            if (VARIABLE.TryGetComponent<PlacedBuild>(out PlacedBuild placedBuild))
            {
                if (placedBuild.gameObject.Equals(context.enemy.actionTarget) )
                {
                
                    Debug.Log("hit a la torreta buena!!!!");
               
                }
                if(placedBuild.BuildingSo.type==BuildingSO.BuildingType.Turret)
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
