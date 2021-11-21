using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer soundMixer;
    public AudioMixer musicMixer;

    public void SetVolumeSounds(float volume)
    {
        if (volume == 0)
        {
            soundMixer.SetFloat("volume", 0);
            return;
        }

        soundMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void SetVolumeMusic(float volume)
    {
        if (volume == 0)
        {
            soundMixer.SetFloat("volume", 0);
            return;
        }

        musicMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }
}