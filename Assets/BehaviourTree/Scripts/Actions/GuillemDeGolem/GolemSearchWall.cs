using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

namespace Nodes.GolemNodes
{
    public class GolemSearchWall : ActionNode
    {
        private List<PlacedBuild> listaMuros;
        private LineRenderer lr;

        protected override void OnStart()
        {
            lr = context.gameObject.GetComponent<LineRenderer>();
        }

        protected override void OnStop()
        {
            // throw new System.NotImplementedException();
        }

        protected override State OnUpdate()
        {
            searchWalls();
            
            if (listaMuros.Count > 0)
            {
                PlacedBuild closest = listaMuros[0];
                float maxDistance = Vector3.Distance(context.transform.position, closest.gameObject.transform.position);
                foreach (var VARIABLE in listaMuros)
                {
                    float dis = Vector3.Distance(context.transform.position, VARIABLE.gameObject.transform.position);
                    if (dis < maxDistance)
                    {
                        closest = VARIABLE;
                        maxDistance = dis;
                    }
                }

                Debug.Log("El mas cercano es : " + closest + " Con una distancia de : " + maxDistance + " POSICION :" +
                          closest.transform.position);

                lr.SetPosition(0, new Vector3(context.transform.position.x, 5, context.transform.position.z));
                lr.SetPosition(1, closest.transform.position);


                Debug.DrawLine(context.transform.position, closest.transform.position);
                return State.Success;
            }

            Debug.Log("Fracaso en la busqueda de muritos");
            return State.Failure;
        }

        private void searchWalls()
        {
            listaMuros = new List<PlacedBuild>();
            Collider[] colliderArray = Physics.OverlapSphere(context.transform.position, 30f);
            foreach (var VARIABLE in colliderArray)
            {
                
                if (VARIABLE.TryGetComponent<PlacedBuild>(out PlacedBuild placedBuild))
                {
                    if (placedBuild.BuildingSo.type == BuildingSO.BuildingType.Wall)
                    {
                        listaMuros.Add(placedBuild);
                    }
                }
            }
        }
    }
}