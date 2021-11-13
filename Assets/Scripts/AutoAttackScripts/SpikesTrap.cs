using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class SpikesTrap : MonoBehaviour
{

    public float dmg = 5;
    public float slowDown = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag($"Enemy"))
        {
            Debug.Log("Estoy comprobando");
            collision.gameObject.GetComponent<Enemy>().OnSlow(slowDown);
            StartCoroutine(collision.gameObject.GetComponent<Enemy>().OnHitTrap(dmg));

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        
        if (collision.gameObject.CompareTag($"Enemy"))
        {
            StopCoroutine(collision.gameObject.GetComponent<Enemy>().OnHitTrap(dmg));
            collision.gameObject.GetComponent<Enemy>().OnResetSlow();

        }

    }
}
