using System.Collections;
using System.Collections.Generic;
using GridSystem;
using TMPro;
using UnityEngine;

public class ComunicacionGridCanvas : MonoBehaviour
{
    public static ComunicacionGridCanvas _instance { get; private set; }

    public GameObject BuildButton;

    public GameObject EditBuild;

    public GameObject buildPlaceVisual;

    private Transform build;
    public TextMeshProUGUI _texto;
    
    
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
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


    public void RefreshVisual()
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