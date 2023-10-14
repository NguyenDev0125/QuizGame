using Newtonsoft.Json;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ChatPanel : MonoBehaviour
{
    [SerializeField] Button sendBtn;
    [SerializeField] TMP_InputField inputTxt;
    [SerializeField] MessengeUI messPrb;
    [SerializeField] ScrollRect scrollRect;
    private string chatPath = "Chats/ChatChanel";
    private string chatCounterPath = "Chats/Counter";

    Queue<GameObject> queueObj;
    private void Start()
    {
        sendBtn.onClick.AddListener(SendText);
        DatabaseManager.Instance.RegistListenerOnChillAdded(chatPath, GetNewMessenge);
        queueObj = new Queue<GameObject>();
        LoadAllChat();
    }
#if UNITY_EDITOR
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SendText();
        }
    }
#endif
    private void LoadAllChat()
    {
        DatabaseManager.Instance.GetJsonDatasSorted(chatPath, (listJson) =>
        {
            foreach (string s in listJson)
            {
                MessengeData data = JsonConvert.DeserializeObject<MessengeData>(s);
                AddNewMessUI(data.username, data.messenge);
            }
        });
    }
    private void AddNewMessUI(string username , string mess)
    {
        MessengeUI m = Instantiate(messPrb , scrollRect.content);
        m.SetText(username, mess);
        if(scrollRect.verticalNormalizedPosition == 0)
        {
            scrollRect.verticalNormalizedPosition = 0f;
        }
        queueObj.Enqueue(m.gameObject);
        if (queueObj.Count > 20)
        {
            Destroy(queueObj.Dequeue());
        }
    }
    private void GetNewMessenge(object obj)
    {
        Debug.Log(obj.ToString());
        MessengeData data = JsonConvert.DeserializeObject<MessengeData>(obj.ToString());
        AddNewMessUI(data.username, data.messenge);
    }

    private void SendText()
    {
        if (inputTxt.text.Length <= 0) return;
        Debug.Log(inputTxt.text);
        DatabaseManager.Instance.GetData(chatCounterPath, (s) =>
        {

            int count = int.Parse(s);
            count++;
            MessengeData mess = new MessengeData(PlayerPrefs.GetString("username"), inputTxt.text);
            string savePath = chatPath + "/" + count;
            string json = JsonConvert.SerializeObject(mess);
            DatabaseManager.Instance.SaveJsonData(savePath, json , () =>
            {
                inputTxt.text = "";
                DatabaseManager.Instance.SaveData(chatCounterPath, count.ToString());
            });
        });
    }


}
