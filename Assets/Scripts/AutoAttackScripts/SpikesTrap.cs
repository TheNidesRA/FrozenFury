using System.Collections;
using System.Collections.Generic;
using Enemies;
using GridSystem;
using UnityEngine;

public class SpikesTrap : PlacedBuild
{

    public float dmg;
    public float health_;
    public bool invencibilidadTrampa = false;
    public float tiempoInv = 1f;


    [Range(0f, 1f)]
    public float slowDown = 0.5f;

    private void Start()
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
            hitTrap(dmg, collision);
            
            


        }
    }


    private void OnCollisionExit(Collision collision)
    {
        
        if (collision.gameObject.CompareTag($"Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().OnResetSlow();
        }

    }
    
    private void hitTrap(float daño, Collision collision)
    {

        if (!invencibilidadTrampa && health_ > 0)
        {
            health_ = health_ - daño;
            StartCoroutine(OnEnableTrap());
        }
       
        if (health_ <= 0)
        {
            destroySpikes();
            collision.gameObject.GetComponent<Enemy>().OnResetSlow();
        }
    }

    public IEnumerator OnEnableTrap()
    {
        invencibilidadTrampa = true;
        yield return new WaitForSeconds(tiempoInv);
        invencibilidadTrampa = false;
    }

    private void destroySpikes()
    {
        GridBuildingSystem.Instance.RemoveBuild(this);
    }


}
