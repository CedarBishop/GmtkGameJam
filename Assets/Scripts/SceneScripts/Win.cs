﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public void showWin()
    {
        ActiveScene.CurrentScene = SceneType.Win;
    }
}
