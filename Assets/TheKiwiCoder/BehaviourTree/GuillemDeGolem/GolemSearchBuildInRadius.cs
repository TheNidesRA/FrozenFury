
using Enemies;
using UnityEngine;
using TheKiwiCoder;


public class GolemSearchBuildInRadius : ActionNode
{

    public float radius;
    public LayerMask layer;
    public GameObject debugTarget;
    [SerializeField] private Sprite ActionImage;

    protected override void OnStart()
        {
            context.enemy.NODOACTUAL = "GolemSearchWall";
            debugTarget = null;
            context.enemy.ActionImage.sprite = ActionImage;
        }

        protected override void OnStop()
        {
            // throw new System.NotImplementedException();
        }

        protected override State OnUpdate()
        {
            //Debug.Log(name);
         

            if (searchWalls())
            {
                

                // Debug.DrawLine(context.transform.position, closest.transform.position);
                return State.Success;
            }

            //  Debug.Log("Fracaso en la busqueda de muritos");
            return State.Failure;
        }

        private bool searchWalls()
        {
           
            Collider[] colliderArray =
                Physics.OverlapSphere(context.transform.position, radius,layer);
            
            float minDistance = float.MaxValue;
            GameObject target = null;
            
            
            foreach (var collider in colliderArray)
            {
                // if (collider.TryGetComponent<PlacedBuild>(out PlacedBuild placedBuild))
                // {
                  
                        float distance =
                            Vector3.Distance(context.transform.position, collider.gameObject.transform.position);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            target = collider.gameObject;
                        }
                    
                // }
            }

            if (ReferenceEquals(target, null))
            {
                
                Vector3 direccion = (EnemyGoal.instance.getPosition()-context.transform.position ).normalized;
                float distance = Vector3.Distance(context.transform.position, EnemyGoal.instance.getPosition());
                if (Physics.Raycast(context.gameObject.transform.position, direccion, out RaycastHit hitInfo, distance,
                   layer))
                {
                    //   Debug.Log("Colisionamos o que?");
                    
                        //   Debug.Log("ENCONTRADO!!");
                        target = hitInfo.collider.gameObject;
                        debugTarget = target;
                        context.enemy.actionTarget = target;
                        return true;
                }
                
                return false;
            }
               
            debugTarget = target;
            context.enemy.actionTarget = target;
            return true;


        }
}
