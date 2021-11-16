using System;
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

    public List<Transform> attactPoints;

    private float _damage;


   [SerializeField] private float _health;
   



    public float damage
    {
        get
        {
            return _damage;
        }set
        {
            _damage = value;
        }
    }

    public float health
    {
        get
        {
            return _health;
        }set
        {

            if (value <= 0)
            {
                _health = 0;
                GridBuildingSystem.Instance.RemoveBuild(this);
            }
           
            _health = value;
            Debug.Log(_buildingSo.name+" vida restante : "+ _health );
        }
    }


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

        if (building.type != BuildingSO.BuildingType.Trap)
        {
            placedBuild._navMeshObstacle = placedBuildTransform.GetComponent<NavMeshObstacle>();
            placedBuild._navMeshObstacle.enabled = true;
        }


        placedBuild._damage = building.damage;
        placedBuild._health = building.health;
        
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

            if (Physics.Raycast(VARIABLE.transform.position, VARIABLE.forward, out hit, 8f,
                1 << LayerMask.NameToLayer("Wall") |
                (1 << LayerMask.NameToLayer("Torreta") | (1 << LayerMask.NameToLayer("Muros")) |
                 (1 << LayerMask.NameToLayer("MuroPlayer")))))
            {
//                Debug.Log(VARIABLE.gameObject);

                //              Debug.Log("Punto blocked");

                VARIABLE.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            }
            else
            {
                valid.Add(VARIABLE);
            }
        }

        return valid;
    }
}