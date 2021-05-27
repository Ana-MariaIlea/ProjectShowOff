﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //SceneManager.LoadScene(3);
        GameManager.instance.LoadGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}