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
    public int bonusScreenInterger;
    public GameObject loadingScreen;
    public Action StartGame;
    public ProgressBar bar;

    [HideInInspector]
    public string playerName = null;
    [HideInInspector]
    public int playerScore = 0;
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


    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    public void LoadGame()
    {
        LoadScene(titleScreenInterger, mainScreenInterger);
    }

    public void LoadScene(int scenetoUnload, int sceneToLoad)
    {
        if (playerName != null)
        {
            loadingScreen.SetActive(true);
            scenesLoading.Add(SceneManager.UnloadSceneAsync(scenetoUnload));
            scenesLoading.Add(SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive));

            StartCoroutine(GetSceneLoadProgress());
        }
    }

    public void GoToBonusLevel()
    {
        LoadScene(mainScreenInterger, bonusScreenInterger);
    }

    public void BackToMenuFromMainScene()
    {
        LoadScene(mainScreenInterger, titleScreenInterger);
        playerName = null;
    }


    public void BackToMenuFromBonusScene()
    {
        LoadScene(bonusScreenInterger, titleScreenInterger);
        playerName = null;
    }


    float totalProgress;

    public IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {

                totalProgress = 0;

                foreach (AsyncOperation operation in scenesLoading)
                {
                    totalProgress += operation.progress;
                }

                totalProgress = (totalProgress / scenesLoading.Count) * 100;
                bar.current = Mathf.RoundToInt(totalProgress);
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
