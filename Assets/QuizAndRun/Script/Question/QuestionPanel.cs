using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using System;
using UnityEngine.Networking;

public class QuestionPanel : MonoBehaviour
{
    [Header("UI dbRef")]
    [SerializeField] QuestionController questionController;
    [SerializeField] private Text questionTitleTxt;
    [SerializeField] private Text questionTxt;
    [SerializeField] private AnswerButton[] listAnswersBtns;
    [SerializeField] Image questionImg;
    
    [SerializeField] private CoolDownPanel coolDownPanel;

    private Question questionData;

    [Header("Animation")]
    [SerializeField] CanvasGroup canvas;
    [SerializeField] RectTransform questionPanel;
    [SerializeField] RectTransform[] listButtonRect;
    [SerializeField] float fadeInDuration;
    [SerializeField] float fadeOutDuation;
    [SerializeField] Ease easeInEase;
    [SerializeField] float buttonFadeDuration;
    [SerializeField] float delayTimeBetweenTwoBtn;
    [SerializeField] Ease buttonEase;


    public void DisplayQuestion(int questionIndex, Question _questionData )
    {
        questionData = _questionData;
        questionTitleTxt.text = "# "+questionIndex;
        coolDownPanel.StartCoolDown(_questionData.LimitedTime);
        questionTxt.text = questionData.questionContent;
        for(int i = 0; i <  listAnswersBtns.Length; i++)
        {
            if(questionData != null && questionData.listAnswer[i] != null)
            {
                string answer = questionData.listAnswer[i];
                listAnswersBtns[i].SetAnswerButton(answer, answer == questionData.listAnswer[questionData.trueAnswerIndex]);
            }
        }
        if(questionData.imageUrl != "")
        {
            questionImg.gameObject.SetActive(false);
            StartCoroutine(IE_LoadImage(_questionData.imageUrl));
        }
        RefreshPanel();
        this.gameObject.SetActive(true);
        StartCoroutine(IE_FadeIn());

    }

    private IEnumerator IE_LoadImage(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        Texture2D text = ((DownloadHandlerTexture)www.downloadHandler).texture;
        questionImg.preserveAspect = true;
        Sprite spr = Sprite.Create(text, new Rect(0, 0, text.width, text.height), Vector2.zero);
        questionImg.sprite = spr;
        questionImg.preserveAspect = true;
        questionImg.gameObject.SetActive(true);
    }
    private void RefreshPanel()
    {
        foreach(AnswerButton answer in listAnswersBtns)
        {
            answer.ResetButton();
        }
        questionImg.gameObject.SetActive(false);
    }

    public void LockAllButton()
    {
        foreach(AnswerButton answer in listAnswersBtns)
        {
            answer.LockButton();
        }
    }

    private IEnumerator IE_FadeIn()
    {
        foreach (RectTransform rect in listButtonRect)
        {
            rect.transform.localScale = Vector3.zero;
        }
        canvas.alpha = 0f;
        questionPanel.transform.localPosition = new Vector3(0, -1416, 0);
        questionPanel.DOAnchorPos(Vector3.zero, fadeInDuration);
        canvas.DOFade(1f, fadeInDuration).SetEase(easeInEase);
        yield return new WaitForSeconds(fadeOutDuation);
        foreach(RectTransform rect in listButtonRect)
        {
            rect.DOScale(1f, buttonFadeDuration).SetEase(buttonEase);
            yield return new WaitForSeconds(delayTimeBetweenTwoBtn);
        }
        yield return null;
    }

    private IEnumerator IE_FadeOut()
    {
        foreach (RectTransform rect in listButtonRect)
        {
            rect.DOScale(0f, buttonFadeDuration).SetEase(buttonEase);
            yield return new WaitForSeconds(delayTimeBetweenTwoBtn);
        }
        canvas.DOFade(0f,fadeOutDuation).SetEase(easeInEase);
        yield return new WaitForSeconds(fadeOutDuation);
        questionController.OnResultDisplayed();
        
        yield return null;
    }    

    public void DisplayResult()
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
        yield return new WaitForSeconds(0.6f);
        foreach (AnswerButton answer in listAnswersBtns)
        {
            answer.ShowResult();
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(IE_FadeOut());
        
    }

    public void OnHoverEnter()
    {
        questionPanel.DOScale(1.05f, 0.5f).SetEase(Ease.OutBack);
    }

    public void OnHoverExit()
    {
        questionPanel.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
    }



}


