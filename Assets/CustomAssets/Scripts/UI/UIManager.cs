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

        if (player != null)
            switch (LocalisationSystem.language)
            {
                case LocalisationSystem.Language.English:
                    player.clip = englishCutscene;
                    break;
                case LocalisationSystem.Language.Dutch:
                    player.clip = dutchCutscene;
                    break;
            }
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
            if (GameManager.instance)
            {
                GameManager.instance.playerScore = e.score;
            }
                if (HighscoreTable.instance)
                HighscoreTable.instance.AddHighScoreEntry(e.score, e.name);
            else Debug.Log("No highscore table");
            resolutionScoreText.text = "Score: " + e.score.ToString();
            cutsceneScreen.SetActive(true);
            player.Play();
            StartCoroutine(ExampleCoroutine());
        }
    }

    IEnumerator ExampleCoroutine()
    {
        Debug.Log("Corutine " + player.clip.length);
        yield return new WaitForSeconds((float)(player.clip.length));
        //yield return new WaitForSeconds(5);
        Debug.Log("Corutine done");
        Time.timeScale = 0f;
        //player.Stop();
        Cursor.lockState = CursorLockMode.None;
        cutsceneScreen.SetActive(false);
        resolutionScreen.SetActive(true);
    }

    public void GoToBonusLevel()
    {
        if (GameManager.instance)
        {
            Time.timeScale = 1f;
            EventQueue.eventQueue.UnSubscribe(EventType.NECTARONBEETEXTCHANGE, OnNectarOnBeeTextChange);
            EventQueue.eventQueue.UnSubscribe(EventType.NECTARONTRUNKTEXTCHANGE, OnNectarOnTrunkTextChange);
            EventQueue.eventQueue.UnSubscribe(EventType.GAMEEND, OnGameEnd);
            GameManager.instance.GoToBonusLevel();
        }
        else Debug.Log("No game manager");
    }
}
