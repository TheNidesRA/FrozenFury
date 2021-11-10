using System;
using System.Collections.Generic;
using Nodes;
using Nodes.GolemNodes;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;
#if UNITY_EDITOR
using UnityEditor;

#endif


namespace Enemies
{
    public class EnemyGolem : Enemy
    {
        private NavMeshPathStatus s;
        public Vector3 objetive;
        public PlacedBuild buildObjetive;
        public BehaviourTreeRunner tr;
        private void Update()
        {
          
            s = this.NavMeshAgent.pathStatus;
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

                EditorGUILayout.LabelField("Distacia camino: " + GetPathRemainingDistance(script.NavMeshAgent));
                EditorGUILayout.LabelField("Distacia ubi: " + script.NavMeshAgent.destination);
                EditorGUILayout.LabelField("Path status: " + script.NavMeshAgent.pathStatus);
            }

            if (script.tr!=null)
            {
                EditorGUILayout.LabelField("Arbol status: " + script.tr.tree.treeState);
                EditorGUILayout.LabelField("Arbol nombre?: " + script.tr.tree.rootNode.position);
                EditorGUILayout.LabelField("Arbol Description?: " + script.tr.tree.rootNode.description);
                EditorGUILayout.LabelField("Arbol asd?: " + script.tr.tree.name);
                EditorGUILayout.LabelField("Arbol asd?: " + script.tr.tree.rootNode.guid);
                //script.tr.tree.nodes.Find(n => n.guid ==script.tr.tree.)
            }
        }


        public float GetPathRemainingDistance(NavMeshAgent navMeshAgent)
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