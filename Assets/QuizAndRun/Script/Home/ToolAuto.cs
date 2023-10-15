using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ToolAuto : MonoBehaviour
{
    public bool Active = false;
    public Text text;
    public TextMeshPro tmp;
    private void OnValidate()
    {
        if (Active)
        {
            Active = false;
            
            foreach(Text text in FindObjectsOfType<Text>())
            {
            }
        }
    }

}
