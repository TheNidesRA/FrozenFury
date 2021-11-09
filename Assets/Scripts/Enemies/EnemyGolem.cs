﻿using System;
using System.Collections.Generic;
using Nodes;
using Nodes.GolemNodes;
using UnityEngine;
using UnityEngine.AI;
#if UNITY_EDITOR
using UnityEditor;

#endif


namespace Enemies
{
    public class EnemyGolem : Enemy
    {
        private Node topNode;

        public float distancia;
        public float path;
        public NavMeshPathStatus PathStatus;
        
        
        void Start()
        {
            //BuildBehaviourTree();
        }


        void BuildBehaviourTree()
        {
            // IsGolemAnObjetiveTransform isGolemAnObjetiveTransform =
            //     new IsGolemAnObjetiveTransform(this, EnemyGoal.instance.transform);
            // GolemMoveObjetiveNode golemMoveObjetiveNode =
            //     new GolemMoveObjetiveNode(EnemyGoal.instance.transform, NavMeshAgent);
            //
            // Sequence GoToObjetive = new Sequence(new List<Node>
            //     {isGolemAnObjetiveTransform, golemMoveObjetiveNode});
            //
            //
            // topNode = new Selector(new List<Node> {GoToObjetive});
        }

        private void Update()
        {
            distancia = Vector3.Distance(NavMeshAgent.destination, transform.position);
            if (NavMeshAgent.hasPath)
                path = NavMeshAgent.remainingDistance;
            PathStatus = NavMeshAgent.pathStatus;
            
            // topNode.Evaluate();
            // if (topNode.nodeState == NodeState.FAILURE)
            // {
            //     Debug.Log("Se ha liao pacooo!!");
            // }
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(EnemyGolem))]
    class EnemyGolemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = (EnemyGolem) target;
            if (script == null) return;

            EditorGUILayout.Space();

            if (script.NavMeshAgent.hasPath)
            {
                EditorGUILayout.LabelField("Distacia al objetivo: " +
                                           Vector3.Distance(script.NavMeshAgent.destination, script.transform.position)
                                               .ToString());

                EditorGUILayout.LabelField("Distacia camino: " +GetPathRemainingDistance(script.NavMeshAgent));
            }
          
        }
        
        
        
        public static float GetPathRemainingDistance(NavMeshAgent navMeshAgent)
        {
            if (navMeshAgent.pathPending ||
                navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid ||
                navMeshAgent.path.corners.Length == 0)
                return -1f;

            float distance = 0.0f;
            for (int i = 0; i < navMeshAgent.path.corners.Length - 1; ++i)
            {
                distance += Vector3.Distance(navMeshAgent.path.corners[i], navMeshAgent.path.corners[i + 1]);
            }

            return distance;
        }
        
        
        
        
        
        
        
        
    }
#endif
}