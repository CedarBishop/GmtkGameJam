using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.gameObject.GetComponent<PlayerController>())
        {
            showWin();
        }
    }

    void showWin()
    {
        ActiveScene.CurrentScene = SceneType.Win;
    }
}
