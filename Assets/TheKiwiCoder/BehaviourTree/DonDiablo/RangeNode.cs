using Enemies;
using TheKiwiCoder;
using UnityEngine;

namespace Nodes.DiablilloTree
{
    public class RangeNode : ActionNode
    {
        private Transform _agent;
        public float _triggerDist;

        protected override void OnStart()
        {
            EnemyDemon demon = context.gameObject.GetComponent<EnemyDemon>();
            
            _agent = context.gameObject.transform;
            // _triggerDist = demon._triggerDistance;
        }

        protected override void OnStop()
        {
            // throw new System.NotImplementedException();
        }

        protected override State OnUpdate()
        {
            Transform player = PlayerStats._instance.gameObject.transform;
            float dist = Vector3.Distance(player.position, _agent.position);
            
            return dist < _triggerDist ? State.Success : State.Failure;
        }
    }
}