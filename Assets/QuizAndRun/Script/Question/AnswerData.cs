using System;
using UnityEngine;

[Serializable]
public class AnswerData
{
    [SerializeField] string _content;
    public string Content { get => _content;}
}
