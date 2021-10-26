using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIA : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    public Vector3 objetive;
    
    void Start()
    {
        _agent.destination = objetive;
    }

    // Update is called once per frame
    void Update()
    { 
    }
}
