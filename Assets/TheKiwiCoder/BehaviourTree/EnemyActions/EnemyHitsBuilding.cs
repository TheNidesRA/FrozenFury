using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

public class EnemyHitsBuilding : ActionNode
{
    public LayerMask buildingMask;
    public BuildingSO.BuildingType buildingType;

    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "EnemiHitBuilding";
    }

    protected override void OnStop()
    {
       
    }
    
    
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(context.transform.position, context.enemy.AttackRange*2);
    }
    

    protected override State OnUpdate()
    {
        if (context.enemy.actionTarget == null)
        {
            Debug.Log("Referencia null se ha ido a la pota");
            return State.Success;
        }
        
        Collider[] colliders =
            Physics.OverlapSphere(context.transform.position, context.enemy.AttackRange*2, buildingMask);
        
        foreach (var VARIABLE in colliders)
        {
            if (VARIABLE.TryGetComponent<PlacedBuild>(out PlacedBuild placedBuild))
            {
                if (placedBuild.gameObject.Equals(context.enemy.actionTarget) )
                {
                
                    Debug.Log("Hit a : "+ placedBuild.BuildingSo.name);
               
                }
                
               // if (placedBuild.BuildingSo.type == BuildingSO.BuildingType.Turret)
                placedBuild.health -= context.enemy.Damage;
                Debug.Log(placedBuild.BuildingSo.name+" Vida : "+ placedBuild.health);
                //return State.Running;
                //Debug.Log("MISS DE HIT :(");
                //return State.Failure;
            }
        }

        //   Debug.Log("MISS DE HIT :(");
        return State.Success;
        
    }
}
