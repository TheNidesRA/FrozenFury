using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAuxFunctions : MonoBehaviour
{
    public void StopGameplayMusic()
    {
        AudioManager.Instance?.Stop("Partida");
    }

    public void StopWinMusic()
    {
        AudioManager.Instance?.Stop("Ganar Partida");
    }

    public void StopLoseMusic()
    {
        AudioManager.Instance?.Stop("Perder Partida");
    }

    public void PlayGameplayMusic()
    {
        AudioManager.Instance?.Play("Partida");
    }


    public void PlayMainMenuMusic()
    {
        AudioManager.Instance?.Play("Menu Principal");
    }

    public void StopMainMenuMusic()
    {
        AudioManager.Instance?.Stop("Menu Principal");
    }

    public void NormalClick()
    {
        AudioManager.Instance?.Play("Boton Normal");
    }

    public void HeladoClick()
    {
        AudioManager.Instance?.Play("Boton Jugar Helado");
    }

    public void PlayStartRound()
    {
        AudioManager.Instance?.Play("Empezar Ronda");
    }

    public void PlayFinishRound()
    {
        AudioManager.Instance?.Play("Finalizar Ronda");
    }

    public void PlayRandomBuy()
    {
        AudioManager.Instance?.PlayRandomBuy();
    }

    public void PlayRandomNewHitSound()
    {
        AudioManager.Instance?.PlayRandomNewHitSound();
    }

    public void PlayWinMusic()
    {
        AudioManager.Instance?.Play("Ganar Partida");
    }

    public void PlayLoseMusic()
    {
        AudioManager.Instance?.Play("Perder Partida");
    }

    public void PlayChillMusic()
    {
        AudioManager.Instance?.Play("Chill Theme");
    }

    public void StopChillMusic()
    {
        AudioManager.Instance?.Stop("Chill Theme");
    }
}