using System;
using System.Collections;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;
using UnityEngine.AI;

public class PlacedBuild : MonoBehaviour
{
    private NavMeshObstacle _navMeshObstacle;
    private BuildingSO _buildingSo;
    public BuildingSO BuildingSo => _buildingSo;
    public Vector2Int origin => _origin;
    public BuildingSO.Dir dir => _dir;
    private Vector2Int _origin;
    private BuildingSO.Dir _dir;

   [SerializeField] private GameObject UI;

    
    
    public static PlacedBuild Create(Vector3 worldPosition, Vector2Int origin, BuildingSO.Dir dir, BuildingSO building)
    {
        Transform placedBuildTransform = Instantiate(
            building.prefab,
            worldPosition,
            Quaternion.Euler(0, building.GetRotationAngle(dir), 0)
        );

        PlacedBuild placedBuild = placedBuildTransform.GetComponent<PlacedBuild>();
        placedBuild._buildingSo = building;
        placedBuild._dir = dir;
        placedBuild._navMeshObstacle= placedBuildTransform.GetComponent<NavMeshObstacle>();
        placedBuild._navMeshObstacle.enabled = true;

        return placedBuild;
    }


    private void OnEnable()
    {
        GridBuildingSystem.Instance.OnClickOutOfObject += DisableCanvas;
    }

    private void OnDisable()
    {
        GridBuildingSystem.Instance.OnClickOutOfObject -= DisableCanvas;
    }
    
    public void IsClicked()
    {
        EnableCanvas();
    }

    private void Activate(object sender, EventArgs eventArgs)
    {
        _navMeshObstacle.enabled = true;
    }
    

    public void DestroySelf()
    {
        Destroy(gameObject);
    }


    public List<Vector2Int> GetGridPositionList()
    {
        return _buildingSo.GetGridPositionList(origin, dir);
    }

    public void EnableCanvas()
    {
        UI.SetActive(true);
    }
    public void DisableCanvas()
    {
        UI.SetActive(false);
    }

    public void DisableCanvas(object a, EventArgs args)
    {
        UI.SetActive(false);
    }
    public void SendDestroy()
    {
       
        GridBuildingSystem.Instance.RemoveBuild(this);
    }
    
}