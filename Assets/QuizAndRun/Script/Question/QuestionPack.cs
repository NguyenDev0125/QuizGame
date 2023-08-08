using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Question Pack" , menuName = "QuestionPack/new Pack")]
public class QuestionPack : ScriptableObject
{
    
    [SerializeField] string packName;
    [SerializeField] string packDes;
    [SerializeField] QuestionData[] questionDatas;
    [SerializeField] int maxAnswerInCorrect;

    public string PackName { get => packName; }
    public QuestionData[] QuestionDatas { get => questionDatas; }
    public int MaxAnswerInCorrect { get => maxAnswerInCorrect; }
    public string PackDes { get => packDes;  }
}
