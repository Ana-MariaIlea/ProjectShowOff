using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI nectarOnBeeText;
    [SerializeField]
    TextMeshProUGUI nectarOnTrunkText;
    [SerializeField]
    TextMeshProUGUI resolutionScoreText;
    [SerializeField]
    GameObject resolutionScreen;
    [SerializeField]
    VideoClip englishCutscene;
    [SerializeField]
    VideoClip dutchCutscene;
    [SerializeField]
    VideoPlayer player;
    [SerializeField]
    GameObject cutsceneScreen;

    private void Start()
    {
        EventQueue.eventQueue.Subscribe(EventType.NECTARONBEETEXTCHANGE, OnNectarOnBeeTextChange);
        EventQueue.eventQueue.Subscribe(EventType.NECTARONTRUNKTEXTCHANGE, OnNectarOnTrunkTextChange);
        EventQueue.eventQueue.Subscribe(EventType.GAMEEND, OnGameEnd);

        switch (LocalisationSystem.language)
        {
            case LocalisationSystem.Language.English:
                player.clip = englishCutscene;
                break;
            case LocalisationSystem.Language.Dutch:
                player.clip = dutchCutscene;
                break;
        }
        //StartCoroutine(ExampleCoroutine());
    }
    public void OnNectarOnBeeTextChange(EventData eventData)
    {
        if (eventData is NectarOnBeeTextChangeEventData)
        {
            NectarOnBeeTextChangeEventData e = eventData as NectarOnBeeTextChangeEventData;
            nectarOnBeeText.text = e.number.ToString();
        }
    }

    public void OnNectarOnTrunkTextChange(EventData eventData)
    {
        if (eventData is NectarOnTrunkTextChangeEventData)
        {
            NectarOnTrunkTextChangeEventData e = eventData as NectarOnTrunkTextChangeEventData;
            nectarOnTrunkText.text = e.number.ToString();
        }
    }

    public void OnGameEnd(EventData eventData)
    {
        if (eventData is GameEndEventData)
        {
            //Time.timeScale = 0f;
            GameEndEventData e = eventData as GameEndEventData;
            if (HighscoreTable.instance)
                HighscoreTable.instance.AddHighScoreEntry(e.score, e.name);
            else Debug.Log("No highscore table");
            resolutionScoreText.text = "Score: " + e.score.ToString();
            cutsceneScreen.SetActive(true);
            player.Play();
            //resolutionScreen.SetActive(true);
            StartCoroutine(ExampleCoroutine());
        }
    }

    //IEnumerator ExampleCoroutine()
    //{
    //    //Print the time of when the function is first called.
    //    Debug.Log("Started Coroutine at timestamp : " + Time.time);

    //    //yield on a new YieldInstruction that waits for 5 seconds.
    //    yield return new WaitForSeconds(5);

    //    //After we have waited 5 seconds print the time again.
    //    Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    //}

    IEnumerator ExampleCoroutine()
    {
        Debug.Log("Corutine " + player.clip.length);
        yield return new WaitForSeconds((float)(player.clip.length));
        //yield return new WaitForSeconds(5);
        Debug.Log("Corutine done");
        Time.timeScale = 0f;
        //player.Stop();
        cutsceneScreen.SetActive(false);
        resolutionScreen.SetActive(true);
    }

    public void GoToBonusLevel()
    {
        if (GameManager.instance)
            GameManager.instance.GoToBonusLevel();
    else Debug.Log("No game manager");
    }
}
