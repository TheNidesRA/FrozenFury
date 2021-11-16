using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class GolemGoesToWall : ActionNode
{
    private bool fracaso = false;
    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "GolemGoesToWall";
        //LeanTween.rotate(context.gameObject,new Vector3(_enemyGolem.objetive.x,0,_enemyGolem.objetive.z),2f);

        //Debug.Log(_enemyGolem.objetive);

        Debug.Log("Vamos a por el murito y tal");
        if (context.enemy.actionTarget == null)
        {
            context.agent.ResetPath();
            context.agent.isStopped = true;
            context.enemy.targetPosition= Vector3.negativeInfinity;
            fracaso = true;
            return;
        }

        if (context.enemy.actionTarget.TryGetComponent<PlacedBuild>( out PlacedBuild p))
        {
            var l = p.getValidAttacksPoints();

            Transform s = NearPosition(l);
            if (!ReferenceEquals(s,null))
            {
                context.agent.SetDestination(s.position);
                context.agent.isStopped = false;
            }
            else
            {
                fracaso = true;
            }
        }
        else
        {
            fracaso = true;
        }
         
        
        
    }


    private Transform NearPosition(List<Transform> posiciones)
    {
        if (posiciones.Count > 0)
        {
            Transform closest = posiciones[0];
            float maxDistance = Vector3.Distance(context.transform.position, closest.position);
            foreach (var VARIABLE in posiciones)
            {
                float dis = Vector3.Distance(context.gameObject.transform.position, VARIABLE.position);

                if (dis < maxDistance)
                {
                    // Debug.Log("Max anterior: "+maxDistance+" Distancia : "+ dis+ " Posicion: "+ VARIABLE.gameObject );
                    closest = VARIABLE;
                    maxDistance = dis;
                }
            }

          //  Debug.Log("Vamos a por " + closest.gameObject);
            //  Debug.Log("El mas cercano es : " + closest + " Con una distancia de : " + maxDistance + " POSICION :" +
            //           closest.transform.position);


           //  Debug.DrawLine(context.transform.position, closest.transform.position);
            return closest;
        }

        return null;
    }


    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        //Debug.Log(name);
        if (context.enemy.actionTarget == null || fracaso)
        {
            Debug.Log("FIN");
            context.agent.ResetPath();
            return State.Failure;
        }
        if (Vector3.Distance(context.transform.position, context.agent.destination) < 2f)
        {
            Debug.Log("Hemos llegao");
            return State.Success;
        }

        Vector3 targetPostition = new Vector3(context.enemy.actionTarget.transform.position.x,
            context.transform.position.y,
            context.enemy.actionTarget.transform.position.z);
        context.transform.LookAt(targetPostition);


       // Debug.Log("DE CAMINO al murito");
        return State.Running;
    }
}