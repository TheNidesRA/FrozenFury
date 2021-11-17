using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BoomfingExplode : ActionNode
{
    public LayerMask targetsLayers;
    public Transform boomffinPrefabParts;

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
        Vector3 offsetPos = new Vector3(context.transform.position.x, context.transform.position.y + 20, context.transform.position.z);
        Transform explosionTransform=Instantiate(boomffinPrefabParts, offsetPos, context.transform.rotation);        
        foreach(Transform child in boomffinPrefabParts)
        {
            if(child.TryGetComponent<Rigidbody>(out Rigidbody childRb))
            {
                childRb.AddExplosionForce(500f, offsetPos, 8f);
            }
        }
        
        Destroy(context.gameObject);
        
        return State.Success;
    }
}