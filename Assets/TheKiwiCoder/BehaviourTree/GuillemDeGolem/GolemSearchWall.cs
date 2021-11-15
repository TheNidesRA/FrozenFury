using System.Collections.Generic;
using Enemies;
using TheKiwiCoder;
using UnityEngine;


namespace Nodes.GolemNodes
{
    public class GolemSearchWall : ActionNode
    {
        private PlacedBuild nearestWall;
        private EnemyGolem _enemyGolem;
        private LineRenderer lr;
        public bool debug = false;
        public float radioSearch = 10f;
        
        
        
        protected override void OnStart()
        {
            context.enemy.NODOACTUAL = "GolemSearchWall";
            if (debug)
                lr = context.gameObject.GetComponent<LineRenderer>();

            nearestWall = null;
        }

        protected override void OnStop()
        {
            // throw new System.NotImplementedException();
        }

        protected override State OnUpdate()
        {
            //Debug.Log(name);
            searchWalls();

            if (!ReferenceEquals(nearestWall, null))
            {
               

                // float maxDistance = Vector3.Distance(context.transform.position, closest.gameObject.transform.position);
                // foreach (var VARIABLE in nearestWall)
                // {
                //     float dis = Vector3.Distance(context.transform.position, VARIABLE.gameObject.transform.position);
                //     if (dis < maxDistance)
                //     {
                //         closest = VARIABLE;
                //         maxDistance = dis;
                //     }
                // }

                //  Debug.Log("El mas cercano es : " + closest + " Con una distancia de : " + maxDistance + " POSICION :" +
                //           closest.transform.position);
                // if (debug)
                // {
                //     lr.SetPosition(0, new Vector3(context.transform.position.x, 5, context.transform.position.z));
                //     lr.SetPosition(1, closest.transform.position);
                // }
            
               // Debug.Log("hollaasA?SAS");
                context.enemy.targetPosition = nearestWall.transform.position;
                context.enemy.actionTarget = nearestWall.gameObject;

                // Debug.DrawLine(context.transform.position, closest.transform.position);
                return State.Success;
            }

            //  Debug.Log("Fracaso en la busqueda de muritos");
            return State.Failure;
        }

        private void searchWalls()
        {
            // listaMuros = new List<PlacedBuild>();
            // Collider[] colliderArray =
            //     Physics.OverlapSphere(context.transform.position, radioSearch, LayerMask.GetMask("Muros"));
            // foreach (var VARIABLE in colliderArray)
            // {
            //     if (VARIABLE.TryGetComponent<PlacedBuild>(out PlacedBuild placedBuild))
            //     {
            //         if (placedBuild.BuildingSo.type == BuildingSO.BuildingType.Wall)
            //         {
            //             listaMuros.Add(placedBuild);
            //         }
            //     }
            // }

            Vector3 direccion = (EnemyGoal.instance.getPosition()-context.transform.position ).normalized;
            float distance = Vector3.Distance(context.transform.position, EnemyGoal.instance.getPosition());
            
//            Debug.Log("Lazando rallo");
            if (Physics.Raycast(context.gameObject.transform.position, direccion, out RaycastHit hitInfo, distance,
                LayerMask.GetMask("Wall")))
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
}