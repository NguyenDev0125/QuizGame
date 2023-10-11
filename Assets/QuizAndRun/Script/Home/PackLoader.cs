using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using System;

public class PackLoader : MonoBehaviour
{
    [SerializeField] Pack selectedPack;
    [SerializeField] SelectPackPanel selectPackPanel;
    private List<Pack> listPack;
    private int selectedPackIndex = 0;
    private string path = "QuizGame/Pack/";

    public Pack GetSelectedPack()
    {
        return listPack[selectedPackIndex];
    }

    public void LoadPacks()
    {
        listPack = new List<Pack>();
        DatabaseManager.Instance.GetJsonDatas(path, GetData);
    }

    private void GetData(string[] _result)
    {
        listPack.Clear();
        foreach (string json in _result)
        {
            Pack question = JsonConvert.DeserializeObject<Pack>(json);
            Debug.Log("Question pack name : " + question.packName);
            listPack.Add(question);
        }
        selectPackPanel.DisplayPacks(listPack);
    }

    public void SelectPack(int _packIndex)
    {
        if (_packIndex < 0 || _packIndex >= listPack.Count) return;
        selectedPackIndex = _packIndex;
        selectedPack.packName = listPack[selectedPackIndex].packName;
        selectedPack.packDes = listPack[selectedPackIndex].packDes;
        selectedPack.listQuestion = listPack[selectedPackIndex].listQuestion;
        Debug.Log("Selected pack : " + selectedPackIndex);
    }



}
