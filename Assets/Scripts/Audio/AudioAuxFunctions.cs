using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAuxFunctions : MonoBehaviour
{
    public void StopGameplayMusic()
    {
        AudioManager.Instance.Stop("Partida");
    }

    public void PlayGameplayMusic()
    {
        AudioManager.Instance.Play("Partida");
    }


    public void PlayMainMenuMusic()
    {
        AudioManager.Instance.Play("Menu Principal");
    }

    public void StopMainMenuMusic()
    {
        AudioManager.Instance.Stop("Menu Principal");
    }

    public void NormalClick()
    {
        AudioManager.Instance.Play("Boton Normal");
    }

    public void HeladoClick()
    {
        AudioManager.Instance.Play("Boton Jugar Helado");
    }
}