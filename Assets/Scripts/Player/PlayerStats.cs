using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats _instance { get; private set; }

    public event EventHandler<float> OnGoldChanged;

    [SerializeField] private float _gold;

    [SerializeField] private float damage;

    public float Damage
    {
        get => damage;
        set => damage = value;
    }

    public float gold
    {
        get => _gold;
        set
        {
            _gold = value;
            OnGoldChanged?.Invoke(this, _gold);
        }
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}