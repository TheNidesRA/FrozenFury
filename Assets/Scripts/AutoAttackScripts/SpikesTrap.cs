using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesTrap : MonoBehaviour
{

    public float dmg = 5;
    public float slowDown = 1;


    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag($"Enemy"))
        {

            StartCoroutine(OnSpikes());

        }


    }

    private IEnumerator OnSpikes()
    {

        while (true)
        {

            //LLAMADA A METODO DE DAÑO


        }


    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.CompareTag($"Enemy"))
        {

            StopCoroutine(OnSpikes());

        }

    }
}
