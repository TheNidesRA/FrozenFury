using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class SpikesTrap : MonoBehaviour
{

    public float dmg = 5;
    public float slowDown = 2;

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag($"Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().OnSlow(slowDown);
           

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag($"Enemy"))
        {

            collision.gameObject.GetComponent<Enemy>().OnHitTrap(dmg);

        }
    }


    private void OnCollisionExit(Collision collision)
    {
        
        if (collision.gameObject.CompareTag($"Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().OnResetSlow();
        }

    }
}
