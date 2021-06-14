using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.LoadGame();
        }
        else
        {
            Debug.Log("Game manager does not exist");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
