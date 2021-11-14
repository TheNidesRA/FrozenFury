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
            context.enemy.NODOACTUAL = "GolemMoveObjetiveNode";
            _objetive = EnemyGoal.instance.getPosition();
            _enemyGolem = context.gameObject.GetComponent<EnemyGolem>();

            context.agent.ResetPath();
            context.agent.isStopped = false;
            context.agent.SetDestination(EnemyGoal.instance.transform.position);
            context.enemy.actionTarget = EnemyGoal.instance.gameObject;
            context.enemy.targetPosition = EnemyGoal.instance.getPosition();
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
//            Debug.Log("En pleno movimiento :)");
            if (lastPosition == context.transform.position)
                cont++;
            lastPosition = context.transform.position;
            if (cont == 3)
            {
                Debug.Log("Estas quietesito");
                context.agent.SetDestination(EnemyGoal.instance.getPosition());
            }

            // Debug.Log(name);
            if (context.agent.destination != new Vector3(-1, -1, -1))
            {
                if (Vector3.Distance(context.transform.position, context.agent.destination) < 3f)
                {
                    return State.Success;
                }

//                Debug.Log("Moviendonos");
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