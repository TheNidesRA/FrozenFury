using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class SpikesTrap : PlacedBuild
{

    public float dmg;
    public float health_;


    [Range(0f, 1f)]
    public float slowDown = 0.5f;

    private void Awake()
    {
        dmg = this.BuildingSo.damage;
        health_ = this.BuildingSo.health;
    }

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
