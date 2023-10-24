using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    public static ScoreManager Instance
    {
        get 
        { 
            if(ScoreManager.instance == null) instance = FindObjectOfType(typeof(ScoreManager)) as ScoreManager;
            return instance;
        }
    }

    private void Awake()
    {
        if (ScoreManager.instance != null || ScoreManager.Instance != this) Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
    private string SCORE_KEY = "score";


    public void AddScore(int score)
    {
        string remoteSavePath = "Accounts/" + PlayerPrefs.GetString("id") + "/score";
        DatabaseManager.Instance.GetData(remoteSavePath, (s) =>
        {
            int sc = int.Parse(s) + score;
            DatabaseManager.Instance.SaveData(remoteSavePath, sc.ToString());
            PlayerPrefs.SetInt(SCORE_KEY, sc);
        });


    }

}
