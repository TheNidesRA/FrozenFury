using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BoomfingExplode : ActionNode
{
    public LayerMask targetsLayers;

    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "BoomfingExplode";
        context.animator.SetBool("Attack", true);
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        Collider[] buildings = Physics.OverlapSphere(context.gameObject.transform.position, context.enemy.attackRange, targetsLayers);

        foreach (var buildCollider in buildings)
        {
            if (buildCollider.TryGetComponent(out PlacedBuild build))
            {
                
                Debug.Log(build.name+" Dañado");
                build.health -= context.enemy.damage;


            }
        }
        Destroy(context.gameObject);
        return State.Success;
    }
}