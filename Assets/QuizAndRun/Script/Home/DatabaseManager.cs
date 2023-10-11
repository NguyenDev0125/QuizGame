using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Storage;
using System;
using System.Collections.Generic;
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
    DatabaseReference dbRef;
    StorageReference storageReference;
    FirebaseApp app;

    void Start()
    {
        if (DatabaseManager.instance != null && DatabaseManager.instance != this) Destroy(this);
        DontDestroyOnLoad(this.gameObject);
        CreateReference();
    }

    private void CreateReference()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReferenceFromUrl("gs://quizgame-3e7a1.appspot.com");
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
        if (dbRef == null) dbRef = FirebaseDatabase.DefaultInstance.RootReference;

        dbRef.Child(_path).GetValueAsync().ContinueWithOnMainThread(task =>
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
        if (dbRef == null) CreateReference();
        dbRef.Child(_path).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Error");
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
        if (dbRef == null) Debug.Log("Firebase is not Initialized");
        DateTime date = DateTime.Now;
        string realTime = date.ToString("mm_hh_dd_yyyy");
        _path += "NguyenDev_QuizGame_Pack" + realTime;
        Debug.Log(_path);
        await dbRef.Child(_path).SetValueAsync(_json);

    }

    public async void SaveData(string _path, string _data)
    {
        if (dbRef == null) Debug.Log("Firebase is nit Initialized");
        await dbRef.Child(_path).SetValueAsync(_data);
    }

}
