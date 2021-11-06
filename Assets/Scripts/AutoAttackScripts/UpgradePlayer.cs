using System;
using System.Collections;
using UnityEngine;

public class UpgradePlayer : MonoBehaviour
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
}