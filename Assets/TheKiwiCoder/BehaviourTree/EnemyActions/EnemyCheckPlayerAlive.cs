using TheKiwiCoder;
using UnityEngine;

public class EnemyCheckPlayerAlive:ActionNode
    {
        protected override void OnStart()
        {
            context.enemy.NODOACTUAL = "EnemyCheckPlayerAlive";
        }

        protected override void OnStop()
        {
           
        }

        protected override State OnUpdate()
        {
            Debug.Log("Vida: "+(PlayerStats._instance.Health > 0));
            return PlayerStats._instance.Health > 0 ? State.Success : State.Failure;
        }
    }
