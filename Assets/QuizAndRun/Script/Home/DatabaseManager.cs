using Firebase.Database;
using System;
using System.Collections;
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
    private DatabaseReference db;
    void Awake()
    {
        db = FirebaseDatabase.DefaultInstance.RootReference;
    }


    public IEnumerator GetJsonData( string _path, Action<string> _callback)
    {
        Debug.Log(db);
        var data = db.Child(_path).GetValueAsync();
        yield return new WaitUntil(predicate: () => data.IsCompleted);
        if(data != null)
        {
            DataSnapshot snapshot = data.Result;
            _callback?.Invoke(snapshot.Value.ToString());
        }
    }

    public void SetData(string _path , string _data)
    {
        db.Child(_path).SetValueAsync(_data);
    }
}
