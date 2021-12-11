using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySingeltone : MonoBehaviour
{
    // Start is called before the first frame update
    public static MoneySingeltone Instance;

    public PlayerEconomySO PlayerEconomySo;

 


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}