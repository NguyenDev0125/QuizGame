
using UnityEngine;
using UnityEngine.UI;

public class QuestionItemUI : MonoBehaviour
{
    [SerializeField] Text questionTxt;

    public void SetText(string _text)
    {
        questionTxt.text = _text;
    }
}
