using DG.Tweening;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    [SerializeField] Sound[] listSound;

    public static SoundManager Instance 
    { 
        get
        {
           if(instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
                
            }
            return instance;
        }
    }
    private void Awake()
    {
        foreach(Sound sound in listSound) 
        { 
            AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
            audioSource.loop = sound.IsLoop;  
            audioSource.pitch = sound.Pitch;
            audioSource.clip = sound.AudioClip;
            audioSource.playOnAwake = false;

            if (sound.Type == SoundType.Sfx)
            {
                audioSource.volume = sound.Volume * GameManager.Instance.SettingManager.SoundVolume;
            }
            else
            {
                audioSource.volume = sound.Volume * GameManager.Instance.SettingManager.MusicVolume;
            }

            sound.audioSource = audioSource;
        }
    }

    public void SetSoundVolum(float _volume)
    {
        foreach (Sound sound in listSound)
        {
            Debug.Log(sound.Volume * _volume);

            if (sound.Type == SoundType.Sfx)
            {
                sound.audioSource.volume = sound.Volume * _volume;
            }

        }
    }

    public void SetMusicVolume(float _volume)
    {
        foreach (Sound sound in listSound)
        {
            Debug.Log(sound.Volume * _volume);
            if (sound.Type == SoundType.Music)
            {
                sound.audioSource.volume = sound.Volume * _volume;
            }
            
            
        }
    }
    public void Play(string _name)
    {
        Sound sound = Array.Find<Sound>(listSound, sound => sound.Name == _name);
        if (sound.audioSource == null || sound.audioSource.isPlaying) return;
        sound.audioSource.Play();
    }

    public void PlayDelay(string _name , float _delayTime)
    {
        Sound sound = Array.Find<Sound>(listSound, sound => sound.Name == _name);
        if (sound.audioSource == null || sound.audioSource.isPlaying) return;
        sound.audioSource.PlayDelayed(_delayTime);
    }

    public void PlayMusic(string _name , float _durationFade)
    {
        Sound sound = Array.Find<Sound>(listSound, sound => sound.Name == _name);
        if (sound.audioSource == null || sound.audioSource.isPlaying) return;
        sound.audioSource.Play();
    }

    public void Stop(string _name)
    {
        AudioSource source = Array.Find<Sound>(listSound, sound => sound.Name == _name).audioSource;
        if (!source.isPlaying) return;
        source.Stop();
    }



    
}
