using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

public class EnemyMoveToBuild : ActionNode
{
    bool fracaso=false;
   // [SerializeField] private Sprite ActionImage;

    protected override void OnStart()
    {
        context.enemy.NODOACTUAL = "EnemyMoveToBuild";

        context.animator.SetBool("Run", true); //Run animation trigger

        fracaso = false;

        if (context.enemy.actionTarget == null)
        {
            context.agent.ResetPath();
            context.agent.isStopped = true;
            context.enemy.targetPosition = Vector3.negativeInfinity;
            fracaso = true;
            // Debug.Log("null el target");
            return;
        }

        if (context.enemy.actionTarget.TryGetComponent<PlacedBuild>(out PlacedBuild p))
        {
     
            context.enemy.ActionImage.sprite = BocadillosSistema._instance.GetSprite(p.BuildingSo.name);
            var l = p.getValidAttacksPoints();
            // Debug.Log("Ya no es null de hecho estamos analizandolo XD");
            Transform s = NearPosition(l);
            if (!ReferenceEquals(s, null))
            {
                context.agent.SetDestination(s.position);
                context.agent.isStopped = false;
            }
            else
            {
                // Debug.Log("No hay por donde darle");
                fracaso = true;
            }
        }
        else
        {
            // Debug.Log("No es un edificio");
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
       // context.agent.ResetPath();
        context.agent.isStopped = true;
        
    }

    protected override State OnUpdate()
    {
        if (context.enemy.actionTarget == null || fracaso)
        {
            context.agent.ResetPath();
            context.agent.isStopped = true;
            Debug.Log(context.enemy.actionTarget);
            // Debug.Log("Fracasillo y tal");
            return State.Failure;
        }

        if (Vector3.Distance(context.transform.position, context.agent.destination) <= context.agent.stoppingDistance+0.2)
        {
            // Debug.Log("Hemos llegao");
            return State.Success;
        }

        Vector3 targetPostition = new Vector3(context.enemy.actionTarget.transform.position.x,
            context.transform.position.y,
            context.enemy.actionTarget.transform.position.z);
        context.transform.LookAt(targetPostition);
//        Debug.Log("De camino y tal");

        return State.Running;
    }
}