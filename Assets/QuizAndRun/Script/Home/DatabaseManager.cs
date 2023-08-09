using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
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
            if(instance == null) instance = FindObjectOfType<DatabaseManager>();
            return instance;

        }
    }
    private DatabaseManager() { }
    DatabaseReference reference;
    

    void Awake()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;

    }


    public async Task<List<QuestionPack>> GetAllQuestionPacksFromFirebase(string path)
    {
        List<QuestionPack> allQuestions = new List<QuestionPack>();

        if (reference == null)
        {
            Debug.LogError("Firebase is not initialized.");
            return allQuestions;
        }

        DataSnapshot snapshot = await reference.Child(path).GetValueAsync();

        if (snapshot != null && snapshot.Exists)
        {
            foreach (var childSnapshot in snapshot.Children)
            {

                    string jsonData = childSnapshot.GetValue(false).ToString();
                    Debug.Log(jsonData);
                    QuestionPack question = JsonConvert.DeserializeObject<QuestionPack>(jsonData);
                    Debug.Log(question.name + question.packDes);
                    Debug.Log(JsonConvert.SerializeObject(question));
                    allQuestions.Add(question);

            }
        }
        else
        {
            Debug.LogWarning("No data found at path: " + path);
        }

        return allQuestions;
    }

    public void SaveQuestionPackToFirebase(string _path,QuestionPack _pack)
    {
        string json = JsonConvert.SerializeObject(_pack);
        if (reference == null) Debug.Log("Firebase is nit Initialized");
        _path += UnityEngine.Random.Range(1111, 9999).ToString() + "n3devpack";
        Debug.Log(_path);
        reference.Child(_path).SetValueAsync(json);

    }
}
