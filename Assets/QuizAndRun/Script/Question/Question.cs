using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Question
{
    public string questionContent;
    public string[] listAnswer = new string[4];
    public int trueAnswerIndex;
    public int LimitedTime;
    public string imageUrl = "";
}

