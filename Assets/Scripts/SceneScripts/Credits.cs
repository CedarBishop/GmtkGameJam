using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public void showCredits()
    {
        AudioManager.instance.Play("Button Click");
        ActiveScene.CurrentScene = SceneType.Credits;
    }
}
