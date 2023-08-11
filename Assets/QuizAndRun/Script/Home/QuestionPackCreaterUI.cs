using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestionPackCreaterUI : MonoBehaviour
{
    [SerializeField] InputField titleTxt;
    [SerializeField] InputField desTxt;
    [SerializeField] InputField questionTxt;
    [SerializeField] InputField timeLimitTxt;
    [SerializeField] InputField aTxt;
    [SerializeField] InputField bTxt;
    [SerializeField] InputField cTxt;
    [SerializeField] InputField dTxt;
    [SerializeField] InputField trueAnswer;
    [SerializeField] Button addBtn;
    [SerializeField] Button uploadBtn;
    [SerializeField] QuestionUIElement itemPb;
    [SerializeField] Transform root;
    
    

    private QuestionPackCreater creater;
    private void Awake()
    {
        addBtn.onClick.AddListener(AddQuestion);
        uploadBtn.onClick.AddListener(UploadPack);
        creater = new QuestionPackCreater();
        creater.CreateNewPack();   
    }


    private void Refresh()
    {
        titleTxt.text = "";
        desTxt.text = "";
        questionTxt.text = "";
        timeLimitTxt.text = "";
        aTxt.text = "";
        bTxt.text = "";
        cTxt.text = "";
        desTxt.text = "";
    }

    public void UpdateListQuestion()
    {

    }

    private void AddQuestion()
    {
        if(CheckQuestionIsCorrect())
        {
            int timeLimit = int.Parse(timeLimitTxt.text);
            int trueIndex = 0;
            if (trueAnswer.text.ToLower() == "b") trueIndex = 1;
            if (trueAnswer.text.ToLower() == "c") trueIndex = 2;
            if (trueAnswer.text.ToLower() == "d") trueIndex = 3;
            creater.AddQuestion(questionTxt.text , aTxt.text , bTxt.text , cTxt.text , dTxt.text , timeLimit , trueIndex );
            QuestionUIElement item = Instantiate(itemPb);
            item.SetText(questionTxt.text );
            
            item.transform.SetParent(root);
            item.transform.localScale = Vector3.one;
            RectTransform contentRect = root.GetComponent<RectTransform>();
            contentRect.sizeDelta = new Vector2( contentRect.sizeDelta.x,item.GetComponent<RectTransform>().sizeDelta.y * creater.ListQuestion.Count + 20);
        }
        else
        {
            Debug.Log("Bad parametter");
        }
    }

    private void UploadPack()
    {
        if(CheckPackIsCorrect())
        {
            creater.UploadPack(titleTxt.text , desTxt.text);
        }
        else
        {
            Debug.Log("Bad parametter");
        }
    }

    private bool CheckQuestionIsCorrect()
    {
        if (questionTxt.text == "") return false;
        if (aTxt.text == "") return false;
        if (bTxt.text == "") return false;
        if (cTxt.text == "") return false;
        if (dTxt.text == "") return false;
        if(timeLimitTxt.text == "") return false;
        int o = 0;
        if(int.TryParse(timeLimitTxt.text ,out o) == false || o == 0) return false;
        string t = trueAnswer.text.ToLower();
        if (t.Contains("a") || t.Contains("b") || t.Contains("c") || t.Contains("d") ) return true;
        return false;
    }

    private bool CheckPackIsCorrect()
    {
        if (titleTxt.text == "") return false;
        if (desTxt.text == "") return false;
        if(creater.ListQuestion.Count == 0) return false;
        return true;
    }
}
