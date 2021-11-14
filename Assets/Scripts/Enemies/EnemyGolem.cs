using System;
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
      

        private void Start()
        {
            NavMeshAgent.updateRotation = true;
           // targetPosition=EnemyGoal.instance.transform.position;
           // actionTarget = EnemyGoal.instance.gameObject;
           // NavMeshAgent.destination = EnemyGoal.instance.transform.position;
        }

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
                EditorGUILayout.LabelField("Path Activo? : " + script.NavMeshAgent.isStopped);
            }

            // if (script.tr!=null)
            // {
            //     EditorGUILayout.LabelField("Arbol status: " + script.tr.tree.treeState);
            //     EditorGUILayout.LabelField("Arbol nombre?: " + script.tr.tree.rootNode.position);
            //     EditorGUILayout.LabelField("Arbol Description?: " + script.tr.tree.rootNode.description);
            //     EditorGUILayout.LabelField("Arbol asd?: " + script.tr.tree.name);
            //     EditorGUILayout.LabelField("Arbol asd?: " + script.tr.tree.rootNode.guid);
            //     //script.tr.tree.nodes.Find(n => n.guid ==script.tr.tree.)
            // }
            
            
             Handles.color = Color.white;
                    Handles.DrawWireArc(script.transform.position, Vector3.up, Vector3.forward, 360, script.radioVision);
            
                    Vector3 viewAngle01 = DirectionFromAngle(script.transform.eulerAngles.y, -script.angleVision / 2);
                    Vector3 viewAngle02 = DirectionFromAngle(script.transform.eulerAngles.y, script.angleVision / 2);
            
                    Handles.color = Color.yellow;
                    Handles.DrawLine(script.transform.position, script.transform.position + viewAngle01 * script.radioVision);
                    Handles.DrawLine(script.transform.position, script.transform.position + viewAngle02 * script.radioVision);
            
                    if (script.actionTarget!=null)
                    {
                        Handles.color = Color.green;
                        Handles.DrawLine(script.transform.position, script.actionTarget.transform.position);
                    }
            
            
            
            
            
        }


        
        private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
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