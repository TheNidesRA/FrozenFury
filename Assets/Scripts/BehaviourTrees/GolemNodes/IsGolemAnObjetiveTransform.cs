using System.Collections;
using System.Collections.Generic;
using Enemies;
using Nodes;
using UnityEngine;

public class IsGolemAnObjetiveTransform : Node
{

   private EnemyGolem _enemyGolem;
   private Transform _objetive;

   public IsGolemAnObjetiveTransform(EnemyGolem enemyGolem, Transform objetive)
   {
       _enemyGolem = enemyGolem;
       _objetive = objetive;
   }

   public override NodeState Evaluate()
   {
       return (_objetive == null) ? NodeState.FAILURE : NodeState.SUCCESS;
   }
}
