using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hints : MonoBehaviour
{
    private Text text;
    void Start()
    {
        text = GetComponentInChildren<Text>();
        PlayerController.textSent += RecieveText;
    }

    void OnDestroy()
    {
        PlayerController.textSent -= RecieveText;
    }

    void RecieveText(string sentence)
    {
        text.text = sentence;
    }    
}
