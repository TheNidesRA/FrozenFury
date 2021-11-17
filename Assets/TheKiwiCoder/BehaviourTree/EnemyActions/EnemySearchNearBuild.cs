using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using TheKiwiCoder;

public class EnemySearchNearBuild : ActionNode
{
    private PlacedBuild nearestWall;
    public LayerMask buildingLayers;
    protected override void OnStart() {
        context.enemy.NODOACTUAL = "EnemySearchNearBuild";
       

        nearestWall = null;
        
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        //Debug.Log(name);
        searchWalls();

        if (!ReferenceEquals(nearestWall, null))
        {
            context.enemy.targetPosition = nearestWall.transform.position;
            context.enemy.actionTarget = nearestWall.gameObject;
            Debug.Log("Encontrado");
            return State.Success;
        }
        Debug.Log("Fracaso");
        return State.Failure;
    }
    
    
    private void searchWalls()
    {
        

        Vector3 direccion = (EnemyGoal.instance.getPosition()-context.transform.position ).normalized;
        float distance = Vector3.Distance(context.transform.position, EnemyGoal.instance.getPosition());
            
//            Debug.Log("Lazando rallo");
        if (Physics.Raycast(context.gameObject.transform.position, direccion, out RaycastHit hitInfo, distance,
            buildingLayers))
        {
            //   Debug.Log("Colisionamos o que?");
            if (hitInfo.collider.gameObject.TryGetComponent<PlacedBuild>(out PlacedBuild build))
            {
                //   Debug.Log("ENCONTRADO!!");
                nearestWall = build;
            }
        }
    }
    
    
}
