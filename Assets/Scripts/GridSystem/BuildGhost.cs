using System;
using System.Collections;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;

public class BuildGhost : MonoBehaviour
{
    // Start is called before the first frame update

    private Transform visual;
    private BuildingSO _building;
    [SerializeField] private GameObject _grid;
    private GhostBuild _ghostBuild;

    void Start()
    {
        RefreshVisual();
        GridBuildingSystem.Instance.OnSelectedChanged += Instance_OnSelectedChanged;
        GridBuildingSystem.Instance.OnObjectPlaced += Instance_OnObjectPlaced;
        GridBuildingSystem.Instance.OnObjectSetPosition += Instance_OnObjectSetPosition;
        GridBuildingSystem.Instance.OnObjectRemovePosition += Instance_OnObjectRemovePosition;
    }


    private void Instance_OnSelectedChanged(object sender, System.EventArgs e)
    {
        RefreshVisual();
        ActivateDisableBuildCanvas();
    }
    private void Instance_OnObjectSetPosition(object sender, System.EventArgs e)
    {
        ActivateDisableBuildCanvas();
    }

    private void Instance_OnObjectRemovePosition(object sender, System.EventArgs e)
    {
        ActivateDisableBuildCanvas();
    }
    
    private void ActivateDisableBuildCanvas()
    {
        if (_ghostBuild != null && GridBuildingSystem.Instance.buildMenu)
        {
            _ghostBuild.EnableCanvas();
            Debug.Log("Activando el canvas");
        }
        else
        {
            _ghostBuild.DisableCanvas();
        }
    }
    
    
    private void Instance_OnObjectPlaced(object sender, System.EventArgs e)
    {
        _grid.SetActive(false);
    }


    private void RefreshVisual()
    {
        if (visual != null) 
        {
            _ghostBuild = null;
            Destroy(visual.gameObject);
            visual = null;
        }

        BuildingSO placedObjectTypeSO = GridBuildingSystem.Instance.buildingSo;

        if (placedObjectTypeSO != null)
        {
            _grid.SetActive(true);
            visual = Instantiate(placedObjectTypeSO.visual, Vector3.zero, Quaternion.identity);
            _ghostBuild = visual.gameObject.GetComponent<GhostBuild>();
            visual.parent = transform;
            visual.localPosition = Vector3.zero;
            visual.localEulerAngles = Vector3.zero;
            SetLayerRecursive(visual.gameObject, 11);
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


    // Update is called once per frame
    private void LateUpdate()
    {
        
        if (visual != null)
        {
            if (GridBuildingSystem.Instance.enableBuildMove)
            {
                Vector3 targetPosition = GridBuildingSystem.Instance.GetMouseWorldSnappedPosition();
                targetPosition.y = 1f;
           
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 15f);
            
                transform.rotation = Quaternion.Lerp(transform.rotation,
                    GridBuildingSystem.Instance.GetPlacedObjectRotation(), Time.deltaTime * 15f);                
            }
            else
            {
                 Vector3 targetPosition = GridBuildingSystem.Instance.GetMouseWorldSnappedPositionV2();
                 targetPosition.y = 1f;
                
                 transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 15f);
            
                transform.rotation = Quaternion.Lerp(transform.rotation,
                    GridBuildingSystem.Instance.GetPlacedObjectRotation(), Time.deltaTime * 15f);
            }
            

        }
    }
}