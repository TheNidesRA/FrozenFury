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
    public float previousAudio;
    public float previousSliderMusic;
    public float previousSliderAudio;
    public float previousMusic;
    public float augmentAudioPercentage = 0.5f;
    public float lowerMusicPercentage = 0.3f;

    private void Start()
    {
        if (AudioManager.getAudioVolume() != -1f)
        {
            SetVolumeSounds(AudioManager.getAudioSlider());
        }

        if (AudioManager.getAudioSlider() != -1f)
        {
            ChangeAudioSliderValue(AudioManager.getAudioSlider());
        }

        if (AudioManager.getMusicVolume() != -1f)
        {
            SetVolumeMusic(AudioManager.getMusicSlider());
        }

        if (AudioManager.getMusicSlider() != -1f)
        {
            ChangeMusicSliderValue(AudioManager.getMusicSlider());
        }
    }

    public void SetVolumeSounds(float volume)
    {
        if (volume == 0)
        {
            soundMixer.SetFloat("volume", -200);
            AudioManager.setAudioVolume(0);
            isMutedAudio = true;
            return;
        }

        isMutedAudio = false;
        soundMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        previousAudio = Mathf.Log10(volume) * 20;
        AudioManager.setAudioVolume(previousAudio);
        AudioManager.setAudioSlider(volume);
    }

    public void SetVolumeMusic(float volume)
    {
        if (volume == 0)
        {
            musicMixer.SetFloat("volume", -200);
            AudioManager.setMusicVolume(0);
            isMutedMusic = true;
            return;
        }

        isMutedMusic = false;
        musicMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        previousMusic = Mathf.Log10(volume) * 20;
        AudioManager.setMusicVolume(previousMusic);
        AudioManager.setMusicSlider(volume);
    }

    public void MuteAudio()
    {
        if (!isMutedAudio)
        {
            isMutedAudio = true;
            previousSliderAudio = audioSlider.value;
            audioSlider.value = 0;
            AudioManager.setAudioSlider(0);
            SetVolumeSounds(0);
        }
        else
        {
            isMutedAudio = false;
            SetVolumeSounds(previousSliderAudio);
            audioSlider.value = previousSliderAudio;
            AudioManager.setAudioSlider(audioSlider.value);
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
            AudioManager.setMusicSlider(0);
            SetVolumeMusic(0);
        }
        else
        {
            isMutedMusic = false;
            SetVolumeMusic(previousSliderMusic);
            musicSlider.value = previousSliderMusic;
            AudioManager.setMusicSlider(musicSlider.value);
            previousSliderMusic = 0;
        }
    }

    public void ChangeAudioSliderValue(float value)
    {
        audioSlider.value = value;
    }

    public void ChangeMusicSliderValue(float value)
    {
        musicSlider.value = value;
    }

    public IEnumerator LowerVolumeStartRoundCoroutine()
    {
        previousSliderAudio = audioSlider.value;
        previousSliderMusic = musicSlider.value;
        SetVolumeMusic(previousSliderMusic - (previousSliderMusic * lowerMusicPercentage));
        SetVolumeSounds(previousSliderAudio + (previousSliderAudio * augmentAudioPercentage));
        yield return new WaitForSeconds(3f);
        SetVolumeMusic(previousSliderMusic);
        SetVolumeSounds(previousSliderAudio);
    }

    public void LowerVolumeStartRound()
    {
        if (!isMutedMusic)
            StartCoroutine(nameof(LowerVolumeStartRoundCoroutine));
    }
}