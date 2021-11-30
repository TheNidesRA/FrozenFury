using System;
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
    private Image _image;
    private Color _initColor;
    public Color FailColor = Color.red;

    public BreathTitle gold = null;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _initColor = _image.color;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
       // _buildGroup.OnPointerEnter(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      //  _buildGroup.OnPointerClick(this);
      if (!GridBuildingSystem.Instance.changeBuild(build))
      {
          LeanTween.value(_image.gameObject, setColorCallback, _initColor, FailColor, 0.3f).setOnComplete(

              () =>
              {

                  if (!ReferenceEquals(gold, null))
                  {
                      gold.StartTweening();
                  }
                  
                  LeanTween.value(_image.gameObject, setColorCallback, FailColor, _initColor, 0.3f).setOnComplete(
                      () =>
                      {
                          boton.buttonClick();
                          Select();
                      });
              });
        
          // LeanTween.color(GetComponent<Image>().material, Color.red, 2f);
          // LeanTween.color(GetComponent<RectTransform>(), Color.red, 0.4f);
          // .material.color.
      }
      else
      {
          boton.buttonClick();
          Select();
      }
        
    }
    
    private void setColorCallback( Color c )
    {
        _image.color = c;
 
        // For some reason it also tweens my image's alpha so to set alpha back to 1 (I have my color set from inspector). You can use the following
 
        var tempColor = _image.color;
        tempColor.a = 1f;
        _image.color = tempColor;
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
