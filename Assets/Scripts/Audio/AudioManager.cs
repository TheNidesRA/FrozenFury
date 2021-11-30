using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Sound[] randomHits;
    public Sound[] randomConstruction;
    public Sound[] randomDeath;
    public Sound[] randomBuy;
    public Sound[] randomNewHits;
    public static AudioManager Instance { get; private set; }


    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.output;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (var s in randomHits)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.output;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (var s in randomConstruction)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.output;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (var s in randomDeath)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.output;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (var s in randomBuy)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.output;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (var s in randomNewHits)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.output;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }


    private void Start()
    {
        Play("Menu Principal");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            //Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            //Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        s.source.Stop();
    }

    public void PlayRandomHitSound()
    {
        Sound s = randomHits[UnityEngine.Random.Range(0, randomHits.Length)];
        s.source.Play();
    }

    public void PlayRandomConstruction()
    {
        Sound s = randomConstruction[UnityEngine.Random.Range(0, randomConstruction.Length)];
        s.source.Play();
    }

    public void PlayRandomDeath()
    {
        Sound s = randomDeath[UnityEngine.Random.Range(0, randomDeath.Length)];
        s.source.Play();
    }

    public void PlayRandomBuy()
    {
        Sound s = randomBuy[UnityEngine.Random.Range(0, randomBuy.Length)];
        s.source.Play();
    }

    public void PlayRandomNewHitSound()
    {
        Sound s = randomNewHits[UnityEngine.Random.Range(0, randomNewHits.Length)];
        s.source.Play();
    }
}