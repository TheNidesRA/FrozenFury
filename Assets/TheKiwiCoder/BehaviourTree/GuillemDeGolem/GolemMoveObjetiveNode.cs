using Enemies;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;

namespace Nodes.GolemNodes
{
    public class GolemMoveObjetiveNode : ActionNode
    {
        private Vector3 _objetive;
        private EnemyGolem _enemyGolem;
        private Vector3 lastPosition;
        private int cont = 0;

        protected override void OnStart()
        {
            _objetive = EnemyGoal.instance.getPosition();
            _enemyGolem = context.gameObject.GetComponent<EnemyGolem>();

            context.agent.SetDestination(EnemyGoal.instance.transform.position); 
            lastPosition = context.agent.transform.position;
        }

        protected override void OnStop()
        {
            // Debug.Log("Parando y tal");
            context.agent.isStopped = true;
            // context.agent.ResetPath();
        }

        protected override State OnUpdate()
        {
           // Debug.Log(name);
            if (context.agent.destination != new Vector3(-1, -1, -1))
            {
                if (lastPosition == context.agent.transform.position)
                {
                    cont++;
                }
                else
                {
                    lastPosition = context.agent.transform.position;
                    cont = 0;
                }

                if (cont == 20)
                {
                    context.agent.ResetPath();
                    return State.Failure;
                    Debug.Log("stuck!!");
                }
                
                
                //Debug.Log("Moviendonos");
                return State.Running;
            }
            else
            {
                // Debug.Log("Fracaso??");
                return State.Failure;
            }
        }
    }
}