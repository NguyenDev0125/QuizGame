using System;
using UnityEngine;
using UnityEngine.UI;

public class PackUI : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] Text title;
    [SerializeField] Text description;
    [SerializeField] Button button;
    
    public void Init(int _id, string _title , string _des,Action<int> _onClick)
    {
        id = _id;
        title.text = _title;
        description.text = _des;
        button.onClick.AddListener(() => _onClick(id));
    }
}
