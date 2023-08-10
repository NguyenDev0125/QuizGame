using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private static DatabaseManager instance;
    public static DatabaseManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<DatabaseManager>();
            return instance;

        }
    }
    private DatabaseManager() { }
    DatabaseReference reference;


    void Awake()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;

    }


    //public async Task<List<QuestionPack>> GetJsonData(string path)
    //{
    //    List<QuestionPack> allQuestions = new List<QuestionPack>();

    //    if (reference == null)
    //    {
    //        Debug.LogError("Firebase is not initialized.");
    //        return allQuestions;
    //    }

    //    DataSnapshot snapshot = await reference.Child(path).GetValueAsync();

    //    if (snapshot != null && snapshot.Exists)
    //    {
    //        foreach (var childSnapshot in snapshot.Children)
    //        {

    //                string jsonData = childSnapshot.GetValue(false).ToString();
    //                while(jsonData.Length < 10)
    //                {
    //                jsonData = childSnapshot.GetValue(false).ToString();
    //                }
    //                QuestionPack question = JsonConvert.DeserializeObject<QuestionPack>(jsonData);
    //                Debug.Log(question.name + question.packDes);
    //                Debug.Log(JsonConvert.SerializeObject(question));
    //                allQuestions.Add(question);

    //        }
    //    }
    //    else
    //    {
    //        Debug.LogWarning("No data found at path: " + path);
    //    }

    //    return allQuestions;
    //}
    public async Task<string[]> GetJsonDatas(string path)
    {
        if (reference == null) return null;
        string[] result = null;
        DataSnapshot snapshot = await reference.Child(path).GetValueAsync();
        if (snapshot != null && snapshot.Exists)
        {
            result = new string[snapshot.ChildrenCount];
            int count = 0;
            foreach (DataSnapshot child in snapshot.Children)
            {
                string jsonData = null;
                jsonData = child.GetValue(false).ToString();
                Debug.Log(jsonData);

                result[count] = jsonData;
                count++;
            }

        }
        else
        {
            Debug.LogWarning("No data found at path: " + path);
        }

        return result;
    }


    public async void SaveJsonData(string _path, QuestionPack _pack)
    {
        string json = JsonConvert.SerializeObject(_pack);
        if (reference == null) Debug.Log("Firebase is nit Initialized");
        DateTime date = DateTime.Now;
        string realTime = date.ToString("mm_hh_dd_yyyy");
        _path += "NguyenDev_QuizGame_Pack" + realTime ;
        Debug.Log(_path);
        await reference.Child(_path).SetValueAsync(json);

    }
}
