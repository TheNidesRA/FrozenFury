using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ParticlesActivated : MonoBehaviour
    {
        public bool particleActive;
        public Toggle toggle;
        // Start is called before the first frame update
        void Awake()
        {
            
            
            if (PlayerPrefs.GetInt("particlesActivated") == 0)//Cuando sea false
            {
                particleActive = false;
                toggle.isOn = false;
            }
            else if (PlayerPrefs.GetInt("particlesActivated") == 1)  //Cuando sea true
            {
                particleActive = true;
                toggle.isOn = true;
            }

        }
        // Update is called once per frame
        void Update()
        {

        }

        public void particulasOnEnabledOnDisabled()
        {
            
            particleActive = toggle.isOn;

            if (particleActive == true)
                PlayerPrefs.SetInt("particlesActivated", 1);
            else
                PlayerPrefs.SetInt("particlesActivated", 0);
           
        }

    }
}

