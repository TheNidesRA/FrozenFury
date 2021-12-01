using System;
using Enemies;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.XR;

namespace UI
{
    public class EnemyHealthBar : MonoBehaviour
    {
        private float maxHealth;
        public UnityEngine.UI.Image healtBar;

        private float fixedRotation = 0;

        private Quaternion fixedrot;

        private void Awake()
        {
            Vector3 initPos = gameObject.transform.position;
            Vector3 aux = initPos;
            Vector3 camPos = Camera.main.transform.position;
            aux.x = camPos.x;
            
            gameObject.transform.position = aux;
            gameObject.transform.LookAt(camPos);
            fixedrot = gameObject.transform.rotation;
            gameObject.transform.position = initPos;
            GetComponentInParent<Enemy>().HealthBarEvent += HandleHealth;
        }
        
        private void Update()
        {
            gameObject.transform.rotation = fixedrot;
        }

        private void OnEnable()
        {
            maxHealth = GetComponentInParent<Enemy>().maxHealth;
        }

        private void OnDisable()
        {
            //Esto tira fallo si se para la partida y hay enemigos en pantalla
            GetComponentInParent<Enemy>().HealthBarEvent -= HandleHealth;
            Debug.Log("Me desuscribo illo");
        }

        private void HandleHealth(object e, float health)
        {
            healtBar.fillAmount = health / maxHealth;
        }

    }
}