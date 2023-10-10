using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Google.MiniJSON;
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
    FirebaseApp app;

    void Start()
    {
        if (DatabaseManager.instance != this) Destroy(this);
        DontDestroyOnLoad(this.gameObject);
        CreateReference();
    }

    private void CreateReference()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                app = Firebase.FirebaseApp.DefaultInstance;
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));

            }
        });
        
    }
    public void GetData(string _path, Action<string> callback)
    {
        if (reference == null) reference = FirebaseDatabase.DefaultInstance.RootReference;

        reference.Child(_path).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error getting data from Firebase: " + task.Exception);
                return;
            }

            DataSnapshot snapshot = task.Result;
            string result = snapshot.Value.ToString();
            Debug.Log("Get data = " + result);

            callback(result);
        });
    }

    public void GetJsonDatas(string _path, Action<string[]> callback)
    {
        if (reference == null) CreateReference();
        reference.Child(_path).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                List<string> valuesList = new List<string>();
                if (snapshot.ChildrenCount > 0)
                {
                    foreach (var childSnapshot in snapshot.Children)
                    {
                        string value = childSnapshot.Value.ToString();
                        valuesList.Add(value);
                    }
                }
                else
                {
                    valuesList.Add(snapshot.Value.ToString());
                }


                string[] result = valuesList.ToArray();

                foreach (string value in result)
                {
                    Debug.Log(value);
                }
                callback(result);
            }
        });


    }

    public async void SaveJsonData(string _path, string _json)
    {
        if (reference == null) Debug.Log("Firebase is nit Initialized");
        DateTime date = DateTime.Now;
        string realTime = date.ToString("mm_hh_dd_yyyy");
        _path += "NguyenDev_QuizGame_Pack" + realTime;
        Debug.Log(_path);
        await reference.Child(_path).SetValueAsync(_json);

    }

    public async void SaveData(string _path, string _data)
    {
        if (reference == null) Debug.Log("Firebase is nit Initialized");
        await reference.Child(_path).SetValueAsync(_data);
    }
}
