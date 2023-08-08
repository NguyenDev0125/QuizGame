using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using System.Collections;

public class AnswerButton : MonoBehaviour
{
    [Header("UI references")]
    [SerializeField] private Text ABCDText;
    [SerializeField] private Text answerText;
    [SerializeField] private Sprite normalBg, trueBg, falseBg;
    [SerializeField] private Button answerBtn;

    [Header("Event listener")]
    [SerializeField] BoolEventChanel OnAnswerButtonClick;
    private bool isTrueAnswer = false;
    private bool isButtonClicked = false;

    private void Awake()
    {
        answerBtn.onClick.RemoveAllListeners();
        answerBtn.onClick.AddListener(() =>
        {
            OnAnswerButtonClick.Raise(isTrueAnswer);
            isButtonClicked = true;
            SoundManager.Instance.Play(SoundName.MouseClick);
            
        });
    }


    public void SetAnswerButton(string _ABCD, string _answer , bool _isTrueAnswer)
    {
        ABCDText.text = _ABCD + ".";
        answerText.text = _answer;
        isTrueAnswer = _isTrueAnswer;
    }

    public void ResetButton()
    {
        answerBtn.interactable = true;
        answerBtn.image.sprite = normalBg;
        isButtonClicked = false;
        answerBtn.gameObject.SetActive(true);
        this.GetComponent<RectTransform>().DOScale(1f, 0f);
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
                SoundManager.Instance.Play(SoundName.Correct);
            }
            else
            {
                SoundManager.Instance.Play(SoundName.Incorrect);
            }
            answerBtn.image.sprite = trueBg;
        }
        else
        {
            gameObject.SetActive(isButtonClicked);
            answerBtn.image.sprite = falseBg;
        }
    }

    public void PlaySoundOnHover()
    {
        SoundManager.Instance.Play(SoundName.MouseClick2);
    }

    

  

}
