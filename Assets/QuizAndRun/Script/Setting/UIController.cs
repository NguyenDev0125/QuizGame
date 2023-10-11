using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
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


    private void Start()
    {
        if (openCreditPanelBtn) openSettingPanelBtn.onClick.AddListener(OpenSettingPanel);
        if (openCreditPanelBtn) openCreditPanelBtn.onClick.AddListener(OpenCreditPanel);
        if (closeSettingPanelBtn) closeSettingPanelBtn.onClick.AddListener(OpenSettingPanel);
        if (closeCreditPanelBtn) closeCreditPanelBtn.onClick.AddListener(OpenCreditPanel);

    }

    public void OnButtonHover()
    {
        SoundManager.Instance.Play("MouseHover");
    }

    public void OnButtonClick()
    {
        SoundManager.Instance.Play("MouseClick");
    }

    public void OpenGameOverPanel()
    {
        GameOverMenu.SetActive(true);
        GameOverMenu.transform.localScale = Vector3.zero;
        GameOverMenu.transform.DOScale(1f, 1f);
        QuestionPanel.transform.DOScale(0f, 0.5f);
    }

    public void DisplayResult()
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




}
