using DG.Tweening;
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
    [SerializeField] Button restartGameBtn;
    [SerializeField] Button restartGameBtn1;

    [Header("Menu")]
    [SerializeField] GameObject GameOverMenu;
    [SerializeField] GameObject GameVictoryMenu;
    [SerializeField] GameObject QuestionPanel;

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

        restartGameBtn?.onClick.AddListener(RestartGame);
        restartGameBtn1?.onClick.AddListener(RestartGame);

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
        GameOverMenu.SetActive(true);
        GameOverMenu.transform.localScale = Vector3.zero;
        GameOverMenu.transform.DOScale(1f, 1f);
        QuestionPanel.transform.DOScale(0f, 0.5f);
    }

    private void OpenGameVictoryPanel()
    {
        GameVictoryMenu.transform.localScale = Vector3.zero;
        GameVictoryMenu.transform.DOScale(1f, 1f);
        GameVictoryMenu.SetActive(true);

        QuestionPanel.transform.DOScale(0f, 0.5f);
    }

    private void OpenTimeOutPanel()
    {
        Debug.Log("Time out");
    }

    private void OpenSettingPanel()
    {
        settingPanel?.SetActive(!settingPanel.activeInHierarchy);
        
    }

    private void OpenCreditPanel()
    {
        creditPanel?.SetActive(!creditPanel.activeInHierarchy);
        
    }

    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }



}
