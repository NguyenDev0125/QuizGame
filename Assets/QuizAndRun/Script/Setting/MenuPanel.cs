using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject creditPanel;
    [SerializeField] Slider soundSlide;
    [SerializeField] Slider musicSlide;
    [SerializeField] Button openSettingPanelBtn;
    [SerializeField] Button openCreditPanelBtn;
    [SerializeField] Button closeSettingPanelBtn;
    [SerializeField] Button closeCreditPanelBtn;

    [Header("Event raise")]
    [SerializeField] VoidEventChanel OnGameOver;
    [SerializeField] VoidEventChanel OnTimeOut;
    [SerializeField] VoidEventChanel OnVictory;

    [Header("Event listener")]
    [SerializeField] FloatEventChanel OnSoundSlideValueChanged;
    [SerializeField] FloatEventChanel OnMusicSlideValueChanged;


    

    private void Start()
    {
        if (OnSoundSlideValueChanged && soundSlide != null) soundSlide.onValueChanged.AddListener(OnSoundSlideValueChanged.Raised);
        if (OnMusicSlideValueChanged && musicSlide != null) musicSlide.onValueChanged.AddListener(OnMusicSlideValueChanged.Raised);

        if (OnGameOver) OnGameOver.OnEventRaised += OpenGameOverPanel;
        if (OnGameOver) OnVictory.OnEventRaised += OpenGameVictoryPanel;
        if (OnGameOver) OnTimeOut.OnEventRaised += OpenTimeOutPanel;

        if (openCreditPanelBtn) openSettingPanelBtn.onClick.AddListener(OpenSettingPanel);
        if (openCreditPanelBtn) openCreditPanelBtn.onClick.AddListener(OpenCreditPanel);
        if (closeSettingPanelBtn) closeSettingPanelBtn.onClick.AddListener(OpenSettingPanel);
        if (closeCreditPanelBtn) closeCreditPanelBtn.onClick.AddListener(OpenCreditPanel);

        soundSlide.value = GameManager.Instance.SettingManager.SoundVolume;
        musicSlide.value = GameManager.Instance.SettingManager.MusicVolume;
    }

    public void OnButtonHover()
    {
        SoundManager.Instance.Play("MouseHover");
    }

    public void OnButtonClick()
    {
        SoundManager.Instance.Play("MouseClick");
    }

    private void OpenGameOverPanel()
    {

    }

    private void OpenGameVictoryPanel()
    {

    }

    private void OpenTimeOutPanel()
    {

    }

    private void OpenSettingPanel()
    {
        settingPanel?.SetActive(!settingPanel.activeInHierarchy);
        
    }

    private void OpenCreditPanel()
    {
        creditPanel?.SetActive(!creditPanel.activeInHierarchy);
        
    }



}
