using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameSessionStats : MonoBehaviour
{
    public static GameSessionStats instance;
    [SerializeField]
    private HumanStates playerPosition;

    [SerializeField]
    private List<DifficultySettings> settings;

    [SerializeField]
    private DifficultySettings currentDifficulty;


    private List<ZoneSettings> zones;


    private int playerScore;
    private string playerName;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
            instance = this;

        zones = FindObjectsOfType<ZoneSettings>().ToList();

    }
    void Start()
    {
        for (int i = 0; i < settings.Count; i++)
        {
            for (int j = i + 1; j < settings.Count; j++)
            {
                if (settings[j].DifficultyLevel > settings[i].DifficultyLevel)
                {
                    DifficultySettings aux = settings[i];
                    settings[i] = settings[j];
                    settings[j] = aux;
                }
            }
        }
        EventQueue.eventQueue.Subscribe(EventType.CHANGEZONE, OnPlayerZoneChanged);
        EventQueue.eventQueue.Subscribe(EventType.CHECKDIFFICULTY, OnCheckDifficulty);

        
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


    public void OnCheckDifficulty(EventData eventData)
    {
        if (eventData is CheckDifficultyEventData)
        {
            CheckDifficultyEventData e = eventData as CheckDifficultyEventData;
            if (playerScore < e.DifficultyCheck.nectarMin)
            {
                if (settings.IndexOf(currentDifficulty) - 1 >= 0)
                {
                    currentDifficulty = settings[settings.IndexOf(currentDifficulty) - 1];
                    EventQueue.eventQueue.AddEvent(new ChangeDifficultyEventData(currentDifficulty));
                }
            }
            else if (playerScore > e.DifficultyCheck.nectarMax)
            {
                if (settings.IndexOf(currentDifficulty) + 1 <= settings.Count-1)
                {
                    currentDifficulty = settings[settings.IndexOf(currentDifficulty) + 1];
                    EventQueue.eventQueue.AddEvent(new ChangeDifficultyEventData(currentDifficulty));
                }
            }
        }
    }


    private void OnChangeStateEvent()
    {
        int rand = Random.Range(0, zones.Count);

        EventQueue.eventQueue.AddEvent(new ChangeStateEventData(zones[rand]));
    }
}
