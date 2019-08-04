using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public float typingSpeed;

    private Text text;
    private Image image;
    private Button button;
    private string[] Sentences;
    int index;
    bool dialogueHasStarted;
    bool isEndingDialogue;

    void Start ()
    {
        text = GetComponentInChildren<Text>();
        image = GetComponentInChildren<Image>();
        button = GetComponentInChildren<Button>();
        ChildrenStatus(false);
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

    void StartedDialogue (string [] sentences)
    {
        index = 0;
        Sentences = sentences;
        dialogueHasStarted = true;
        StartCoroutine("Type");
        ChildrenStatus(true);
    }

    IEnumerator Type ()
    {       
        foreach (char letter in Sentences[index].ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(typingSpeed);
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
    }

    private void ChildrenStatus(bool answer)
    {
        image.gameObject.SetActive(answer);
        text.gameObject.SetActive(answer);
        button.gameObject.SetActive(answer);
    }
}
