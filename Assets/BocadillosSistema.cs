using System;
using UnityEngine;

public class BocadillosSistema : MonoBehaviour
{
    public static BocadillosSistema _instance;


    public Sprite irACaravana;
    public Sprite irAMuro;
    public Sprite irACatapulta;
    public Sprite irACannon;


    public Sprite irAICM;
    public Sprite irAICMSkin1;
    public Sprite irAICMSkin2;
    public Sprite irAConeman;
    public Sprite irAConemanSkin1;
    public Sprite irAConemanSkin2;
    public Sprite irATorrine;
    public Sprite irATorrineSkin1;
    public Sprite irATorrineSkin2;

    public Sprite irACasa;
    public Sprite irADormir;
    public Sprite NecesitoPaga;

    private const String NOMBRECANNON = "Ice cream cannon";
    private const String NOMBRECATAPULTA = "Cherry catapult";
    private const String NOMBREMURO = "Ice cream wall";

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


    public Sprite GetCasa()
    {
        return irACasa;
    }

    public Sprite Dormir()
    {
        return irADormir;
    }

    public Sprite Reparacion()
    {
        return NecesitoPaga;
    }

    public Sprite GetEdificioCereza(String objetivo)
    {
        switch (objetivo)
        {
            case NOMBREMURO:
                return irAMuro;
            case NOMBRECANNON:
                return irACannon;
            case NOMBRECATAPULTA:
                return irACatapulta;
            default:
                return GetCasa();
        }


        return irACasa;
    }

    public Sprite GetPlayer()
    {
        switch (PlayerStats._instance.PlayerStatsSo.PlayerName)
        {
            case PlayerStatsSO.playerName.ICM:
                return irAICM;
            case PlayerStatsSO.playerName.ICMSkin1:
                return irAICMSkin1;
            case PlayerStatsSO.playerName.ICMSkin2:
                return irAICMSkin2;
            case PlayerStatsSO.playerName.Coneman:
                return irAConeman;
            case PlayerStatsSO.playerName.ConemanSkin1:
                return irAConemanSkin1;
            case PlayerStatsSO.playerName.ConemanSkin2:
                return irAConemanSkin2;
            case PlayerStatsSO.playerName.Torrine:
                return irATorrine;
            case PlayerStatsSO.playerName.TorrineSkin1:
                return irATorrineSkin1;
            case PlayerStatsSO.playerName.TorrineSkin2:
                return irATorrineSkin2;
        }

        return irAICM;
    }


    public Sprite GetSprite(String objetivo)
    {
        switch (objetivo)
        {
            case NOMBREMURO:
                return irAMuro;
            case NOMBRECANNON:
                return irACannon;
            case NOMBRECATAPULTA:
                return irACatapulta;
            default:
                return GetPlayer();
        }


        return irACaravana;
    }
}