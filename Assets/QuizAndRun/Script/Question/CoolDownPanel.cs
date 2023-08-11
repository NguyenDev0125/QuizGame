using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownPanel : MonoBehaviour
{
    [SerializeField] Slider timeSlide;
    [SerializeField] VoidEventChanel OnTimeOut;
    private float timer = 0;
    private bool isCooldown = false;
    private bool isTimeout = false;
    public void StartCoolDown(float _maxValue)
    {
        timer = _maxValue;
        timeSlide.maxValue = _maxValue;
        timeSlide.value = _maxValue;
        isCooldown = true;
        Show();
    }

    private void Update()
    {
        if (isCooldown)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
                timeSlide.value = timer;
            }
            else
            {
                if(!isTimeout)
                {
                    OnTimeOut.Raise();
                    isTimeout = true;
                }
                
            }

        }

    }
    
    public void Hide()
    {
        GetComponent<RectTransform>().DOScale(0f, 0.5f).SetEase(Ease.OutBounce);
    }

    public void Show()
    {
        GetComponent<RectTransform>().DOScale(1f, 0.5f).SetEase(Ease.InBounce);
    }
    public void UpdateSlide(float _duration)
    {
        timeSlide.value = _duration;
    }

    public void StopCoolDown()
    {
        isCooldown = false;
    }
}
