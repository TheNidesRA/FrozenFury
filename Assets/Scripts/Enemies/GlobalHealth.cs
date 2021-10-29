using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class GlobalHealth : MonoBehaviour
{
    [SerializeField]
    private int _globalHealth = 20;
    public event EventHandler<int> OnHealthChange;

    public int globalHealth
    {
        get => _globalHealth;
        set
        {
            _globalHealth = value;
            OnHealthChange?.Invoke(this,_globalHealth);
        }
    }

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
        if (globalHealth <= 0)
        {
            EndGameFunc();
        }
    }

    private void EndGameFunc()
    {
        Debug.Log("GAME OVER");
        SceneController._instance.GoToLoseScene();
    }
}