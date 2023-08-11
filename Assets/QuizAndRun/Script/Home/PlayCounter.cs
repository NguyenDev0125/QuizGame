using UnityEngine;

public class PlayCounter : MonoBehaviour
{
    [SerializeField] HomeManager homeManager;
    string path = "QuizGame/PlayCount";
    private void Start()
    {
        DatabaseManager.Instance.GetJsonDatas(path, GetData);
    }

    private void GetData(string[] _data)
    {
        Debug.Log("Total play game : "+ _data[0]);
        homeManager.SetTotalPlayText(_data[0]);
        int count = int.Parse(_data[0]);
        count++;
        DatabaseManager.Instance.SaveData(path,count.ToString());
    }
}
