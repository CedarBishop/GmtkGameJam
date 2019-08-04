using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public string[] sentences;
    public bool isEndOfLevel;

    public delegate void SendSentences(string[] Sentences, bool answer);
    public static event SendSentences sendSentences;

    public void InteractNpc ()
    {
        if (sendSentences != null)
        {
            sendSentences(sentences,isEndOfLevel);
        }
    }
}
