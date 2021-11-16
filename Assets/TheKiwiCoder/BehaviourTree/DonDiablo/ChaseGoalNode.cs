using Enemies;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;

namespace Nodes.DiablilloTree
{
    public class ChaseGoalNode : ActionNode
    {
        private NavMeshAgent _agent;
        protected override void OnStart()
        {
            _agent = context.gameObject.GetComponent<NavMeshAgent>();
        }

        protected override void OnStop()
        {
            // throw new System.NotImplementedException();
        }

        protected override State OnUpdate()
        {
            Debug.Log("RUNNING");
            _agent.destination = EnemyGoal.instance.getPosition();
            return State.Success;
        }
    }
}