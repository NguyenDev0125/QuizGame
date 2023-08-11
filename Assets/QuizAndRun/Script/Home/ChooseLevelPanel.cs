using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChooseLevelPanel : MonoBehaviour
{
    [SerializeField] GameObject levelItemPrb;
    [SerializeField] ScrollRect scrollView;
    [SerializeField] UnityEngine.UI.Button startGameBtn;
    [SerializeField] RectTransform viewPort;
    private List<GameObject> levelItems;
    public void DisplayLevels(QuestionPack[] _pack)
    {
        Debug.Log("Display " +  _pack.Length);
        Clear();
        levelItems = new List<GameObject>();
        if (_pack.Length < 0) return;
        Vector2 viewPortSize = viewPort.GetComponent<RectTransform>().sizeDelta;
        viewPort.GetComponent<RectTransform>().sizeDelta = new Vector2(viewPortSize.x, levelItemPrb.GetComponent<RectTransform>().sizeDelta.y * _pack.Length);
        for (int i = 0; i < _pack.Length; i++)
        {
            
            QuestionPack pack = _pack[i];
            LevelItemUI levelItemUI = Instantiate(levelItemPrb).GetComponent<LevelItemUI>();
            levelItemUI.transform.SetParent(viewPort);
            levelItemUI.transform.localScale = Vector3.one;
            
            viewPort.GetComponent<RectTransform>().sizeDelta = new Vector2(viewPortSize.x, levelItemUI.GetComponent<RectTransform>().sizeDelta.y * _pack.Length);
            levelItemUI.SetLevelItem(i, pack.packName, pack.packDes, OnLevelItemOnClick);
            levelItems.Add(levelItemUI.gameObject);
            
        }
    }

    private void Clear()
    {
        if (levelItems == null) return;
       foreach(GameObject obj in  levelItems)
        {
            Destroy(obj);
        }
    }

    private void OnLevelItemOnClick(int _id)
    {
        QuestionPackManager.Instance.SelectPack(_id);
    }

    public void ReloadData()
    {
        QuestionPackManager.Instance.LoadPacks();
    }

    

}
