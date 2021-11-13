using System.Collections;
using System.Collections.Generic;
using Enemies;
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

            other.GetComponent<Enemy>().OnSlow(slowDown);
            other.GetComponent<Enemy>().OnHitTrap(dmg);

        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag($"Enemy"))
        {

            other.GetComponent<Enemy>().OnHitTrap(dmg);

        }

    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.CompareTag($"Enemy"))
        {

            other.GetComponent<Enemy>().OnResetSlow();

        }

    }
}
