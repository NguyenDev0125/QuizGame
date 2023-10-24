
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeUIController : MonoBehaviour
{
    
    [SerializeField] SelectPackPanel chooseLvPanel;
    [SerializeField] GameObject lvCreaterPanel;
    [SerializeField] GameObject storyPanel;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject chatPanel;
    [SerializeField] GameObject leaderBoard;

    [SerializeField] Button openChooseLvPanelBtn;
    [SerializeField] Button openLvCreaterPanelBtn;
    [SerializeField] Button openStoryPanelBtn;
    [SerializeField] Button openChatPanelBtn;
    [SerializeField] Button openLeaderBoardBtn;

    [SerializeField] Button closeChooseLvPanelBtn;
    [SerializeField] Button closeLvCreaterPanelBtn;
    [SerializeField] Button closeStoryPanelBtn;
    [SerializeField] Button colseChatPanelBtn;
    [SerializeField] Button colseLeaderBoardBtn;

    private void Awake()
    {

        openChooseLvPanelBtn.onClick.AddListener(OpenOptionPanel);
        openLvCreaterPanelBtn.onClick.AddListener(OpenUploadLvPanel);
        openStoryPanelBtn.onClick.AddListener(OpenStoryPanel);
        openChatPanelBtn.onClick.AddListener(OpenChatPanel);
        openLeaderBoardBtn.onClick.AddListener(OpenLeaderBoard);

        closeChooseLvPanelBtn.onClick.AddListener(OpenOptionPanel);
        closeLvCreaterPanelBtn.onClick.AddListener(OpenUploadLvPanel);
        closeStoryPanelBtn.onClick.AddListener(OpenStoryPanel);
        colseChatPanelBtn.onClick.AddListener(OpenChatPanel);
        colseLeaderBoardBtn.onClick.AddListener(OpenLeaderBoard);
    }

    private void OpenLeaderBoard()
    {
        menuPanel.SetActive(!menuPanel.activeInHierarchy);
        leaderBoard.SetActive(!menuPanel.activeInHierarchy);
    }

    public void OpenMenuPanel(bool fullOption)
    {
        if(!fullOption)
        {
            openLvCreaterPanelBtn.gameObject.SetActive(false);
        }
        menuPanel.gameObject.SetActive(true);
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

    private void OpenChatPanel()
    {
        chatPanel.gameObject.SetActive(!chatPanel.activeInHierarchy);
        menuPanel.gameObject.SetActive(!chatPanel.activeInHierarchy);
    }
}
