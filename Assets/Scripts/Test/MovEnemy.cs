using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MovEnemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    public Transform position;
    public float speed;
    
    void Start()
    {
        if (transform != null)
            _navMeshAgent.SetDestination(position.position);
        _navMeshAgent.speed = speed;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
