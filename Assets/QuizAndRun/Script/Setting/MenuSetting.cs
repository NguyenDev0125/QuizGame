
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MenuSetting : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject Credit;
    [SerializeField] Slider soundSlide;
    [SerializeField] Slider musicSlide;


    private bool isOpen = false;

    private void Start()
    {
        soundSlide.value = PlayerPrefs.GetFloat("sound", 0.8f);
        SoundManager.Instance.SetSoundVolum(soundSlide.value);
        musicSlide.value = PlayerPrefs.GetFloat("music", 0.8f);
        SoundManager.Instance.SetMusicVolume(musicSlide.value);
        soundSlide.onValueChanged.AddListener(OnSoundSlideChange);
        musicSlide.onValueChanged.AddListener(OnMusicSlideChange);
    }

    private void OnSoundSlideChange(float _value)
    {
        SoundManager.Instance.SetSoundVolum(_value);
        PlayerPrefs.SetFloat("sound", _value);
    }

    private void OnMusicSlideChange(float _value)
    {
        SoundManager.Instance.SetMusicVolume(_value);
        PlayerPrefs.SetFloat("music", _value);
    }
    public void OpenMenu()
    {
        Menu.SetActive(!isOpen);
        isOpen = !isOpen;
    }

    public void CloseMenu()
    {
        Menu.SetActive(!isOpen);
        isOpen = !isOpen;
    }

    public void OpenCredit()
    {
        Credit.SetActive(!Credit.activeInHierarchy);
    }
}
