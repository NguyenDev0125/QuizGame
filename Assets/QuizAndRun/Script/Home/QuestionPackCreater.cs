using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class QuestionPackCreater 
{
    private QuestionPack pack;
    [SerializeField] VoidEventChanel OnAddQuestionToPack;


    public List<QuestionData> ListQuestion
    {
        get
        {
            return pack.listQuestion;
        }
    }
    public void CreateNewPack()
    {
        pack = new QuestionPack();
        
    }

    public void AddQuestion(string _ques , string _a , string _b, string _c , string _d , int _timeLimit , int _trueAnswerIndex)
    {
        if (pack == null) pack = new QuestionPack();
        if (pack.listQuestion == null) pack.listQuestion = new List<QuestionData>();
        
        QuestionData data = new QuestionData();
        data.questionContent = _ques;
        data.listAnswer[0] = _a;
        data.listAnswer[1] = _b;
        data.listAnswer[2] = _c;
        data.listAnswer[3] = _d;
        data.LimitedTime = _timeLimit;
        data.trueAnswerIndex = _trueAnswerIndex;
        
        pack.listQuestion.Add(data);
        
    }

    public void UploadPack(string _packName , string _packDes)
    {
        if(pack != null && pack.listQuestion != null)
        {
            pack.packName = _packName;
            pack.packDes = _packDes;
            QuestionPackManager.Instance.SavePack(pack);
        }
    }
}
