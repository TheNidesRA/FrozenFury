
using UnityEngine;

namespace Enemies
{
    public class EnemyGoal : MonoBehaviour
    {
        
        public static EnemyGoal instance { get; private set; }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }
        
        
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Enemy")) return;
            
            //Bajamos la vida global
            try{GlobalHealth.instance.DecreaseHealth();}
            catch{Debug.Log("Bro, you need to introduce a globalHealth controller.");}

            //Destruimos al enemigo que ha entrado
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.Die();
        }

        public Vector3 getPosition()
        {
            return gameObject.GetComponent<Transform>().position;
        }
    }
}