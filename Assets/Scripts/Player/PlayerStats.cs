using System;
using System.Collections;
using AutoAttackScripts;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats _instance { get; private set; }

    public event EventHandler<float> OnGoldChanged;
    public event EventHandler<int> OnLvlChanged;
    public event EventHandler<float> OnDeathEvent;

    public event EventHandler<float> OnHealthChanged;

    [SerializeField] private float _gold;

    [SerializeField] private float damage = 0;

    [SerializeField] private int _level = 1;

    [SerializeField] private float _health = 10;

    [SerializeField] private float _maxHealth = 100;

    [SerializeField] private float _respawnTime = 5;

    public AutoShoot Shoot;

    private bool _isAlive = true;

    public Animator Animator;


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
            OnHealthChanged?.Invoke(this, _health);
            if (_health <= 0)
            {
                if (_isAlive)
                {
                    OnDeathEvent?.Invoke(this, _respawnTime);
                    Dead();
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

        Animator = gameObject.GetComponent<Animator>();
    }

    public void Dead()
    {
        GetComponent<nuevoPlayerMovement>().enabled = false;
        Shoot.StartStopShooting();
        Animator.SetLayerWeight(Animator.GetLayerIndex("Dead"), 1f);
        Animator.SetLayerWeight(Animator.GetLayerIndex("Base Layer"), 0f);
        Animator.SetLayerWeight(Animator.GetLayerIndex("Shooting"), 0f);
        Animator.SetBool("Dead", true);
        AudioManager.Instance?.PlayRandomDeath();
        StartCoroutine(Respawn(_respawnTime));
    }

    public IEnumerator Respawn(float secs)
    {
        yield return new WaitForSeconds(secs);
        Revive();
    }

    [ContextMenu("Revivir papa")]
    public void Revive()
    {
        Animator.SetLayerWeight(Animator.GetLayerIndex("Base Layer"), 1f);
        Animator.SetBool("Dead", false);
        GetComponent<nuevoPlayerMovement>().enabled = true;
        Shoot.StartStopShooting();
        Health = _maxHealth;
        _isAlive = true;
    }

    public bool IsPlayerAlive()
    {
        return _health > 0;
    }

    public float GetMaxHealth()
    {
        return _maxHealth;
    }

    [ContextMenu("DamagePlayer")]
    public void DamagePlayer()
    {
        Health -= 10;
    }
}