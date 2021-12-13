using System;
using UnityEngine;
using UnityEngine.AI;

namespace UtilityBehaviour
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        private System.Action MoveToCallback;

        public bool reached = false;

        private void Awake()
        {
            reached = false;
            _agent.ResetPath();
            ;
            _agent.isStopped = false;
        }

        public void MoveTo(Vector3 position)
        {
            _agent.SetDestination(position);
            _agent.isStopped = false;
            //reached = false;
        }

        public void MoveTo(GameObject position)
        {
            _agent.SetDestination(position.transform.position);
            _agent.isStopped = false;
            //reached = false;
        }

        public void MoveTo(Transform position)
        {
            _agent.SetDestination(position.position);
            _agent.isStopped = false;
            //reached = false;
        }


        public void MoveTo(Vector3 position, System.Action callback)
        {
            _agent.SetDestination(position);
            _agent.isStopped = false;
            MoveToCallback = callback;
            //reached = false;
        }

        public void MoveTo(GameObject position, System.Action callback)
        {
            _agent.SetDestination(position.transform.position);
            _agent.isStopped = false;
            MoveToCallback = callback;
            reached = false;
        }

        public void MoveTo(Transform position, System.Action callback)
        {
            _agent.SetDestination(position.position);
            _agent.isStopped = false;
            MoveToCallback = callback;
            reached = false;
        }


        private void Update()
        {
            if (_agent.hasPath && Vector3.Distance(transform.position, _agent.destination) <= 10 && !reached)
            {
                // MoveToCallback();
                Debug.Log("hemos llegao");
                reached = true;
                _agent.isStopped = true;
                _agent.ResetPath();
                ;
            }
         
        }
    }
}