using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    void Start ()
    {
        WinCondition.OnWin += showWin;
    }

    void OnDestroy ()
    {
        WinCondition.OnWin -= showWin;
    }

    public void showWin()
    {
        ActiveScene.CurrentScene = SceneType.Win;
    }
}
