using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer soundMixer;
    public AudioMixer musicMixer;
    public Slider musicSlider;
    public Slider audioSlider;
    public bool isMutedAudio = false;
    public bool isMutedMusic = false;
    public float previousAudio = 0.5f;
    public float previousSliderMusic = 0.5f;
    public float previousSliderAudio = 0.5f;
    public float previousMusic = 0.5f;

    public void SetVolumeSounds(float volume)
    {
        if (volume == 0)
        {
            soundMixer.SetFloat("volume", -200);
            return;
        }

        soundMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        previousAudio = Mathf.Log10(volume) * 20;
    }

    public void SetVolumeMusic(float volume)
    {
        if (volume == 0)
        {
            musicMixer.SetFloat("volume", -200);
            return;
        }

        musicMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        previousMusic = Mathf.Log10(volume) * 20;
    }

    public void MuteAudio()
    {
        if (!isMutedAudio)
        {
            isMutedAudio = true;
            previousSliderAudio = audioSlider.value;
            audioSlider.value = 0;
            SetVolumeSounds(0);
        }
        else
        {
            isMutedAudio = false;
            SetVolumeSounds(previousAudio);
            audioSlider.value = previousSliderAudio;
            previousSliderAudio = 0;
        }
    }

    public void MuteMusic()
    {
        if (!isMutedMusic)
        {
            isMutedMusic = true;
            previousSliderMusic = musicSlider.value;
            musicSlider.value = 0;
            SetVolumeMusic(0);
        }
        else
        {
            isMutedMusic = false;
            SetVolumeMusic(previousMusic);
            musicSlider.value = previousSliderMusic;
            previousSliderMusic = 0;
        }
    }
}