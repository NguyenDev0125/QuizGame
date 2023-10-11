using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPackPanel : MonoBehaviour
{
    [SerializeField] PackUI packUIPrb;
    [SerializeField] RectTransform viewPort;
    [SerializeField] PackLoader packLoader;
    private List<PackUI> levelItems;

    private void OnEnable()
    {
        packLoader.LoadPacks();
    }
    public void DisplayPacks(List<Pack> _packs)
    {
        ClearUIList();
        levelItems = new List<PackUI>();
        if (_packs.Count < 0) return;

        for (int i = 0; i < _packs.Count; i++)
        {
            PackUI packUIClone = Instantiate(packUIPrb, viewPort).GetComponent<PackUI>();
            packUIClone.transform.localScale = Vector3.one;
            Pack pack = _packs[i];
            packUIClone.Init(i, pack.packName, pack.packDes, OnLevelItemOnClick);
            levelItems.Add(packUIClone);
        }

    }
    private void ClearUIList()
    {
        if (levelItems == null) return;
        foreach (PackUI obj in levelItems)
        {
            Destroy(obj.gameObject);
        }
        levelItems.RemoveRange(0,levelItems.Count-1);
    }

    private void OnLevelItemOnClick(int _id)
    {
        packLoader.SelectPack(_id);
    }
}
