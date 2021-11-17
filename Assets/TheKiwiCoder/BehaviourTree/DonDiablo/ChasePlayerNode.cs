using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;

namespace Nodes.DiablilloTree
{
    public class ChasePlayerNode : ActionNode
    {
        private Transform _target;
        protected override void OnStart()
        {
            _target = PlayerStats._instance.transform;
            
        }

        protected override void OnStop()
        {
            // throw new System.NotImplementedException();
        }

        protected override State OnUpdate()
        {
            Debug.Log("Chase player");
            context.agent.destination = _target.position;
            return State.Success;
        }
    }
}