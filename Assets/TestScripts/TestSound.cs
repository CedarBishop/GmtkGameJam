using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayIt()
    {
        int mynum = count % 6;
        if(mynum == 0)
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Gate_Open");
        }
        else if (mynum == 1)
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Gate_Close");
        }
        else if (mynum == 2)
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Menu_Hover");
        }
        else if (mynum == 3)
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Menu_Select");
        }
        else if (mynum == 4)
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Switch");
        }
        else if (mynum == 5)
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Teleport");
        }
        count++;
    }
}
