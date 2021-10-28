using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats _instance { get; private set; }

    public event EventHandler OnGoldChanged;

    [SerializeField] private float _gold;

    public float gold
    {
        get => _gold;
        set  {
            _gold = value;
            OnGoldChanged?.Invoke(_gold,EventArgs.Empty);
        }
    }


    private void Awake()
    {
        if (_instance != null && _instance!=this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

 
    
    
}
