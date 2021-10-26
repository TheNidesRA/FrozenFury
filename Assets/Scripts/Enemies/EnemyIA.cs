using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIA : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    void Start()
    {
        _agent.destination = EnemyGoal.instance.getPosition();
    }

    public void setSpeed(float speed)
    {
        _agent.speed = speed;
    }
}
