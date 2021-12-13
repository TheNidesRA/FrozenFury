using UnityEngine;

namespace UtilityBehaviour.Considerations
{
    
    [CreateAssetMenu(fileName = "ToolState", menuName = "Consideration/Toolstate")]
    public class ToolState:Consideration
    {
        public override float ScoreConsideration(NPCController npc)
        {
          
            return 0.5f;
        }
    }
}