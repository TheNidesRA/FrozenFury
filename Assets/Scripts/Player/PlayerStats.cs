using System;
using System.Collections;
using AutoAttackScripts;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PlayerStats : MonoBehaviour
{
    public static PlayerStats _instance { get; private set; }

    public event EventHandler<float> OnGoldChanged;
    public event EventHandler<int> OnLvlChanged;
    public event EventHandler<float> OnDeathEvent;

    public event EventHandler<float> OnHealthChanged;

    public PlayerStatsSO PlayerStatsSo;

    [SerializeField] private int _gold;


    [SerializeField] private float _damage = 0;

    public const int MAXLEVEL = 15;

    [SerializeField] protected float _initDamage;
    [SerializeField] protected float _initAttackSpeed;
    [SerializeField] protected float _initHealth;
    [SerializeField] protected int _initGoldCostLevel;


    [SerializeField] private int _level = 1;
    [SerializeField] private float _health = 10;
    [SerializeField] private float _maxHealth;
    public event EventHandler<float> OnMaxHealthChanged;
    public float maxHealth
    {
        get => _maxHealth;
        set
        { 
            OnMaxHealthChanged?.Invoke(this, value);
            _maxHealth = value;
        }
    }

    [SerializeField] private float _attackSpeed;
    [SerializeField] public int _goldLevelCost{ get; private set; }


    public AnimationCurve CurveHealth;
    public AnimationCurve CurveDamage;
    public AnimationCurve CurveAttackSpeed;
    public AnimationCurve CurveGoldLevelCost;

    public float MAXHEALTH;
    public float MAXDAMAGE;
    public float MAXATTACKSPEED;
    public int MAXGOLDCOSTLEVEL;


    [SerializeField] private float _respawnTime = 5;
    public AutoShoot Shoot;

    private bool _isAlive = true;

    public Animator Animator;


    public float Damage
    {
        get => _damage;
        set
        {
            Shoot.DamagePerBullet = value;
            _damage = value;
        }
    }

    public int gold
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
            Evaluate();
            OnLvlChanged?.Invoke(this, _level);
        }
    }

    public float attackSpeed
    {
        get => _attackSpeed;
        set
        {
            Shoot.shootsPerSecond = value;
            _attackSpeed = value;
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

        InitStats();
    }


    private void Start()
    {
        CurveHealth = AnimationCurve.EaseInOut(1, _initHealth, MAXLEVEL, MAXHEALTH);
        CurveDamage = AnimationCurve.EaseInOut(1, _initDamage, MAXLEVEL, MAXDAMAGE);
        CurveAttackSpeed = AnimationCurve.EaseInOut(1, _initAttackSpeed, MAXLEVEL, MAXATTACKSPEED);
        CurveGoldLevelCost = AnimationCurve.EaseInOut(1, _initGoldCostLevel, MAXLEVEL, MAXGOLDCOSTLEVEL);

        Level = 1;
    }


    private void InitStats()
    {
        _gold = PlayerStatsSo.initGold;

        _initDamage = PlayerStatsSo.initDamage;
        _initHealth = PlayerStatsSo.initHealth;
        _initAttackSpeed = PlayerStatsSo.initAttackSpeed;
        _initGoldCostLevel = PlayerStatsSo.initUpgradeCostGold;

        Damage = _initDamage;
        Health = _initHealth;
        attackSpeed = _initAttackSpeed;


        MAXDAMAGE = PlayerStatsSo.MAXDAMAGE;
        MAXHEALTH = PlayerStatsSo.MAXHEALTH;
        MAXATTACKSPEED = PlayerStatsSo.MAXATTACKSPEED;
        MAXGOLDCOSTLEVEL = PlayerStatsSo.MAXGOLDUPGRADECOST;
    }


    public void Dead()
    {
        GetComponent<nuevoPlayerMovement>().enabled = false;
        Shoot.StartStopShooting();
        Animator.SetLayerWeight(Animator.GetLayerIndex("Dead"), 1f);
        Animator.SetLayerWeight(Animator.GetLayerIndex("Base Layer"), 0f);
        Animator.SetLayerWeight(Animator.GetLayerIndex("Shooting"), 0f);
        Animator.SetBool("Dead", true);
        Animator.SetBool("Shoot", false);
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


    public void LevelingUp()
    {
        if ((_gold >= _goldLevelCost))
        {
            gold -= _goldLevelCost;
            Level++;
        }
    }

    private void Evaluate()
    {
        Damage = CurveDamage.Evaluate(Level);
        attackSpeed = CurveAttackSpeed.Evaluate(Level);
        Health = CurveHealth.Evaluate(Level);
        _goldLevelCost = (int) CurveGoldLevelCost.Evaluate(Level);
        _maxHealth = CurveHealth.Evaluate(Level);

    }


    [ContextMenu("DamagePlayer")]
    public void DamagePlayer()
    {
        Health -= 10;
    }
}