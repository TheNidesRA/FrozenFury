using System;
using System.Collections;
using Enemies;
using UnityEngine;

namespace UtilityBehaviour.Action
{
    [CreateAssetMenu(fileName = "Rest", menuName = "Actions/Rest")]
    public class Rest : UtilityAction
    {
        public override void Execute(NPCController npc)
        {
            Debug.Log("execute de Rest");
            // npc.Rest(() => { npc.mover.MoveTo(EnemyGoal.instance.getPosition()); }, () =>
            // {
            //    Corout
            // });
            npc.Rest();
        }

        // public IEnumerable descanso(NPCController npc, int descanso)
        // {
        //     yield return new WaitForSeconds(3f);
        //     Debug.Log("se esta descansando restando cansacion");
        //     npc.TimeWorked -= 10;
        // }
    }
}