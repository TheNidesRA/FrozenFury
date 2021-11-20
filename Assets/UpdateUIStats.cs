using System;

using TMPro;
using UnityEngine;

public class UpdateUIStats : MonoBehaviour
{
    public TextMeshProUGUI Health;
    public TextMeshProUGUI Damage;
    public TextMeshProUGUI AttackSpeed;

    public void UpdateStatsText(BuildingSO build)
    {
        Health.text = build.initHealth.ToString();
        Damage.text = build.initDamage.ToString();
        AttackSpeed.text = build.initAttackSpeed.ToString();
    }
    
    public void UpdateStatsText(PlacedBuild build)
    {
        Health.text = Math.Round(build.health, 2).ToString();
        Damage.text = Math.Round(build.damage, 2).ToString();
        AttackSpeed.text = Math.Round(build.attackSpeed, 2).ToString();
    }
}