using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]

public class Sound
{
    [SerializeField] string name;
    [SerializeField] AudioClip audioClip;
    [Range(0f, 2f)]
    [SerializeField] float volume = 1f;
    [Range(0f,2f)]
    [SerializeField] float pitch = 1f;
    [SerializeField] bool isLoop = false;
    [SerializeField] SoundType type;
    public AudioSource audioSource;
    public float Volume { get => volume;}
    public float Pitch { get => pitch; }
    public bool IsLoop { get => isLoop;  }
    public string Name { get => name;  }
    public AudioClip AudioClip { get => audioClip;}
    internal SoundType Type { get => type;}
}

[Serializable]
enum SoundType
{
    Sfx,
    Music
}


