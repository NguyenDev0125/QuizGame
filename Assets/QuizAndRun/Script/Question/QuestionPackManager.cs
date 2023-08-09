using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

public class QuestionPackManager : MonoBehaviour
{
    private static QuestionPackManager instance;
    [SerializeField] List<QuestionPack> listPack;
    [SerializeField] VoidEventChanel OnListPackLoaded;
    [SerializeField] QuestionPack packToSave;

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
        LoadPack();
        //SavePack(packToSave);
    }
    private async void LoadPack()
    {
        Task<List<QuestionPack>> getDataTask = DatabaseManager.Instance.GetAllQuestionPacksFromFirebase(path);
        listPack = await getDataTask;
        OnListPackLoaded.Raise();
        
    }

    private void SavePack(QuestionPack _pack)
    {
        DatabaseManager.Instance.SaveQuestionPackToFirebase(path, _pack);
    }

    public void SelectPack(int _packIndex)
    {
        if (_packIndex < 0 || _packIndex >= listPack.Count) return;
        selectedPackIndex = _packIndex;
        Debug.Log("Selected pack : " + selectedPackIndex);
        Debug.Log(listPack[selectedPackIndex]);
    }



}
