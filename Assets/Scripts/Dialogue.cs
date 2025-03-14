﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    private Text text;
    private Image image;
    private Button button;
    private string[] Sentences;
    int index;
    public static bool dialogueHasStarted;
    bool isEndingDialogue;

    void Start ()
    {
        text = GetComponentInChildren<Text>();
        image = GetComponentInChildren<Image>();
        button = GetComponentInChildren<Button>();
        ChildrenStatus(false);
        Npc.sendSentences += StartedDialogue;
    } 

    void OnDestroy ()
    {
        Npc.sendSentences += StartedDialogue;
    }

    void Update ()
    {
        if (dialogueHasStarted)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                NextSentence();
            }
        }
    }

    void StartedDialogue (string [] sentences, bool answer)
    {
        index = 0;
        Sentences = sentences;
        isEndingDialogue = answer;
        dialogueHasStarted = true;
        ClearText();
        StartCoroutine("Type");
        ChildrenStatus(true);
    }

    IEnumerator Type ()
    {       
        foreach (char letter in Sentences[index].ToCharArray())
        {
            text.text += letter;
            yield return new WaitForEndOfFrame();
        }        
    }

    void ClearText ()
    {
        StopCoroutine("Type");
        text.text = "";
    }

    public void NextSentence ()
    {
        ClearText();
        if (index < Sentences.Length - 1)
        {
            index++;
            StartCoroutine("Type");
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue ()
    {
        if (isEndingDialogue)
        {
            //end game here
        }
        ClearText();
        ChildrenStatus(false);
        dialogueHasStarted = false;
    }

    private void ChildrenStatus(bool answer)
    {
        image.gameObject.SetActive(answer);
        text.gameObject.SetActive(answer);
        button.gameObject.SetActive(answer);
    }
}