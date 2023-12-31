using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.Table;


public class ChatPanel : MonoBehaviour
{
    [SerializeField] Button sendBtn;
    [SerializeField] TMP_InputField inputTxt;
    [SerializeField] MessengeUI messPrb;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] AccountManager accountManager;
    private string chatPath = "Chats/ChatChanel";
    private string myMessenge = "";
    Queue<GameObject> queueObj;
    private void Start()
    {
        sendBtn.onClick.AddListener(SendText);
        DatabaseManager.Instance.RegistListenerOnChillAdded(chatPath, GetNewMessenge);
        queueObj = new Queue<GameObject>();
        //LoadAllChat();
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendText();
        }
    }
#endif
    private void LoadAllChat()
    {
        DatabaseManager.Instance.GetJsonDatasSortedAccending(chatPath, (listJson) =>
        {
            Debug.Log(listJson.Length);
            foreach (string s in listJson)
            {
                MessengeData data = JsonConvert.DeserializeObject<MessengeData>(s);
                AddNewMessUI(data.username, data.messenge);
            }
        });
    }
    private void AddNewMessUI(string username, string mess)
    {
        MessengeUI m = Instantiate(messPrb, scrollRect.content);
        m.SetText(username, mess);
        if (scrollRect.verticalNormalizedPosition == 0)
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
        if(data.username != accountManager.GetCurrentUser().username)
        {
            SoundManager.Instance.Play("NewMessenge");
        }

    }

    private void SendText()
    {
        if (inputTxt.text.Length <= 0) return;
        string messenge = inputTxt.text;
        myMessenge = messenge;
        inputTxt.text = "";
        inputTxt.Select();
        MessengeData mess = new MessengeData(accountManager.GetCurrentUser().username, messenge);
        string savePath = chatPath + "/" + (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        string json = JsonConvert.SerializeObject(mess);
        DatabaseManager.Instance.SaveJsonDataCallBack(savePath, json, () =>
        {
        });
    }


}
