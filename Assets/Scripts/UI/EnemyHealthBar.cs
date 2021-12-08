using System.Collections;
using Enemies;
using UnityEngine;

namespace UI
{
    public class EnemyHealthBar : MonoBehaviour
    {
        private float maxHealth;
        public UnityEngine.UI.Image healtBar;

        private float fixedRotation = 0;

        private float secondsOnScreen = 2;

        private Quaternion fixedrot;

        private Canvas _canvas;

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
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = false;
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
        }

        private void HandleHealth(object e, float health)
        {
            if (health <= 0)
            {
                _canvas.enabled = false;
                return;
            }

            _canvas.enabled = true;
            healtBar.fillAmount = health / maxHealth;
            try
            {
                StopCoroutine("DisableBar");
            }
            catch
            {
                Debug.LogError("Routine not initialized yet");
            }

            StartCoroutine("DisableBar", DisableBar());
        }

        private IEnumerator DisableBar()
        {
            yield return new WaitForSeconds(secondsOnScreen);
            _canvas.enabled = false;
        }
    }
}