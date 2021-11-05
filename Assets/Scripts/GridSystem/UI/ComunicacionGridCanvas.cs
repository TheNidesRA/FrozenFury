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

    public GameObject buildPlaceVisual;

    private Transform build;
    public TextMeshProUGUI _texto;
    
    
    
    private void Start()
    {
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

    
    
    
    
    public void StartBuilding()
    {
        BuildButton.SetActive(false);
        EditBuild.SetActive(true);
        
    }

    public void FinishBuilding()
    {
        BuildButton.SetActive(true);
        EditBuild.SetActive(false);
    }


    public void SetBuildPosition()
    {
        EditBuild.SetActive(true);
    }
    
    public void EnableBuildMoving()
    {
        EditBuild.SetActive(false);
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
            EditBuild.SetActive(false);
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