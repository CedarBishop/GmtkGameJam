using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : MonoBehaviour
{

    public void StartGame()
    {
        AudioManager.instance.Play("Button Click");
        ActiveScene.CurrentScene = SceneType.Game;
    }
    
}
