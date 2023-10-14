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
    public int GetScore()
    {
        return PlayerPrefs.GetInt(SCORE_KEY, 0);
    }

    public void AddScore(int score)
    {
        int sc = GetScore() + score;
        string remoteSavePath = "Accounts/" + PlayerPrefs.GetString("id") + "/score";
        DatabaseManager.Instance.SaveData(remoteSavePath, sc.ToString());
        PlayerPrefs.SetInt(SCORE_KEY, sc);
    }

}
