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

        protected override void OnStart()
        {
            _objetive = EnemyGoal.instance.getPosition();
            _enemyGolem = context.gameObject.GetComponent<EnemyGolem>();
            context.agent.destination = _objetive;
        }

        protected override void OnStop()
        {
            Debug.Log("Parando y tal");
        }

        protected override State OnUpdate()
        {
            if (context.agent.hasPath)
            {
                Debug.Log("Corriendo");
                float distance = Vector3.Distance(_objetive, context.transform.position);
                float diferencia = _enemyGolem.GetPathRemainingDistance() - distance;

                if (diferencia >= 4)
                {
                    Debug.Log("HAY QUE RECALCULAR");
                    return State.Failure;
                }

                return State.Running;
            }
            else
            {
                return State.Failure;
            }
        }
    }
}