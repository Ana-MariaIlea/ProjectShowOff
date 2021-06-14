using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int titleScreenInterger;
    public int mainScreenInterger;
    public GameObject loadingScreen;
    public Action StartGame;

    public string playerName = null;
    private void Awake()
    {
        if (instance == null)
            instance = this;

        SceneManager.LoadSceneAsync(titleScreenInterger, LoadSceneMode.Additive);
    }
    private void Start()
    {
        loadingScreen.SetActive(false);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    public void LoadGame()
    {
        if (playerName != null)
        {
            loadingScreen.SetActive(true);
            scenesLoading.Add(SceneManager.UnloadSceneAsync(titleScreenInterger));
            scenesLoading.Add(SceneManager.LoadSceneAsync(mainScreenInterger, LoadSceneMode.Additive));

            StartCoroutine(GetSceneLoadProgress());
        }
    }

    public void BackToMenu()
    {
        loadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync(mainScreenInterger));
        scenesLoading.Add(SceneManager.LoadSceneAsync(titleScreenInterger, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
        playerName = null;
    }

    public IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                yield return null;

            }
        }
        loadingScreen.SetActive(false);

        // yield return new WaitForSeconds(3);
        scenesLoading = new List<AsyncOperation>();
        //StartGame.Invoke();
        //intro.
    }



    public void ChangeText(string text)
    {
        Debug.Log("player name is changed in game manager");
        playerName = text;
    }
}
