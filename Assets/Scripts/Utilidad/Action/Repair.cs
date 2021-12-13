using UnityEngine;

namespace UtilityBehaviour.Action
{
    [CreateAssetMenu(fileName = "Repair", menuName = "Actions/Repair")]
    public class Repair : UtilityAction

    {
        public override void Execute(NPCController npc)
        {
            npc.accionActual = "Repair";
            npc.scoreAccionActual = Score;
            npc.Repair();
        }
    }
}