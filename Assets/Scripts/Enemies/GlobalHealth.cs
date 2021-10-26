using System;
using UnityEngine;

class GlobalHealth : MonoBehaviour
{
    private int globalHealth = 20;

    public static GlobalHealth instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void DecreaseHealth()
    {
        globalHealth--;
    }
}