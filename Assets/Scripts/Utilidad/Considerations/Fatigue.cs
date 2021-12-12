using UnityEngine;

namespace UtilityBehaviour.Considerations
{
    public class Fatigue:Consideration
    {
        public AnimationCurve FatigueCurve;
        public override float ScoreConsideration(NPCController npc)
        {
            return 0;
        }
    }
}