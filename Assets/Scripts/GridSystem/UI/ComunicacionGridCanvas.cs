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
   
    public TextMeshProUGUI _buildUpdateText;
    public GameObject _buildUpdateObjectPlace;
    public GameObject _buildUpdateConteiner;
    private Transform buildUpdate;
    private PlacedBuild _placedBuild;
    private RectTransform _rectTransformBuildUpdate;
    
    
    private void Start()
    {
        _rectTransformEditBuild = EditBuild.GetComponent<RectTransform>();
        _rectTransformBuildUpdate = _buildUpdateConteiner.GetComponent<RectTransform>();
        RefreshVisual();
        GridBuildingSystem.Instance.OnSelectedChanged += Instance_OnSelectedChanged;
        GridBuildingSystem.Instance.OnObjectRemovePosition += Instance_OnObjectRemovePosition;
        GridBuildingSystem.Instance.OnObjectPlaced += Instance_OnObjectPlaced;
        GridBuildingSystem.Instance.OnObjectSetPosition += Instance_OnObjectSetPosition;
        GridBuildingSystem.Instance.OnBuildSelected += Instance_OnBuildSelected;
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
    
    
    private void Instance_OnBuildSelected(object sender, PlacedBuild eventArgs)
    {
        _buildUpdateConteiner.SetActive(true);
        RefreshVisualUpdateBuild(eventArgs);
        tween = LeanTween.move(_rectTransformBuildUpdate, insidePosition, 0.5f).setEaseInCubic();
        BuildButton.SetActive(false);


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



    public void RemoveBuild()
    {
        if (_placedBuild == null) return;
        GridBuildingSystem.Instance.RemoveBuild(_placedBuild);
        RefreshVisualUpdateBuild(null);
    }

    public void HideUpdateBuild()
    {
        tween = LeanTween.move(_rectTransformBuildUpdate, outsidePosition, 0.5f).setEaseOutCubic().setOnComplete(() => {_buildUpdateConteiner.SetActive(false); });
        BuildButton.SetActive(true);
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

            build.localPosition = new Vector3(0, 0, -150);
            build.localEulerAngles = placedObjectTypeSO.canvasVisual.rotation.eulerAngles;
            SetLayerRecursive(build.gameObject, 5);
            _texto.text = placedObjectTypeSO.name;
        }
        else
        {
            tween = LeanTween.move(_rectTransformEditBuild, outsidePosition, 0.5f).setEaseOutCubic().setOnComplete(() => {EditBuild.SetActive(false); });
            BuildButton.SetActive(true);
        }
    }
    
    
    
    private void RefreshVisualUpdateBuild(PlacedBuild placedBuild)
    {
        if (buildUpdate != null)
        {
            Destroy(buildUpdate.gameObject);
            buildUpdate = null;
            _placedBuild = null;
        }
        
      
        if (placedBuild != null)
        {
            _placedBuild = placedBuild;
            buildUpdate = Instantiate(placedBuild.BuildingSo.canvasVisual, Vector3.zero, Quaternion.identity,
                _buildUpdateObjectPlace.transform);

            buildUpdate.localPosition = new Vector3(0, 0, -60);
            buildUpdate.localEulerAngles = placedBuild.BuildingSo.canvasVisual.rotation.eulerAngles;;
            SetLayerRecursive(buildUpdate.gameObject, 5);
            _buildUpdateText.text = placedBuild.BuildingSo.name;
        }
        else
        {
            tween = LeanTween.move(_rectTransformBuildUpdate, outsidePosition, 0.5f).setEaseOutCubic().setOnComplete(() => {_buildUpdateConteiner.SetActive(false); });
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