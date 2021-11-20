using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowUpgrades : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TextMeshProUGUI HealthUpgrade;
    public TextMeshProUGUI DamageUpgrade;
    public TextMeshProUGUI AttackSpeedUpgrade;

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
        if (cgc.ExposedPlacedBuild.level >= PlacedBuild.MAXLEVEL) return;
        HealthUpgrade.enabled = true;
        string mejoraVida = Math.Round(cgc.ExposedPlacedBuild.CurveHealth.Evaluate(cgc.ExposedPlacedBuild.level + 1) -
                                       cgc.ExposedPlacedBuild.currentMaxHealth, 2).ToString();
        HealthUpgrade.text = "+ " + mejoraVida;

        DamageUpgrade.enabled = true;
        string mejoraDamage = Math.Round(cgc.ExposedPlacedBuild.CurveDamage.Evaluate(cgc.ExposedPlacedBuild.level + 1) -
                                         cgc.ExposedPlacedBuild.damage, 2).ToString();
        DamageUpgrade.text = "+ " + mejoraDamage;

        AttackSpeedUpgrade.enabled = true;
        string mejoraAttackSpeed = Math.Round(
            cgc.ExposedPlacedBuild.CurveAttackSpeed.Evaluate(cgc.ExposedPlacedBuild.level + 1) -
            cgc.ExposedPlacedBuild.attackSpeed, 2).ToString();
        AttackSpeedUpgrade.text = "+ " + mejoraAttackSpeed;
    }


    private void HideUpgrades()
    {
        HealthUpgrade.enabled = false;
        DamageUpgrade.enabled = false;
        AttackSpeedUpgrade.enabled = false;
    }
}