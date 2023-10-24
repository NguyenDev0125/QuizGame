using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardItemUI : MonoBehaviour
{
    [SerializeField] Sprite avtTop1;
    [SerializeField] Sprite avtTop2;
    [SerializeField] Sprite avtTop3;
    [SerializeField] Sprite goldCup;
    [SerializeField] Text usernameTxt;
    [SerializeField] Text scoreTxt;
    [SerializeField] Image avtImage;
    [SerializeField] Text topTxt;
    [SerializeField] Image cupImg;
    public void SetItem(string username ,string score, int top)
    {
        usernameTxt.text = username;
        scoreTxt.text =  score;
        topTxt.text = "Top " + (top + 1);
        if (top >= 3) topTxt.color = Color.white;
        else cupImg.sprite = goldCup;
        switch (top)
        {
            case 0: avtImage.sprite = avtTop1; break;
            case 1: avtImage.sprite = avtTop2; break;
            case 2: avtImage.sprite = avtTop3; break;
        }
    }
}
