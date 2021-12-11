using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerEconomySO", menuName = "Player/PlayerEconomy")]
public class PlayerEconomySO : ScriptableObject
{
    public  float coins;
    public float rings;
    public bool Torrine;
    public bool ConeMan;
    public bool ICM_skin_1;
    public bool ICM_skin_2;
    public bool ConeMan_skin_1;
    public bool ConeMan_skin_2;
    public bool Torrine_skin_1;
    public bool Torrine_skin_2;
    public static float staticCoins;
}
