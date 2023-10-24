using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessengeUI : MonoBehaviour
{
    [SerializeField] Text usernameTxt;
    [SerializeField] Text messengeTxt;

    public void SetText(string username , string mess)
    {
        usernameTxt.text = username;
        messengeTxt.text = mess;
    }
}
