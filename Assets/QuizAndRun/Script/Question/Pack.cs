using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "QuestionPack", menuName = "Question/new QuestionPack")]
public class Pack : ScriptableObject
{
    public string packName;
    public string packDes;
    public List<Question> listQuestion;
    public string imageUrl = "";
    public int maxIncorrectAnswer = 0;
    public DateTime date = DateTime.Now;
    public int totalPlay = 0;
    public int totalLike = 0;
    public int ADMIN = 0;

}


