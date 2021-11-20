using System.Collections;
using System.Collections.Generic;
using Enemies;
using GridSystem;
using UnityEngine;

public class SpikesTrap : PlacedBuild
{
    public bool invencibilidadTrampa = false;


    [Range(0f, 1f)] public float slowDown = 0.2f;


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
            collision.gameObject.GetComponent<Enemy>().OnHitTrap(_damage);
            hitTrap(_damage, collision);
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
        if (!invencibilidadTrampa && _health > 0)
        {
            _health = _health - 1;
            StartCoroutine(OnEnableTrap());
        }

        if (_health <= 0)
        {
            destroySpikes();
            collision.gameObject.GetComponent<Enemy>().OnResetSlow();
        }
    }

    public IEnumerator OnEnableTrap()
    {
        invencibilidadTrampa = true;
        yield return new WaitForSeconds(1/attackSpeed);
        invencibilidadTrampa = false;
    }

    private void destroySpikes()
    {
        GridBuildingSystem.Instance.RemoveBuild(this);
    }
}