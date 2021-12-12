using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowUpgrades : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TextMeshProUGUI HealthUpgrade;
    public TextMeshProUGUI DamageUpgrade;
    public TextMeshProUGUI AttackSpeedUpgrade;

    public Image Health;
    public Image Damage;
    public Image AttackSpeed;

    public Sprite SBasicHealth;
    public Sprite SUpgradeHealth;
    public Sprite SBasicDamage;
    public Sprite SUpgradeDamage;
    public Sprite SBasicAttackSpeed;
    public Sprite SUpgradeAttackSpeed;

    public int type = 0;


    public ComunicacionGridCanvas cgc;

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowUpgrade();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ShowUpgrade();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideUpgrades();
    }

    private void ShowUpgrade()
    {
        if (type == 0)
        {
            if (cgc.ExposedPlacedBuild.level >= PlacedBuild.MAXLEVEL) return;
            HealthUpgrade.enabled = true;
            string mejoraVida = Math.Round(
                cgc.ExposedPlacedBuild.CurveHealth.Evaluate(cgc.ExposedPlacedBuild.level + 1) -
                cgc.ExposedPlacedBuild.currentMaxHealth, 2).ToString();
            HealthUpgrade.text = "+ " + mejoraVida;

            DamageUpgrade.enabled = true;
            string mejoraDamage = Math.Round(
                cgc.ExposedPlacedBuild.CurveDamage.Evaluate(cgc.ExposedPlacedBuild.level + 1) -
                cgc.ExposedPlacedBuild.damage, 2).ToString();
            DamageUpgrade.text = "+ " + mejoraDamage;

            AttackSpeedUpgrade.enabled = true;
            string mejoraAttackSpeed = Math.Round(
                cgc.ExposedPlacedBuild.CurveAttackSpeed.Evaluate(cgc.ExposedPlacedBuild.level + 1) -
                cgc.ExposedPlacedBuild.attackSpeed, 2).ToString();
            AttackSpeedUpgrade.text = "+ " + mejoraAttackSpeed;


            Health.sprite = SUpgradeHealth;
            Damage.sprite = SUpgradeDamage;
            AttackSpeed.sprite = SUpgradeAttackSpeed;
        }
        else
        {
            HealthUpgrade.enabled = true;
            string mejoraVida = Math.Round(PlayerStats._instance.CurveHealth.Evaluate(PlayerStats._instance.Level + 1) -
                                           PlayerStats._instance.maxHealth, 2).ToString();
            HealthUpgrade.text = "+ " + mejoraVida;

            DamageUpgrade.enabled = true;
            string mejoraDamage = Math.Round(
                PlayerStats._instance.CurveDamage.Evaluate(PlayerStats._instance.Level + 1) -
                PlayerStats._instance.Damage, 2).ToString();
            DamageUpgrade.text = "+ " + mejoraDamage;

            AttackSpeedUpgrade.enabled = true;
            string mejoraAttackSpeed = Math.Round(
                PlayerStats._instance.CurveAttackSpeed.Evaluate(PlayerStats._instance.Level + 1) -
                PlayerStats._instance.attackSpeed, 2).ToString();
            AttackSpeedUpgrade.text = "+ " + mejoraAttackSpeed;


            Health.sprite = SUpgradeHealth;
            Damage.sprite = SUpgradeDamage;
            AttackSpeed.sprite = SUpgradeAttackSpeed;
        }
    }


    private void HideUpgrades()
    {
        HealthUpgrade.enabled = false;
        DamageUpgrade.enabled = false;
        AttackSpeedUpgrade.enabled = false;

        Health.sprite = SBasicHealth;
        Damage.sprite = SBasicDamage;
        AttackSpeed.sprite = SBasicAttackSpeed;
    }
}