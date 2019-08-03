using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : MonoBehaviour
{

    public void StartGame()
    {
        ActiveScene.CurrentScene = SceneType.Game;
    }
    
}
