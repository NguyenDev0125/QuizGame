using System;
using UnityEngine;
[Serializable]
public class QuestionData
{
    public string questionContent;
    public AnswerData[] listAnswer;
    public int indexTrueAnswer;
    public int LimitedTime;

}

