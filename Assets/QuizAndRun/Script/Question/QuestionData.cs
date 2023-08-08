using System;
using UnityEngine;
[Serializable]
public class QuestionData
{
    [SerializeField] string questionContent;
    [SerializeField] AnswerData[] listAnswer;
    [SerializeField] int indexTrueAnswer;
    [SerializeField] int LimitedTime;

    public string QuestionContent { get => questionContent; }
    public AnswerData[] ListAnswer { get => listAnswer; }
    public int IndexTrueAnswer { get => indexTrueAnswer; }
    public int LimitedTime1 { get => LimitedTime; }
}

