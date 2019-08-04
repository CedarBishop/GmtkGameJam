using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public void showTitle()
    {
        AudioManager.instance.Play("Button Click");
        ActiveScene.CurrentScene = SceneType.Title;
    }
}
