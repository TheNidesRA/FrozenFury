using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradePlayer : MonoBehaviour
{
    public TextMeshProUGUI Health;
    public TextMeshProUGUI Damage;
    public TextMeshProUGUI AttackSpeed;
    public TextMeshProUGUI GoldUpgrade;
    private PlayerStats player;

    private void Start()
    {
        player = PlayerStats._instance;
        PlayerStats._instance.OnLvlChanged += Instance_OnLvlChanged;
       
        
    }

    public void Instance_OnLvlChanged(object sender, int i)
    {
        UpdateStatsPlayer();
    }

    public void UpdateStatsPlayer()
    {
        Debug.Log("Vida: " + player.Health + " Damage: " + player.Damage + " AttaclSpeed:" + player.attackSpeed);
        Health.text = Math.Round(PlayerStats._instance.Health, 2).ToString();
        Damage.text = Math.Round(PlayerStats._instance.Damage, 2).ToString();
        AttackSpeed.text = Math.Round(PlayerStats._instance.attackSpeed, 2).ToString();
        GoldUpgrade.text = "x" + PlayerStats._instance.gold.ToString();
    }


    public void LevelUpPlayer()
    {
        player.LevelingUp();
    }
}