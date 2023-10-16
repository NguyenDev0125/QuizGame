using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase.Storage;
using System.Linq;

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
    StorageReference stRef;

    FirebaseApp app;

    void Awake()
    {
        if (DatabaseManager.instance != null && DatabaseManager.instance != this) Destroy(this);
        DontDestroyOnLoad(this.gameObject);
        CreateReference();
    }

    private void CreateReference()
    {

        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        stRef = storage.GetReferenceFromUrl("gs://quizgame-3e7a1.appspot.com/");
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
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

    public  void GetAllUserAccount(string _path , Action<List<Dictionary<string,string>>> callBack)
    {
        
        if (dbRef == null) dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        DatabaseReference userRef = dbRef.Child(_path);
        userRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();
                DataSnapshot snapshot = task.Result;
                foreach (var snap in snapshot.Children)
                {
                    string key = snap.Key;
                    Dictionary<string, string> result = new Dictionary<string, string>();
                    result.Add("id", key);
                    result.Add("username", snap.Child("username").Value.ToString());
                    result.Add("password", snap.Child("password").Value.ToString());
                    result.Add("email", snap.Child("email").Value.ToString());
                    result.Add("score", snap.Child("score").Value.ToString());
                    result.Add("isadmin", snap.Child("isadmin").Value.ToString());
                    results.Add(result);
                }
                callBack(results);
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
            callback(result);
        });
    }
    public void GetJsonDatas(string _path, Action<string[]> callback)
    {

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
    public void GetJsonDatasSortedAccending(string _path, Action<string[]> callback, int count = 20)
    {

        dbRef.Child(_path).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Error");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.ChildrenCount > 0)
                {
                    var list = from child in snapshot.Children
                               orderby long.Parse(child.Key)
                               select child.Value.ToString();
                    list.Take(count);
                    callback(list.ToArray());
                }

            }
        });
    }

    public void GetJsonDatasSortedDeccending(string _path, Action<string[]> callback, int count = 20)
    {

        dbRef.Child(_path).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Error");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.ChildrenCount > 0)
                {
                    var list = from child in snapshot.Children
                               orderby long.Parse(child.Key)
                               select child.Value.ToString();
                    list.Take(count);
                    callback(list.ToArray());
                }

            }
        });
    }

    public async void SavePack(string _path, string _json)
    {
        DateTime date = DateTime.Now;
        string realTime = date.ToString("mm_hh_dd_yyyy");
        _path += "NguyenDev_QuizGame_Pack_" + realTime;
        await dbRef.Child(_path).SetValueAsync(_json);
    }
    public void SaveJsonDataCallBack(string _path, string _json, Action callBack)
    {
        if(dbRef == null) dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        dbRef.Child(_path).SetValueAsync(_json).ContinueWith(task =>
        {
            callBack.Invoke();
        });

    }


    public async void SaveDictionary(string _path, Dictionary<string , string> _data)
    {
        if (dbRef == null) Debug.Log("Firebase is nit Initialized");
        await dbRef.Child(_path).SetValueAsync(_data);
    }

    public async void SaveData(string _path, string _data)
    {
        if (dbRef == null) Debug.Log("Firebase is nit Initialized");
        await dbRef.Child(_path).SetValueAsync(_data);
    }

    public void SaveImage(string localPath, string remotePath , Action<string > callBack)
    {
        
        StorageReference imageRef = stRef.Child(remotePath);
        imageRef.PutFileAsync(localPath)
        .ContinueWithOnMainThread((Task<StorageMetadata> task) => {
        if (task.IsFaulted || task.IsCanceled)
        {
            callBack("t1"+ task.Exception);
        }
        else
        {
                imageRef.GetDownloadUrlAsync().ContinueWithOnMainThread(getUrlTask =>
                {
                    if (getUrlTask.IsFaulted || getUrlTask.IsCanceled)
                    {
                        callBack("t2"+getUrlTask.Exception.Message);
                    }
                    else
                    {
                        callBack(getUrlTask.Result.ToString());
                    }

                });
        }
    });
    }

    public void SaveImage(byte[] img, string remotePath, Action<string> callBack)
    {

        StorageReference imageRef = stRef.Child(remotePath);
        imageRef.PutBytesAsync(img)
        .ContinueWithOnMainThread((Task<StorageMetadata> task) => {
            if (task.IsFaulted || task.IsCanceled)
            {
                callBack("t1" + task.Exception);
            }
            else
            {
                imageRef.GetDownloadUrlAsync().ContinueWithOnMainThread(getUrlTask =>
                {
                    if (getUrlTask.IsFaulted || getUrlTask.IsCanceled)
                    {
                        callBack("t2" + getUrlTask.Exception.Message);
                    }
                    else
                    {
                        callBack(getUrlTask.Result.ToString());
                    }

                });
            }
        });
    }

    public async void GetTextureFormImageUrl(string remotePath ,int w , int h, Action<Texture2D> callBack)
    {
        StorageReference imageRef = stRef.Child(remotePath);
        Debug.Log("Dowload " + imageRef);
        int maxSize = 1 * 1024 * 1024 * 10;
        Task<byte[]> dowloadTask = imageRef.GetBytesAsync(maxSize);
        await dowloadTask;

        if(!dowloadTask.IsFaulted && !dowloadTask.IsCanceled)
        {
            Texture2D newText = new Texture2D(w,h);
            newText.LoadImage(dowloadTask.Result);
            callBack(newText);

        }
    }

    public void RegistListenerOnChillAdded(string path , Action<object> callBack)
    {
        DatabaseReference subject = dbRef.Child(path);
        subject.ChildAdded += (sender, args) =>
        {
            callBack.Invoke(args.Snapshot.Value);
        };
    }
}
