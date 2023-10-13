using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackCreater 
{
    private Pack pack;
    private string path = "QuizGame/Pack/";
    public List<Question> ListQuestion
    {
        get
        {
            return pack.listQuestion;
        }
    }
    public Pack CreateNewPack()
    {
        pack = new Pack();
        pack.packId = RandomId();
        return pack;
    }

    private string RandomId()
    {
        char[] result = new char[8];
        for(int i = 0; i < result.Length; i++)
        {
            result[i] = (char)UnityEngine.Random.Range(48, 58);
        }
        return new string(result);
    }
    public void AddQuestion(Question question)
    {
        if (pack == null) pack = new Pack();
        if (pack.listQuestion == null) pack.listQuestion = new List<Question>();
        pack.listQuestion.Add(question);
        
    }

    public void UploadPack(string _packName , string _packDes , string imagePath)
    {
        if(pack != null && pack.listQuestion != null)
        {
            pack.packName = _packName;
            pack.packDes = _packDes;
            string json = JsonConvert.SerializeObject(pack);
            DatabaseManager.Instance.SaveJsonData(path, json);

        }
    }
}
