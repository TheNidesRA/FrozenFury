using System.Collections;
using System.Collections.Generic;
using BigfootSdk.SafeArea.BuildingButtons;
using GridSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BuildElementUI : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{

    [SerializeField] private BuildGroupUI _buildGroup;
    public int build;
    public UnityEvent onTabSelected;
    public SideBarButtonActions boton;
    

    public void OnPointerEnter(PointerEventData eventData)
    {
       // _buildGroup.OnPointerEnter(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      //  _buildGroup.OnPointerClick(this);
        GridBuildingSystem.Instance.changeBuild(build);
        boton.buttonClick();
        Select();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       // _buildGroup.OnPointerExit(this);
    }
    
      public void Select()
        {
            if (onTabSelected != null)
            {
                onTabSelected.Invoke();
            }
        }
    
    
}
