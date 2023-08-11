
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    [SerializeField] ChooseLevelPanel chooseLvPanel;
    [SerializeField] GameObject lvCreaterPanel;
    [SerializeField] GameObject storyPanel;
    [SerializeField] Button openChooseLvPanelBtn;
    [SerializeField] Button openLvCreaterPanelBtn;
    [SerializeField] Button openStoryPanelBtn;

    [SerializeField] Button closeChooseLvPanelBtn;
    [SerializeField] Button closeLvCreaterPanelBtn;
    [SerializeField] Button closeStoryPanelBtn;
    [SerializeField] Text totalPlayTxt;
    [SerializeField] VoidEventChanel OnListPackLoaded;

    private void Awake()
    {
        if (OnListPackLoaded) OnListPackLoaded.OnEventRaised += DisplayLevel;

        openChooseLvPanelBtn.onClick.AddListener(OpenOptionPanel);
        openLvCreaterPanelBtn.onClick.AddListener(OpenUploadLvPanel);
        openStoryPanelBtn.onClick.AddListener(OpenStoryPanel);        

        closeChooseLvPanelBtn.onClick.AddListener(OpenOptionPanel);
        closeLvCreaterPanelBtn.onClick.AddListener(OpenUploadLvPanel);
        closeStoryPanelBtn.onClick.AddListener(OpenStoryPanel);
    }

    private void OpenOptionPanel()
    {
        chooseLvPanel.gameObject.SetActive(!chooseLvPanel.isActiveAndEnabled);
    }

    private void OpenUploadLvPanel()
    {
        lvCreaterPanel.gameObject.SetActive(!lvCreaterPanel.activeInHierarchy);
    }

    private void OpenStoryPanel()
    {
        storyPanel.SetActive(!storyPanel.activeInHierarchy);
    }
    private void DisplayLevel()
    {
        
        chooseLvPanel.DisplayLevels(QuestionPackManager.Instance.ListPack.ToArray());
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void SetTotalPlayText(string _text)
    {
        totalPlayTxt.text ="Total play : "+ _text;
    }

    

}
