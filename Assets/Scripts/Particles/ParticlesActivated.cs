using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesActivated : MonoBehaviour
{
    public bool particleActive = false;
    // Start is called before the first frame update
    void Start()
    {

        if (PlayerPrefs.GetInt("particlesActivated") == 0)//Cuando sea false
        {
            particleActive = false;
        }

        if (PlayerPrefs.GetInt("particlesActivated") == 1)  //Cuando sea true
        {
            particleActive = true; 
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void particulasActivas()
    {
        PlayerPrefs.SetInt("particlesActivated", 1);
        particleActive = true;
    }

    public void particulasDesactivadas()
    {
        PlayerPrefs.SetInt("particlesActivated", 0);
        particleActive = false;
    }

}
