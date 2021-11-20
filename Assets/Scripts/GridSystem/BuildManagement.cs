using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class BuildManagement : MonoBehaviour
{
    public ComunicacionGridCanvas cgc;
    public Button Btn;
    public UpdateUIStats UpdateUIStats;


    public void CheckIfInteractable()
    {
        if (cgc.ExposedPlacedBuild.level >= 15)
        {
            Btn.interactable = false;
        }

        else
        {
            Btn.interactable = true;
        }
    }

    public void Repair()
    {
        if (cgc.ExposedPlacedBuild == null)
        {
            Debug.Log("Fallo");
            return;
        }
        
        cgc.ExposedPlacedBuild.Repair();
    }
    
    public void LevelUp()
    {
        if (cgc.ExposedPlacedBuild == null)
        {
            Debug.Log("Fallo");
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
        }

        else
        {
            Btn.interactable = true;
        }

    }
}