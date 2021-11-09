using Enemies;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;

namespace Nodes.GolemNodes
{
    public class GolemMoveObjetiveNode:ActionNode
    {


        private Vector3 _objetive;
    


   
        protected override void OnStart()
        {
            _objetive =  EnemyGoal.instance.getPosition();
            context.agent.destination = _objetive;


        }

        protected override void OnStop()
        {
            //throw new System.NotImplementedException();
        }

        protected override State OnUpdate()
        {
            float distance = Vector3.Distance(_objetive, context.transform.position);
            if (distance > 0.2f)
            {
                //context.agent.isStopped = false;
                //context.agent.SetDestination(_objetive.position);
                return State.Running;
            }
            else
            {
                context.agent.isStopped = true;
                return State.Success;
            }
        }
    }
}