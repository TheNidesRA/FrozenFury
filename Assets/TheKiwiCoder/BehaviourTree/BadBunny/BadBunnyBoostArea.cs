using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class BadBunnyBoostArea : MonoBehaviour
{
    // Start is called before the first frame update
    private const string TAG = "Enemy";
    [Range(1f, 2f)] public float boost;

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Saltaa eventop");
        if (other.gameObject.tag == TAG)
            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.OnSpedUp(boost);
            }
    }

    private void OnCollisionExit(Collision other)
    {
        Debug.Log("Salee eventop");
        if (other.gameObject.tag == TAG)
            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.OnResetSlow();
            }
    }
}