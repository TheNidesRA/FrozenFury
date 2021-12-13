using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UtilityBehaviour.Considerations
{
    [CreateAssetMenu(fileName = "ToolState", menuName = "Consideration/Toolstate")]
    public class ToolState : Consideration
    {
        public AnimationCurve considerationCurve;

        public override float ScoreConsideration(NPCController npc)
        {
            return considerationCurve.Evaluate(npc.ToolDurability / NPCController.MAXTOOLDURABILITY);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(ToolState))]
    class ToolStateEditor : Editor
    {
        private Vector2 scroll;
        int current_tab = 0;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var script = (ToolState) target;
            if (script == null) return;

            EditorGUILayout.Space();


            current_tab = GUILayout.Toolbar(current_tab, new string[] {"Valor"});

            switch (current_tab)
            {
                case 0:
                    scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(300));

                    for (int i = 0; i < 101; i++)
                    {
                        EditorGUILayout.BeginHorizontal("box");
                        EditorGUILayout.LabelField("ToolState " + (i));
                        EditorGUILayout.LabelField((script.considerationCurve.Evaluate(i / 100.0f)) + " Score");
                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.EndScrollView();

                    break;
            }
        }
    }
#endif
}