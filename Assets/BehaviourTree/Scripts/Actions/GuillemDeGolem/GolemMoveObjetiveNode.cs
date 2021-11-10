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
            
            context.agent.destination = EnemyGoal.instance.transform.position;
        }

        protected override void OnStop()
        {
           // Debug.Log("Parando y tal");
            context.agent.isStopped = true;
           // context.agent.ResetPath();
        }

        protected override State OnUpdate()
        {
            Debug.Log(name);
            if (context.agent.destination != new Vector3(-1,-1,-1))
            {
                
              //  Debug.Log("Moviendonos");
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