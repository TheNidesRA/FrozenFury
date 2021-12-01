using System;
using System.Collections;
using UnityEngine;

namespace UI
{
    public class PlayerHealthBar:MonoBehaviour
    {
        private float maxHealth;
        public UnityEngine.UI.Image healtBar;

        private float fixedRotation = 0;

        private Quaternion fixedrot;
        // private GameObject Camera;
        private void Awake()
        {
            // Camera = GameObject.FindWithTag("MainCamera");
            
            // healtBar = GetComponentInChildren<UnityEngine.UI.Image>();

            
            // fixedRotation = gameObject.transform.rotation.y;
            Vector3 initPos = gameObject.transform.position;
            Vector3 aux = initPos;
            Vector3 camPos = Camera.main.transform.position;
            aux.x = camPos.x;
            
            gameObject.transform.position = aux;
            gameObject.transform.LookAt(camPos);
            fixedrot = gameObject.transform.rotation;
            gameObject.transform.position = initPos;
        }

        private void Update()
        {
            gameObject.transform.rotation = fixedrot;

        }

        private void OnDestroy()
        {
            PlayerStats._instance.OnHealthChanged -= HandleHealth;
            PlayerStats._instance.OnDeathEvent -= Regenearate;
        }
        private void Start()
        {
            maxHealth = PlayerStats._instance.GetMaxHealth();
            PlayerStats._instance.OnHealthChanged += HandleHealth;
            PlayerStats._instance.OnDeathEvent += Regenearate;
        }

        private void Regenearate(object sender, float e)
        {
            StartCoroutine(RegenRoutine(e));
        }

        private IEnumerator RegenRoutine(float f)
        {
            float elapsed = 0;
            while (elapsed < f)
            {
                elapsed += Time.deltaTime;
                
                healtBar.fillAmount = Mathf.Lerp(0, 1, elapsed / f);
                yield return null;
            }
        }


        public void HandleHealth(object sender, float hp)
        {
            healtBar.fillAmount = hp / maxHealth;
        }
    }
}