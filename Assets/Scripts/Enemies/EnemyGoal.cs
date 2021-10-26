
using UnityEngine;

namespace Enemies
{
    public class EnemyGoal : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Enemy")) return;
            
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.Die();
        }
    }
}