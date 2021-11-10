using System;
using System.Collections;
using System.Collections.Generic;
using GridSystem;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
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

    public List<Transform> attactPoints;


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
        placedBuild._navMeshObstacle = placedBuildTransform.GetComponent<NavMeshObstacle>();
        placedBuild._navMeshObstacle.enabled = true;
       // placedBuild.getValidAttacksPoints();
        return placedBuild;
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


    private void Update()
    {
        foreach (var VARIABLE in attactPoints)
        {
            Debug.DrawRay(VARIABLE.position, VARIABLE.forward * 10f, Color.green);
        }
    }

    public List<Transform> getValidAttacksPoints()
    {
        List<Transform> valid = new List<Transform>();
        foreach (var VARIABLE in attactPoints)
        {
            RaycastHit hit;

            if (Physics.Raycast(VARIABLE.transform.position, VARIABLE.forward, out hit, 10f,
                LayerMask.GetMask("Muros")))
            {
              //  Debug.Log(VARIABLE.gameObject);

                //Debug.Log("Punto blocked");
            }
            else
            {
                valid.Add(VARIABLE);
            }
        }

        return valid;
    }
}