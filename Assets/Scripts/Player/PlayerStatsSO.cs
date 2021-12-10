using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    public float initHealth;
    public float initDamage;
    public float initAttackSpeed;
    public int initGold;
    public int initUpgradeCostGold;


    public float MAXHEALTH;
    public float MAXDAMAGE;
    public float MAXATTACKSPEED;
    public int MAXGOLDUPGRADECOST;

    public string name;
    public Transform CanvasModel;
    public playerName PlayerName;

    public enum playerName
    {
        Coneman,
        ConemanSkin1,
        ConemanSkin2,
        Torrine,
        TorrineSkin1,
        TorrineSkin2,
        ICM,
        ICMSkin1,
        ICMSkin2
    }
}