using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using System.Collections;

public class AnswerButton : MonoBehaviour
{
    [Header("UI references")]
    [SerializeField] private Text answerText;
    [SerializeField] private Sprite normalBg, trueBg, falseBg;
    [SerializeField] private Button answerBtn;
    [SerializeField] QuestionController questionController;

    private bool isTrueAnswer = false;
    private bool isButtonClicked = false;

    private void Awake()
    {
        answerBtn.onClick.RemoveAllListeners();
        answerBtn.onClick.AddListener(() =>
        {
            isButtonClicked = true;
            SoundManager.Instance.Play("MouseClick");
            questionController.OnQuestionAnswer(isTrueAnswer);
        });
    }


    public void SetAnswerButton(string _answer , bool isTrue)
    {
        answerText.text = _answer;
        isTrueAnswer = isTrue;
    }

    public void ResetButton()
    {
        answerBtn.interactable = true;
        answerBtn.image.sprite = normalBg;
        isButtonClicked = false;
        gameObject.SetActive(true);
    }

    public void HideButton()
    {
        this.GetComponent<RectTransform>().DOScale(0f, 0.5f).SetEase(Ease.Flash);
    }

    public void LockButton()
    {
        answerBtn.interactable = false;
    }

    public void ShowResult()
    {
        if (isTrueAnswer)
        {
            if (isButtonClicked)
            {
                SoundManager.Instance.Play("Correct");
            }
            else
            {
                SoundManager.Instance.Play("Incorrect");
            }
            answerBtn.image.sprite = trueBg;
        }
        else
        {
            gameObject.SetActive(isButtonClicked);
            answerBtn.image.sprite = falseBg;
        }
        
    }


    public void OnHoverEnter()
    {
        GetComponent<RectTransform>().DOScale(1.05f, 0.1f).SetEase(Ease.OutBounce);
        if (isButtonClicked) return;
        SoundManager.Instance.Play("MouseHover");
    }

    public void OnHoverExit()
    {
        if (isButtonClicked) return;
        GetComponent<RectTransform>().DOScale(1f, 0.1f).SetEase(Ease.OutBounce);
    }





}
