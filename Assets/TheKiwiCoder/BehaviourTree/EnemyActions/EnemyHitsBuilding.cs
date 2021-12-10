using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

public class EnemyHitsBuilding : ActionNode
{
    public LayerMask buildingMask;
    public BuildingSO.BuildingType buildingType;
    //[SerializeField] private Sprite ActionImage;

    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "EnemiHitBuilding";
       // context.enemy.ActionImage.sprite = ActionImage;
        //context.animator.SetBool("Attack", true);
    }

    protected override void OnStop()
    {
       
    }
    
    
    
    

    protected override State OnUpdate()
    {
        if (context.enemy.actionTarget == null)
        {
            Debug.Log("Referencia null se ha ido a la pota");
            return State.Success;
        }
        
        Collider[] colliders =
            Physics.OverlapSphere(context.transform.position, context.enemy.attackRange, buildingMask);
        
        foreach (var VARIABLE in colliders)
        {
            if (VARIABLE.TryGetComponent<PlacedBuild>(out PlacedBuild placedBuild))
            {
                if (placedBuild.gameObject.Equals(context.enemy.actionTarget) )
                {
                
                    Debug.Log("Hit a : "+ placedBuild.BuildingSo.name);
               
                }
                
               // if (placedBuild.BuildingSo.type == BuildingSO.BuildingType.Turret)
                placedBuild.health -= context.enemy.damage;
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
