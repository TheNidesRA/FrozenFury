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
            
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            //Bajamos la vida global
            try
            {
                GlobalHealth.instance.DecreaseHealth(enemy.baseDamage);
                AudioManager.Instance?.Play("Golpe Furgoneta");
            }
            catch
            {
                Debug.Log("Bro, you need to introduce a globalHealth controller.");
            }

            //Add the enemy remaining stats to the DDA
            try
            {
                DDACalculator.instance.AddWinner(enemy);
            }
            catch
            {
                Debug.LogError("Instance of DDACalculator missing");
            }

            //Destroy the enemy 
            enemy.DieNoAnim();
        }

        public Vector3 getPosition()
        {
            return gameObject.transform.position;
        }
    }
}