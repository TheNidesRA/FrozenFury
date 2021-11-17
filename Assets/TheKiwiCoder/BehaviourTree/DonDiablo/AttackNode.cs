using Enemies;
using TheKiwiCoder;
using UnityEngine;

namespace Nodes.DiablilloTree
{
    public class AttackNode : ActionNode
    {
        private EnemyDemon _enemyDemon;
        private Transform _target;

        protected override void OnStart()
        {
            _enemyDemon = context.gameObject.GetComponent<EnemyDemon>();
            _target = PlayerStats._instance.transform;
            // _enemyDemon.Attack(_target);
        }

        protected override void OnStop()
        {
            // throw new System.NotImplementedException();
            // _enemyDemon.StopAttack();
        }

        protected override State OnUpdate()
        {
            return State.Success;
        }
    }
}