using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionStats : MonoBehaviour
{
    public static GameSessionStats instance;
    [SerializeField]
    private HumanStates playerPosition;

    [SerializeField]
    private DifficultySettings currentDifficulty;

    private int playerScore;
    private string playerName;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        EventQueue.eventQueue.Subscribe(EventType.CHANGEZONE, OnPlayerZoneChanged);
    }


    public void OnPlayerZoneChanged(EventData eventData)
    {
        if (eventData is ChangePlayerLocationEventData)
        {
            ChangePlayerLocationEventData e = eventData as ChangePlayerLocationEventData;
            SetPlayerPositionZone(e.zone);
        }
    }

    public void SetPlayerPositionZone(HumanStates newZone)
    {
        playerPosition = newZone;
    }

    public HumanStates GetPlayerPositionZone()
    {
        return playerPosition;
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
    }


    public string GetPlayerName()
    {
        return playerName;
    }

    public void SetPlayerScore(int score)
    {
        playerScore = score;
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }


    public void CheckDifficulty(EventData eventData)
    {
        if(eventData is CheckDifficultyEventData)
        {
            CheckDifficultyEventData e = eventData as CheckDifficultyEventData;
            if (playerScore < e.DifficultyCheck.nectarMin)
            {
                EventQueue.eventQueue.AddEvent(new ChangeDifficultyEventData(e.DifficultyCheck.easierDifficulty));
            }else if (playerScore > e.DifficultyCheck.nectarMax)
            {
                EventQueue.eventQueue.AddEvent(new ChangeDifficultyEventData(e.DifficultyCheck.harderDifficulty));
            }
        }
    }

}
