using System;
using UnityEngine;
[Serializable]
public class Sound 
{
    [SerializeField] SoundName soundName; 
    [SerializeField] AudioClip audioClip;
    [SerializeField] float volume;
    [SerializeField] float pitch;
    [SerializeField] bool isLoop;
    [SerializeField] float speed = 1f;
    public AudioSource audioSource;
    public string SoundName
    {
        get
        {
            return soundName.ToString();
        }
    }
    public AudioClip AudioClip { get => audioClip;  }
    public float Volume { get => volume;}
    public float Pitch { get => pitch; }
    public bool IsLoop { get => isLoop;  }
    public float Speed { get => speed;  }
}
[Serializable]
public enum SoundName
{
    MouseClick,
    Sword,
    SwordHit,
    FootStepGrass,
    Hurt,
    Die,
    Slide,
    Correct,
    Incorrect,
    Background,
    MouseClick2,
    YasuoDeath,
    YasuoQ2,
    YasuoQ3,
    YasuoR,
    YasuoCast1,
    YasuoCast2,
    YasuoCast3
}