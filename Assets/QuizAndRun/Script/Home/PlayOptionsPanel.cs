using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayOptionsPanel : MonoBehaviour
{
    [SerializeField] GameObject levelItemPrb;
    [SerializeField] List<LevelItemUI> levelItems;
    [SerializeField] ScrollView scrollView;
    [SerializeField] Button i;
    [SerializeField] RectTransform viewPort;

    public void DisplayLevels(QuestionPack[] _pack)
    {
        if (_pack.Length < 0) return;
        for (int i = 0; i < _pack.Length; i++)
        {
            
            QuestionPack pack = _pack[i];
            
            LevelItemUI levelItemUI = Instantiate(levelItemPrb).GetComponent<LevelItemUI>();
            levelItemUI.transform.SetParent(viewPort);
            levelItemUI.transform.localScale = Vector3.one;
            Debug.Log("Pack des " + pack.packDes);
            levelItemUI.SetLevelItem(i, pack.name, pack.packDes, OnLevelItemOnClick);
            levelItems.Add(levelItemUI);

        }
    }

    private void OnLevelItemOnClick(int _id)
    {
        QuestionPackManager.Instance.SelectPack(_id);
    }

}
