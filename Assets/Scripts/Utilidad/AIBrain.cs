using System;
using UnityEngine;

namespace UtilityBehaviour
{
    public class AIBrain : MonoBehaviour
    {
        public UtilityAction bestAction;

        [SerializeField] private NPCController npc;

        public bool finishedDeciding;


        private void Update()
        {
            // if (bestAction is null)
            // {
            //     DecideBestAction(npc.actionsAviable);
            // }
        }

        public void DecideBestAction(UtilityAction[] actionsAvaiable)
        {
            float score = 0f;

            int index = 0;
            for (int i = 0; i < actionsAvaiable.Length; i++)
            {
                if (ScoreAction(actionsAvaiable[i]) > score)
                {
                    index = i;
                    score = actionsAvaiable[i].Score;
                }
                
            }
            bestAction = actionsAvaiable[index];
             finishedDeciding = true;
        }

        public float ScoreAction(UtilityAction action)
        {
            float score = 1f;

            foreach (var consideration in action.considerations)
            {
                float considerationScore = consideration.ScoreConsideration(npc);
                score *= considerationScore;
                if (score == 0)
                {
                    action.Score = 0;
                    return action.Score;
                }
            }

            float originalScore = score;
            float modFactor = 1 - (1 / action.considerations.Length);
            float makeupValue = (1 - originalScore) * modFactor;
            action.Score = originalScore + (makeupValue * originalScore);


            return action.Score;
        }
    }
}