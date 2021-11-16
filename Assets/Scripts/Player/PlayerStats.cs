using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats _instance { get; private set; }

    public event EventHandler<float> OnGoldChanged;
    public event EventHandler<int> OnLvlChanged;
    public event EventHandler<bool> OnDeathEvent;

    [SerializeField] private float _gold;

    [SerializeField] private float damage = 0;

    [SerializeField] private int _level = 1;

    [SerializeField] private float _health = 10;
    
    private bool _isAlive = true;
    

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
    
    public int Level
    {
        get => _level;
        set
        {
            _level = value;
            OnLvlChanged?.Invoke(this, _level);
        }
    }
    
    public float Health
    {
        get => _health;
        set
        {
            _health = value;
            if (_health <= 0)
            {
                if (_isAlive)
                {
                    OnDeathEvent?.Invoke(this, _isAlive);
                }
                _isAlive = false;
            }
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

    public bool IsPlayerAlive()
    {
        return _health > 0;
    }
}