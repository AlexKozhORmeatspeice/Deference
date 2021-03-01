using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;

public class AudioManager : Singleton<AudioManager>
{
    public Sound[] sounds;
    public AudioMixerGroup effects;
    private bool isActiveEffects = true;

    public bool IsActiveEffects
    {
        get { return isActiveEffects; }
        set
        {
            isActiveEffects = value;
            effects.audioMixer.SetFloat("master", isActiveEffects ? 0 : -80);
            //PlayerPrefs.SetInt("effects", isActiveEffects ? 1 : 0);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        foreach (Sound a in sounds)
        {
            a.source = gameObject.AddComponent<AudioSource>();
            a.source.clip = a.clip;

            a.source.volume = a.volume;
            a.source.pitch = a.pitch;
            a.source.outputAudioMixerGroup = a.audioMixerGroup;
            a.source.playOnAwake = false;
        }
        Play("music");
    }

    public void Play(string name)
    {
        if (!IsActiveEffects) return;
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.pitch = UnityEngine.Random.Range(0.6f, 1.1f);
        s.source.Play();
    }
}