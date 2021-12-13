using UnityEngine;

namespace UtilityBehaviour.Considerations
{
    [CreateAssetMenu(fileName = "Fatigue", menuName = "Consideration/Fatigue")]
    public class Fatigue:Consideration
    {
        const float MAXFATIGUE = 100;
        public AnimationCurve FatigueCurve;
        public override float ScoreConsideration(NPCController npc)
        {
            float reduccion = npc.TimeWorked / MAXFATIGUE;
            return FatigueCurve.Evaluate(reduccion);
        }
    }
}