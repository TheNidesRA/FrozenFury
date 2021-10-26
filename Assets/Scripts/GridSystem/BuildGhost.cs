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

    void Start()
    {
        RefreshVisual();
        GridBuildingSystem.Instance.OnSelectedChanged += Instance_OnSelectedChanged;
    }


    private void Instance_OnSelectedChanged(object sender, System.EventArgs e)
    {
        RefreshVisual();
    }


    private void RefreshVisual()
    {
        if (visual != null)
        {
            _grid.SetActive(false);
            Destroy(visual.gameObject);
            visual = null;
        }

        BuildingSO placedObjectTypeSO = GridBuildingSystem.Instance.buildingSo;

        if (placedObjectTypeSO != null)
        {
            _grid.SetActive(true);
            visual = Instantiate(placedObjectTypeSO.prefab, Vector3.zero, Quaternion.identity);
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
          
            Vector3 targetPosition = GridBuildingSystem.Instance.GetMouseWorldSnappedPosition();
            targetPosition.y = 1f;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 15f);

            transform.rotation = Quaternion.Lerp(transform.rotation,
                GridBuildingSystem.Instance.GetPlacedObjectRotation(), Time.deltaTime * 15f);
        }
    }
}