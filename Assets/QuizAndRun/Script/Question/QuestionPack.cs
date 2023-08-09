using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Question Pack" , menuName = "QuestionPack/new Pack")]
public class QuestionPack : ScriptableObject
{
    
     public string packName;
     public string packDes;
     public QuestionData[] questionDatas;
     public int maxAnswerInCorrect;

}
    

