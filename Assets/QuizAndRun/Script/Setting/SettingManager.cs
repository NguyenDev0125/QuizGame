using UnityEngine;

public class SettingManager : MonoBehaviour
{
    [SerializeField] float defaultSoundVolume;
    [SerializeField] float defaultMusicVolume;

    private const string SOUND_VOLUME = "soundVolume";
    private const string MUSIC_VOLUME = "musicVolume";
    
    public float SoundVolume { get => PlayerPrefs.GetFloat(SOUND_VOLUME, defaultSoundVolume); }
    public float MusicVolume { get => PlayerPrefs.GetFloat(MUSIC_VOLUME, defaultMusicVolume); }

    private void OnEnable()
    {

    }
    private void SoundSlideChanged(float _volume)
    {
        PlayerPrefs.SetFloat(SOUND_VOLUME, _volume);
        SoundManager.Instance.SetSoundVolum(_volume);
    }

    private void MusicSlideChanged(float _volume)
    {
        PlayerPrefs.SetFloat(MUSIC_VOLUME, _volume);
        SoundManager.Instance.SetMusicVolume(_volume);
    }
}
