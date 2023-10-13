using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject creditPanel;
    [SerializeField] Slider soundSlide;
    [SerializeField] Slider musicSlide;
    [SerializeField] Button toggleSettingBtn;
    [SerializeField] Button openCreditBtn;
    [SerializeField] Button closeSettingBtn;
    [SerializeField] Button closeCreditPanelBtn;
    [SerializeField] Button GoGomeBtn;
    [SerializeField] Text ScoreTxt;
    [SerializeField] Text TotalAnswerTxt;
    [SerializeField] Button backHomeBtn;
    

    [Header("Menu")]
    [SerializeField] GameObject GameOverMenu;
    [SerializeField] GameObject GameVictoryMenu;
    [SerializeField] GameObject questionPanel;
    [SerializeField] Button OpenSettingBtn;


    private void Start()
    {
        if (openCreditBtn) toggleSettingBtn.onClick.AddListener(ToggleSettingPanel);
        if (openCreditBtn) openCreditBtn.onClick.AddListener(ToggleCredit);
        if (closeSettingBtn) closeSettingBtn.onClick.AddListener(ToggleSettingPanel);
        if (closeCreditPanelBtn) closeCreditPanelBtn.onClick.AddListener(ToggleCredit);
        if (GoGomeBtn) GoGomeBtn.onClick.AddListener(GoHome);
        if (backHomeBtn) backHomeBtn.onClick.AddListener(GoHome);

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
        questionPanel.transform.DOScale(0f, 0.5f);
    }

    public void DisplayResult(int totalTrue , int totalAnswer ,int score, float time)
    {
        GameVictoryMenu.SetActive(true);
        toggleSettingBtn.gameObject.SetActive(false);
        questionPanel.SetActive(false);
        toggleSettingBtn.gameObject.SetActive(false);
        settingPanel.gameObject.SetActive(false);
        StartCoroutine(IE_ShowScore(totalTrue, totalAnswer));
        ScoreTxt.text = "Your score : " + score;
    }

    private IEnumerator IE_ShowScore(int totcalTrueAnswer , int totalAnswer)
    {
        yield return new WaitForSeconds(1f);
        
        for(int i = 1; i <= totcalTrueAnswer; i++)
        {
            TotalAnswerTxt.text = $"{i} / {totalAnswer}";
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void GoHome()
    {
        SceneManager.LoadScene("HomeMenu");
    }

    private void ToggleSettingPanel()
    {
        settingPanel.SetActive(!settingPanel.activeInHierarchy);
    }

    private void ToggleCredit()
    {
        creditPanel.SetActive(!creditPanel.activeInHierarchy);
        settingPanel.SetActive(!creditPanel.activeInHierarchy);
    }




}
