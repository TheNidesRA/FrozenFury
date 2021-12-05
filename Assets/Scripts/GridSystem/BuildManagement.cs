using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class BuildManagement : MonoBehaviour
{
    public ComunicacionGridCanvas cgc;
    public Button Btn;
    public Image LevelUpText;
    public Image ladrillos;
    public TextMeshProUGUI numero;
    public Color DisableColor;
    private Color Default = Color.white;
    public UpdateUIStats UpdateUIStats;


    public void CheckIfInteractable()
    {
        if (cgc.ExposedPlacedBuild.level >= 15)
        {
            Btn.interactable = false;
           
            numero.color = DisableColor;
            ladrillos.color = DisableColor;
            LevelUpText.color = DisableColor;
        }

        else
        {
            Btn.interactable = true;
            numero.color = Default;
            ladrillos.color = Default;
            LevelUpText.color = Default;
        }
    }

    public void Repair()
    {
        if (cgc.ExposedPlacedBuild == null)
        {
            return;
        }
        
        cgc.ExposedPlacedBuild.Repair();
    }
    
    public void LevelUp()
    {
        if (cgc.ExposedPlacedBuild == null)
        {
            return;
        }
        if (cgc.ExposedPlacedBuild.level >= 15)
        {
            Btn.interactable = false;
        }

        else
        {
            Btn.interactable = true;
        }


     
        cgc.ExposedPlacedBuild.LevelingUp();
        UpdateUIStats.UpdateStatsText(cgc.ExposedPlacedBuild);
        
        if (cgc.ExposedPlacedBuild.level >= 15)
        {
            Btn.interactable = false;
            Btn.interactable = false;
          
            numero.color = DisableColor;
            ladrillos.color = DisableColor;
            LevelUpText.color = DisableColor;
        }

        else
        {
            Btn.interactable = true;
            numero.color = Default;
            ladrillos.color = Default;
            LevelUpText.color = Default;
        }

    }
}