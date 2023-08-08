using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;

public class QuestionPanel : MonoBehaviour
{
    [Header("UI reference")]
    [SerializeField] private Text questionTitleTxt;
    [SerializeField] private Text questionTxt;
    [SerializeField] private AnswerButton[] listAnswersBtns;
    [SerializeField] private CoolDownPanel coolDownPanel;

    [Header("Event raise")]

    [Header("Data")]
    [SerializeField] private QuestionData questionData;
    [SerializeField] private AnswerButton answerButtonUIPb;


    [Header("Animation")]
    [Space]
    [SerializeField] RectTransform questionRectTransform;
    [SerializeField] Vector2 questionPanelPosition;
    [SerializeField] float questionDurationScale;

    [Space]
    [SerializeField] RectTransform[] listButtonRect;
    [SerializeField] float delayShowButton;
    [SerializeField] Ease questionEase;
    [SerializeField] float answerButtonDuration;
    [SerializeField] Ease buttonEase;
    [SerializeField] float duration;
    [SerializeField] Vector3[] answerPosition;



    public void ShowQuestion(string _questionTitle, QuestionData _questionData)
    {
        questionData = _questionData;
        questionTitleTxt.text = _questionTitle;
        
        coolDownPanel.StartCoolDown(_questionData.LimitedTime1);
        questionTxt.text = questionData.QuestionContent;
        for(int i = 0; i <  listAnswersBtns.Length; i++)
        {
            if(questionData != null && questionData.ListAnswer[i] != null)
            {
                string ABCD = Alphabet.GetCharByIndex(i).ToString();
                string answer = questionData.ListAnswer[i].Content;
                bool isTrueAnswer = questionData.IndexTrueAnswer == i;
                listAnswersBtns[i].SetAnswerButton(ABCD, answer, isTrueAnswer);
            }
        }
        this.gameObject.SetActive(true);
        StartCoroutine(IE_Show());

    }



    public void LockAllButton()
    {
        foreach(AnswerButton answer in listAnswersBtns)
        {
            answer.LockButton();
        }
    }

    private IEnumerator IE_Show()
    {
        
        questionRectTransform.localScale = Vector3.zero;
        foreach(AnswerButton answer in listAnswersBtns)
        {
            answer.GetComponent<RectTransform>().localScale = Vector3.zero;
        }
        yield return new WaitForSeconds(1f);
        questionRectTransform.DOScale(1f, questionDurationScale);
        questionRectTransform.DOAnchorPos(questionPanelPosition,questionDurationScale);

        yield return new WaitForSeconds(delayShowButton);

        for (int i = 0; i < listAnswersBtns.Length; i++)
        {
            listAnswersBtns[i].GetComponent<RectTransform>().DOScale(1f, answerButtonDuration);
            listAnswersBtns[i].ResetButton();
        }


    }

    private IEnumerator IE_Hide()
    {
        questionRectTransform.DOScale(0f, questionDurationScale);
        coolDownPanel.Hide();

        for (int i = 0; i < listAnswersBtns.Length; i++)
        {
            listAnswersBtns[i].HideButton();
        }
        yield return null;
    }    

    public void ShowResult()
    {
        StartCoroutine(IE_ShowResult());
    }

    private IEnumerator IE_ShowResult()
    {
        coolDownPanel.StopCoolDown();
        foreach(AnswerButton answerButton in listAnswersBtns)
        {
            answerButton.LockButton();
        }    
        yield return new WaitForSeconds(1f);
        foreach (AnswerButton answer in listAnswersBtns)
        {
            answer.ShowResult();
        }
        yield return new WaitForSeconds(3f);
        StartCoroutine(IE_Hide());
    }



}


