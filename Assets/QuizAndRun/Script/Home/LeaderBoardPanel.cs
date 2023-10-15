
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

public class LeaderBoardPanel : MonoBehaviour
{
    [SerializeField] LeaderBoardItemUI itemPrb;
    [SerializeField] ScrollRect rect;
    string LeaderBoardPath = "Accounts/";
    List<LeaderBoardItemUI> items;
    private void OnEnable()
    {
        if (items == null) items = new List<LeaderBoardItemUI>();
        GetLeaderBoard();
    }
    private void GetLeaderBoard()
    {
        DatabaseManager.Instance.GetAllUserAccount(LeaderBoardPath, (listJson) =>
        {
            Debug.Log(listJson.Count);
            var users = from json in listJson
                        orderby json["score"] descending
                        select json;
            for(int i = 0; i < users.Count(); i++)
            { 
                LeaderBoardItemUI clone = Instantiate(itemPrb,rect.content);
                clone.SetItem(users.ElementAt(i)["username"] , users.ElementAt(i)["score"], i);
                items.Add(clone);
            }
        });
    }

    private void ClearleaderBoar()
    {
        foreach (var item in items)
        {
            Destroy(item.gameObject);
        }
    }

}
