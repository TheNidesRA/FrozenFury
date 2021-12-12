using UnityEngine;
using UnityEngine.AI;

namespace UtilityBehaviour
{
    public class MoveController : MonoBehaviour
    {
         [SerializeField] private NavMeshAgent _agent;

         public void MoveTo(Vector3 position)
         {
             _agent.SetDestination(position);
         }
         public void MoveTo(GameObject position)
         {
             _agent.SetDestination(position.transform.position);
         }

         public void MoveTo(Transform position)
         {
             _agent.SetDestination(position.position);
         }
    }
}