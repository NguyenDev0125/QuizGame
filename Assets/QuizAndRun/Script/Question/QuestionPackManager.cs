using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

public class QuestionPackManager : MonoBehaviour
{
    
    [SerializeField] QuestionPack[] pack;

    private QuestionPack selectedPack;

    private string path = "QuizGame/Pack/1";
    private string json;
    
    public QuestionPack[] Packs
    {
        get
        {
            if (pack == null)
            {
                pack = GetQuestionPacksOnDatabase();
            }
            return pack;
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        pack = GetQuestionPacksOnDatabase();
    }

    private void CreateJsonPack(QuestionPack _pack)
    {
        
        
    }

    private QuestionPack[] GetQuestionPacksOnDatabase()
    {
        List<QuestionPack> result = new List<QuestionPack>();
       // StartCoroutine(DatabaseManager.Instance.GetJsonData(path,GetJson));
        Debug.Log(json);

        return result.ToArray();
    }

    private void GetJson(string _json)
    {
        json = _json;
    }
}
