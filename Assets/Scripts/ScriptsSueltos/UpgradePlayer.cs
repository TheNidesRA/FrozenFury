using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePlayer : MonoBehaviour
{
    public TextMeshProUGUI Health;
    public TextMeshProUGUI Damage;
    public TextMeshProUGUI AttackSpeed;
    public TextMeshProUGUI GoldUpgrade;
    private PlayerStats player;
    public Button btn;
    public Image texto;
    public Image Ladrillos;
    public TextMeshProUGUI coste;
    public Color DisableColor;

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
        //Debug.Log("Vida: " + PlayerStats._instance.Health + " Damage: " + PlayerStats._instance.Damage + " AttaclSpeed:" + PlayerStats._instance.attackSpeed);
        Health.text = Math.Round(PlayerStats._instance.Health, 2).ToString();
        Damage.text = Math.Round(PlayerStats._instance.Damage, 2).ToString();
        AttackSpeed.text = Math.Round(PlayerStats._instance.attackSpeed, 2).ToString();
        GoldUpgrade.text = "x" + PlayerStats._instance._goldLevelCost.ToString();
    }


    public void LevelUpPlayer()
    {
        if (PlayerStats._instance?.Level >= 15)
        {
            btn.interactable = false;
            //texto.color = new Color(0, 0, 0, 0);
            texto.color = DisableColor;
            Ladrillos.color = DisableColor;
            coste.color = DisableColor;
        }
        else
        {
            player.LevelingUp();
        }
    }
}