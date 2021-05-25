using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionStats : MonoBehaviour
{
    public static GameSessionStats instance;
    [SerializeField]
    private HumanStates playerPosition;

    private int playerScore;
    private string playerName;
    // Start is called before the first frame update
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

}
