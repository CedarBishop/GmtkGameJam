using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public void showCredits()
    {
        ActiveScene.CurrentScene = SceneType.Credits;
    }
}
