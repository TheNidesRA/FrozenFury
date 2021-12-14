using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityBehaviour;

namespace UI
{
    public class CherryEnergyBar : MonoBehaviour
    {
        public UnityEngine.UI.Image energyBar;
        public UnityEngine.UI.Image durabilityBar;

        private Quaternion fixedrot;

        private Canvas _canvas;
        private NPCController _cherry;
        
        private float maxTimeWorked;
        private float maxToolDurability;

        private void Awake()
        {
            Vector3 initPos = gameObject.transform.position;
            Vector3 aux = initPos;
            Vector3 camPos = Camera.main.transform.position;
            aux.x = camPos.x;
            _cherry = GetComponentInParent<NPCController>();

            gameObject.transform.position = aux;
            gameObject.transform.LookAt(camPos);
            fixedrot = gameObject.transform.rotation;
            gameObject.transform.position = initPos;
            _cherry.OnTimeWorkedChanged += HandleEnergy;
            _cherry.OnDurabilityChanged += HandleDurability;
        }

        private void Update()
        {
            gameObject.transform.rotation = fixedrot;
        }

        private void OnEnable()
        {
            _cherry.OnTimeWorkedChanged += HandleEnergy;
            _cherry.OnDurabilityChanged += HandleDurability;
            maxTimeWorked = NPCController.MAXFATIGUE;
            maxToolDurability = NPCController.MAXTOOLDURABILITY;
        }

        private void OnDisable()
        {
            //Esto tira fallo si se para la partida y hay enemigos en pantalla
            _cherry.OnTimeWorkedChanged -= HandleEnergy;
            _cherry.OnDurabilityChanged -= HandleDurability;
        }

        private void HandleEnergy(object e, float timeWorked)
        {
            energyBar.fillAmount = 1 - timeWorked / maxTimeWorked;
           
        }
        
        private void HandleDurability(object sender, float durability)
        {
            durabilityBar.fillAmount = durability / maxToolDurability;
        }

    }
}