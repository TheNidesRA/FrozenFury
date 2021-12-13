using UnityEngine;

namespace UtilityBehaviour.Action
{
    [CreateAssetMenu(fileName = "GetPaid", menuName = "Actions/GetPaid")]
    public class GetPaid:UtilityAction
    {
        
        public override void Execute(NPCController npc)
        {
         Debug.Log("Pagame rata");
         npc.accionActual = "GET PAID";
         npc.scoreAccionActual = Score;
         npc.GetPaid();
        }
    }
}