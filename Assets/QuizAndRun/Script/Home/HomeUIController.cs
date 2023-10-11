
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeUIController : MonoBehaviour
{
    
    [SerializeField] SelectPackPanel chooseLvPanel;
    [SerializeField] GameObject lvCreaterPanel;
    [SerializeField] GameObject storyPanel;

    [SerializeField] Button openChooseLvPanelBtn;
    [SerializeField] Button openLvCreaterPanelBtn;
    [SerializeField] Button openStoryPanelBtn;

    [SerializeField] Button closeChooseLvPanelBtn;
    [SerializeField] Button closeLvCreaterPanelBtn;
    [SerializeField] Button closeStoryPanelBtn;

    private void Awake()
    {

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

    public void StartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }


    

}
