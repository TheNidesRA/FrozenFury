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
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        Collider[] buildings = Physics.OverlapSphere(context.gameObject.transform.position, context.enemy.AttackRange, targetsLayers);

        foreach (var buildCollider in buildings)
        {
            if (buildCollider.TryGetComponent(out PlacedBuild build))
            {
                
                Debug.Log(build.name+" Dañado");
                build.health -= context.enemy.Damage;


            }
        }
        Destroy(context.gameObject);
        return State.Success;
    }
}