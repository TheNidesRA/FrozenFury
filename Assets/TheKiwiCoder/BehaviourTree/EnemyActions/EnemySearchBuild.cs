using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using TheKiwiCoder;

public class EnemySearchBuild : ActionNode
{
    public LayerMask buildLayer;
    private PlacedBuild targetBuild;

    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "EnemySearchBuild";
        targetBuild = null;
        search();
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (targetBuild != null)
        {
            context.enemy.targetPosition = targetBuild.transform.position;
            context.enemy.actionTarget = targetBuild.gameObject;
            return State.Success;
        }
           
        return State.Failure;
    }

    private void search()
    {
        Vector3 direccion = (EnemyGoal.instance.getPosition() - context.transform.position).normalized;
        float distance = Vector3.Distance(context.transform.position, EnemyGoal.instance.getPosition());

//            Debug.Log("Lazando rallo");
        if (Physics.Raycast(context.gameObject.transform.position, direccion, out RaycastHit hitInfo, distance,
            buildLayer))
        {
            //   Debug.Log("Colisionamos o que?");
            if (hitInfo.collider.gameObject.TryGetComponent<PlacedBuild>(out PlacedBuild build))
            {
                //   Debug.Log("ENCONTRADO!!");
                targetBuild = build;
            }
        }
    }
}