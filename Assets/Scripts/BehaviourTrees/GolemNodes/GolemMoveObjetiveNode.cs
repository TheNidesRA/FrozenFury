using UnityEngine;
using UnityEngine.AI;

namespace Nodes.GolemNodes
{
    public class GolemMoveObjetiveNode:Node
    {


        private Transform _objetive;
        private NavMeshAgent _navMeshAgent;


        public GolemMoveObjetiveNode(Transform objetive, NavMeshAgent navMeshAgent)
        {
            _objetive = objetive;
            _navMeshAgent = navMeshAgent;
        }

        public override NodeState Evaluate()
        {
            float distance = Vector3.Distance(_objetive.position, _navMeshAgent.transform.position);
            if (distance > 0.2f)
            {
                _navMeshAgent.isStopped = false;
                _navMeshAgent.SetDestination(_objetive.position);
                return NodeState.RUNNING;
            }
            else
            {
                _navMeshAgent.isStopped = true;
                return NodeState.SUCCESS;
            }
            
        }
    }
}