using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionUIElement : MonoBehaviour
{
    [SerializeField] Text questionTxt;

    public void SetText(string _text)
    {
        questionTxt.text = _text;
    }
}
