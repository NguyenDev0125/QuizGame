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
        foreach(var sound in listSound)
        {
            AudioSource source =  gameObject.AddComponent<AudioSource>();
            source.loop = sound.IsLoop;
            source.clip = sound.AudioClip;
            source.volume = sound.Volume;
            source.pitch = sound.Pitch;
            sound.audioSource = source;

        }
    }

    public void SetSoundVolum(float _volume)
    {
        foreach( var sound in listSound)
        {
            if (sound.SoundName != SoundName.Background.ToString())
            {
                sound.audioSource.volume = sound.Volume * _volume;
            }
        }
    }

    public void SetMusicVolume(float _volume)
    {
        foreach (var sound in listSound)
        {
            if(sound.SoundName == SoundName.Background.ToString())
            {
                sound.audioSource.volume = sound.Volume *_volume;
            }
            
        }
    }

    public void Play(SoundName _name)
    {
        foreach (var sound in listSound)
        {
            if(sound.SoundName == _name.ToString())
            {
                if (sound.audioSource.isPlaying) return;
                sound.audioSource.Play();
            }
        }
    }

    public void Stop(SoundName _name)
    {
        foreach (var sound in listSound)
        {
            if (sound.SoundName == _name.ToString())
            {
                sound.audioSource.Stop();
            }
        }
    }



    
}
