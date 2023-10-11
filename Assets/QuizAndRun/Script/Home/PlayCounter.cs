using UnityEngine;
using UnityEngine.UI;

public class PlayCounter : MonoBehaviour
{
    [SerializeField] Text countTxt;
    string path = "QuizGame/PlayCount";
    private void Start()
    {
        DatabaseManager.Instance.GetJsonDatas(path, GetData);
    }
    private void GetData(string[] _data)
    {
        
        countTxt.text = "Total play : "+ _data[0];
        int count = int.Parse(_data[0]);
        count++;
        DatabaseManager.Instance.SaveData(path,count.ToString());
        
    }
}
