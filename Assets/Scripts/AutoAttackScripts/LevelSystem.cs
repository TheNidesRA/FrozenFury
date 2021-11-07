using System;
using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif


public class LevelSystem : MonoBehaviour
{
    public AnimationCurve damageLevelCurve;
    public AnimationCurve attackSpeedLevelCurve;
    public event EventHandler<int> OnPlayerLevelChanged;
    public int playerLevel = 0;
    public float attackSpeed = 0f;

    public int PlayerLevel
    {
        get => playerLevel;
        set
        {
            playerLevel = value;
            OnPlayerLevelChanged?.Invoke(this, playerLevel);
        }
    }

    public float damage = 0;


    // Start is called before the first frame update
    void Start()
    {
        OnPlayerLevelChanged += Instace_OnPlayerLevelChange;
        StartCoroutine(nameof(upgradeLevel));
    }

    private void UpdateDamagePlayer(int level)
    {
        damage = damageLevelCurve.Evaluate(level);
        Debug.Log(damage);
    }

    private void UpdateAttacSpeedPlayer(int level)
    {
        attackSpeed = attackSpeedLevelCurve.Evaluate(level);
    }

    private void Instace_OnPlayerLevelChange(object sender, int level)
    {
        UpdateDamagePlayer(level);
        UpdateAttacSpeedPlayer(level);
    }

    IEnumerator upgradeLevel()
    {
        while (true)
        {
            PlayerLevel += 1;
            yield return new WaitForSeconds(5.0f);
        }
    }

    public static float CalculateStat(AnimationCurve curve, float level)
    {
        return curve.Evaluate(level);
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(LevelSystem))]
class LevelSystemEditor : Editor
{
    private Vector2 scroll;
    int current_tab = 0;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (LevelSystem) target;
        if (script == null) return;

        EditorGUILayout.Space();

        

        current_tab=GUILayout.Toolbar (current_tab, new string[] {"dmg", "spd"});

        switch (current_tab)
        {
            case 0:
                scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(300));

                for (int i = 1; i < 15; i++)
                {
                    EditorGUILayout.BeginHorizontal("box");
                    EditorGUILayout.LabelField("Level " + (i));
                    EditorGUILayout.LabelField(((int) script.damageLevelCurve.Evaluate(i)) + " DMG");
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndScrollView();

                break;
            case 1:
                scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(300));

                for (int i = 1; i < 15; i++)
                {
                    EditorGUILayout.BeginHorizontal("box");
                    EditorGUILayout.LabelField("Level " + (i));
                    EditorGUILayout.LabelField(((int) script.attackSpeedLevelCurve.Evaluate(i)) + " SPD");
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndScrollView();
                break;
        }
        
    }
}
#endif