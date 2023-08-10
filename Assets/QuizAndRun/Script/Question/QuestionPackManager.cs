using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;

public class QuestionPackManager : MonoBehaviour
{
    private static QuestionPackManager instance;
    
    [SerializeField] VoidEventChanel OnListPackLoaded;

    private List<QuestionPack> listPack;
    private int selectedPackIndex = 0;
    private string path = "QuizGame/Pack/";

    public static QuestionPackManager Instance 
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null )
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }
    public List<QuestionPack> ListPack
    {
        get
        {
            if (listPack == null) Debug.Log("List pack is null");
            return listPack;
        }
    }

    public QuestionPack SelectedPack
    {
        get
        {
            if (listPack.Count == 0) Debug.Log("List pack is null");
            return listPack[selectedPackIndex];
        }
    }
      
      

    private void Start()
    {
        
        DontDestroyOnLoad(gameObject);
        LoadPacks();
        
    }
    private async void LoadPacks()
    {
        listPack = new List<QuestionPack>();
        string[] jsonDatas = await DatabaseManager.Instance.GetJsonDatas(path);
        foreach (string json in jsonDatas)
        {
            QuestionPack question = JsonConvert.DeserializeObject<QuestionPack>(json);
            Debug.Log("Question pack name : " +question.packName);
            listPack.Add(question);
        }
        OnListPackLoaded.Raise();
        
    }

    public  void SavePack(QuestionPack _pack)
    {
        DatabaseManager.Instance.SaveJsonData(path, _pack);
    }

    public void SelectPack(int _packIndex)
    {
        if (_packIndex < 0 || _packIndex >= listPack.Count) return;
        selectedPackIndex = _packIndex;
        Debug.Log("Selected pack : " + selectedPackIndex);
        Debug.Log(listPack[selectedPackIndex]);
    }



}
