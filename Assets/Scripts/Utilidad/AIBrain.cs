using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif


namespace UtilityBehaviour
{
    public class AIBrain : MonoBehaviour
    {
        public UtilityAction bestAction;

        public NPCController npc;

        public bool finishedDeciding;


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

#if UNITY_EDITOR
    [CustomEditor(typeof(AIBrain))]
    class AIBrainEditor : Editor
    {
        private Vector2 scroll;
        int current_tab = 0;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var script = (AIBrain) target;
            if (script == null) return;


            //  current_tab = GUILayout.Toolbar(current_tab, new string[] {"Rest","GetPaid","Repair"});


            scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(20));


            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("Score Rest: " + script.ScoreAction(script.npc.actionsAviable[0]));
            // EditorGUILayout.LabelField((script.FatigueCurve.Evaluate(i / 100.0f)) + " Score");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndScrollView();

            //  break;
            //  case 1:
            scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(20));


            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("Score GetPaid: " + script.ScoreAction(script.npc.actionsAviable[1]));
            // EditorGUILayout.LabelField((script.FatigueCurve.Evaluate(i / 100.0f)) + " Score");
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.EndScrollView();

            //     break;
            // case 2:
            scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(20));


            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("Score Repair: " + script.ScoreAction(script.npc.actionsAviable[2]));
            // EditorGUILayout.LabelField((script.FatigueCurve.Evaluate(i / 100.0f)) + " Score");
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.EndScrollView();

            //  break;
        }
    }
}
#endif