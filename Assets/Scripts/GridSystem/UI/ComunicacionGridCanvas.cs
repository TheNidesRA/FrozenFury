using System;
using System.Collections;
using System.Collections.Generic;
using GridSystem;
using TMPro;
using UnityEngine;

public class ComunicacionGridCanvas : MonoBehaviour
{


    public GameObject BuildButton;

    public GameObject EditBuild;
    private RectTransform _rectTransformEditBuild;
    public GameObject buildPlaceVisual;

    private Transform build;
    public TextMeshProUGUI _texto;

    public Vector3 insidePosition;
    public Vector3 outsidePosition;

    public float transitionTime;

    private LTDescr tween;
    
    
    /////////////////////////////////////////////////////////////////////////////
   
    
    
    
    
    private void Start()
    {
        _rectTransformEditBuild = EditBuild.GetComponent<RectTransform>();
        RefreshVisual();
        GridBuildingSystem.Instance.OnSelectedChanged += Instance_OnSelectedChanged;
        GridBuildingSystem.Instance.OnObjectRemovePosition += Instance_OnObjectRemovePosition;
        GridBuildingSystem.Instance.OnObjectPlaced += Instance_OnObjectPlaced;
        GridBuildingSystem.Instance.OnObjectSetPosition += Instance_OnObjectSetPosition;
    }


    private void Instance_OnSelectedChanged(object sender, EventArgs eventArgs)
    {
        RefreshVisual();
    }

    private void Instance_OnObjectRemovePosition(object sender, EventArgs eventArgs)
    {
        EnableBuildMoving();
    }
    
    private void Instance_OnObjectPlaced(object sender, EventArgs eventArgs)
    {
        FinishBuilding();
    }
    private void Instance_OnObjectSetPosition(object sender, EventArgs eventArgs)
    {
        SetBuildPosition();
    }

    


    public void FinishBuilding()
    {
        BuildButton.SetActive(true);
        tween = LeanTween.move(_rectTransformEditBuild, outsidePosition, 0.5f).setEaseOutCubic().setOnComplete(() => {EditBuild.SetActive(false); });
    }


    public void SetBuildPosition()
    {
        BuildButton.SetActive(false);
        EditBuild.SetActive(true);
        tween = LeanTween.moveX(_rectTransformEditBuild.GetComponent<RectTransform>(), insidePosition.x, 0.5f).setEaseInCubic();//.setOnComplete(() => {EditBuild.SetActive(true); });
    }
    
    public void EnableBuildMoving()
    {
        tween = LeanTween.move(_rectTransformEditBuild, outsidePosition, 0.5f).setEaseOutCubic().setOnComplete(() => {EditBuild.SetActive(false); });
    }


    private void RefreshVisual()
    {
        if (build != null)
        {
            Destroy(build.gameObject);
            build = null;
        }
        
        BuildingSO placedObjectTypeSO = GridBuildingSystem.Instance.buildingSo;
        if (placedObjectTypeSO != null)
        {
            
            build = Instantiate(placedObjectTypeSO.canvasVisual, Vector3.zero, Quaternion.identity,
                buildPlaceVisual.transform);

            build.localPosition = new Vector3(0, 0, -60);
            build.localEulerAngles = Vector3.zero;
            SetLayerRecursive(build.gameObject, 5);
            _texto.text = placedObjectTypeSO.name;
        }
        else
        {
            tween = LeanTween.move(_rectTransformEditBuild, outsidePosition, 0.5f).setEaseOutCubic().setOnComplete(() => {EditBuild.SetActive(false); });
            BuildButton.SetActive(true);
        }
    }
    
    
    private void SetLayerRecursive(GameObject targetGameObject, int layer)
    {
        targetGameObject.layer = layer;
        foreach (Transform child in targetGameObject.transform)
        {
            SetLayerRecursive(child.gameObject, layer);
        }
    }
    

}