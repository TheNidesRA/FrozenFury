using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace UtilityBehaviour.Considerations
{
    [CreateAssetMenu(fileName = "Work", menuName = "Consideration/Work")]
    public class Work : Consideration
    {
        public override float ScoreConsideration(NPCController npc)
        {
            /*
             * considerations[0] --> fatigue
             * considerations[1] --> damagedStructure
             */

            // float fatigueWeight = (1 - considerations[0].Score) * 0.5f;
            // float damagedStructWeight = considerations[1].Score * 0.5f;
            foreach (var consideration in considerations)
            {
                consideration.ScoreConsideration(npc);
            }

            Debug.Log("Escores que llegan en WORK: " + considerations[1].Score + " | " + considerations[0].Score);


            Score = considerations[1].Score / (considerations[0].Score * 0.3f + 1);
            //Debug.Log("CALCULO DEL ESCORE CON LA NUEVA FORMULITA Y TAL"+auxScroe);


            //  Score = fatigueWeight + damagedStructWeight;
            Debug.Log("El escore es : " + Score);
            return Score;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Work))]
    class WorkEditor : Editor
    {
        private Vector2 scroll;
        int current_tab = 0;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var script = (Work) target;
            if (script == null) return;

            EditorGUILayout.Space();


            current_tab = GUILayout.Toolbar(current_tab, new string[] {"Valor"});
            float fatigueWeight = (1 - script.considerations[0].Score) * 0.5f;
            float damagedStructWeight = (1 - script.considerations[1].Score) * 0.5f;

            switch (current_tab)
            {
                case 0:
                    scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(300));

                    EditorGUILayout.BeginHorizontal("box");
                    EditorGUILayout.LabelField("fatigueWeight " + fatigueWeight);
                    EditorGUILayout.LabelField("damagedStructWeight " + damagedStructWeight);
                    EditorGUILayout.LabelField("Score  " + (fatigueWeight + damagedStructWeight));
                    EditorGUILayout.EndHorizontal();


                    EditorGUILayout.EndScrollView();

                    break;
            }
        }
    }
#endif
}