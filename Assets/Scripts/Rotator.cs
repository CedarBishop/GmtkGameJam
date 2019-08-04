using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    bool isOpen;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && isOpen == false)
        {
            transform.Rotate(new Vector3 (0,0,-90));
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            transform.Rotate(new Vector3(0,0, 90));
            isOpen = false;
        }
    }
}
