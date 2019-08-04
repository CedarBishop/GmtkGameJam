using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType { Title, Game, Win, Rules, Credits };

public class ActiveScene : MonoBehaviour
{

    [SerializeField] private static SceneType currentScene;
    public static SceneType CurrentScene
    {
        get { return currentScene; }
        set
        {
            AudioManager.instance.Play("Button Click");
            currentScene = value;
            switch (currentScene)
            {
                case SceneType.Title:
                    SceneManager.LoadScene("Title");
                    break;
                case SceneType.Game:
                    SceneManager.LoadScene("GameScene");
                    break;
                case SceneType.Win:
                    SceneManager.LoadScene("Win");
                    break;
                case SceneType.Rules:
                    SceneManager.LoadScene("Rules");
                    break;
                case SceneType.Credits:
                    SceneManager.LoadScene("Credits");
                    break;
                default:
                    break;
            }
        }

    }

}
