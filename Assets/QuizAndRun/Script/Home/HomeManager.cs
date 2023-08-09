
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    [SerializeField] PlayOptionsPanel panel;
    [SerializeField] VoidEventChanel OnListPackLoaded;

    private void Awake()
    {
        if (OnListPackLoaded) OnListPackLoaded.OnEventRaised += DisplayLevel;
    }

    private void DisplayLevel()
    {
        Debug.Log("Display");
        panel.DisplayLevels(QuestionPackManager.Instance.ListPack.ToArray());
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

}
