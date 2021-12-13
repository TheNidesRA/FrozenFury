using UnityEngine;
using UtilityBehaviour.Considerations;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace UtilityBehaviour.Considerations
{
    [CreateAssetMenu(fileName = "Fatigue", menuName = "Consideration/Fatigue")]
    public class Fatigue : Consideration
    {
        public AnimationCurve FatigueCurve;

        public override float ScoreConsideration(NPCController npc)
        {
            float reduccion = npc.TimeWorked / NPCController.MAXFATIGUE;
            Debug.Log("Calculando peso fatiga");
            Score = FatigueCurve.Evaluate(reduccion);
            return Score;
        }
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(Fatigue))]
class FatigueEditor : Editor
{
    private Vector2 scroll;
    int current_tab = 0;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Fatigue script = (Fatigue) target;
        if (script == null) return;

        EditorGUILayout.Space();


        current_tab = GUILayout.Toolbar(current_tab, new string[] {"Valor"});

        switch (current_tab)
        {
            case 0:
                scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(300));

                for (int i = 0; i < 101; i+=5)
                {
                    EditorGUILayout.BeginHorizontal("box");
                    EditorGUILayout.LabelField("Fatigue " + (i));
                    EditorGUILayout.LabelField((script.FatigueCurve.Evaluate(i / 100.0f)) + " Score");
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndScrollView();

                break;
        }
    }
}
#endif