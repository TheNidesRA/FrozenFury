using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{
    public ComunicacionGridCanvas cgc;
    private void OnCollisionEnter(Collision other)
    {
       
        if (other.gameObject.CompareTag("Player"))
        {
          
            cgc.ShowPlayerStats();
        }
        
    }

    private void OnCollisionExit(Collision other)
    {


        if (other.gameObject.CompareTag("Player"))
        {
            cgc.HidePlayerStats();
        }
      
    }
}
