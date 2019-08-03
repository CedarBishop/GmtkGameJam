using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public void showTitle()
    {
        ActiveScene.CurrentScene = SceneType.Title;
    }
}
