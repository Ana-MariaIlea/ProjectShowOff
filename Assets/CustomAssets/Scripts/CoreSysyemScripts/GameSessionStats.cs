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

    [SerializeField]
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
        if (GameManager.instance != null)
            playerName = GameManager.instance.playerName;
        if (settings.Count != 0)
        {
            for (int i = 0; i < settings.Count; i++)
            {
                for (int j = i + 1; j < settings.Count; j++)
                {
                    if (settings[j].DifficultyLevel < settings[i].DifficultyLevel)
                    {
                        DifficultySettings aux = settings[i];
                        settings[i] = settings[j];
                        settings[j] = aux;
                    }
                }
            }
        }
        EventQueue.eventQueue.Subscribe(EventType.CHANGEZONE, OnPlayerZoneChanged);
        EventQueue.eventQueue.Subscribe(EventType.CHECKDIFFICULTY, OnCheckDifficulty);
        EventQueue.eventQueue.Subscribe(EventType.CHANGESTATESTART, OnChangeStateEvent);

        // Debug.Log("Player name in the game stats " + playerName);
    }


    public void OnPlayerZoneChanged(EventData eventData)
    {
        if (eventData is ChangePlayerLocationEventData)
        {
            ChangePlayerLocationEventData e = eventData as ChangePlayerLocationEventData;
            SetPlayerPositionZone(e.zone);
        }
        else
        {
            throw new System.ArgumentOutOfRangeException("eventData", "EventData is not ChangePlayerLocationEventData");
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
    public int GetZoneListCount()
    {
        return zones.Count;
    }


    public int GetDifficultyListCount()
    {
        return settings.Count;
    }

    public void ResetDifficultyList()
    {
        settings = new List<DifficultySettings>();
    }

    public void OnCheckDifficulty(EventData eventData)
    {
        if (eventData is CheckDifficultyEventData)
        {
            Debug.Log("Difficulty check in game states");
            CheckDifficultyEventData e = eventData as CheckDifficultyEventData;
            if (settings.Count > 0)
            {
                if (playerScore < e.DifficultyCheck.nectarMin)
                {
                    Debug.Log("Nectar too small");
                    if (settings.IndexOf(currentDifficulty) - 1 >= 0)
                    {
                        Debug.Log("Change diff below");
                        currentDifficulty = settings[settings.IndexOf(currentDifficulty) - 1];
                        EventQueue.eventQueue.AddEvent(new ChangeDifficultyEventData(currentDifficulty));
                    }
                    else
                    {
                        Debug.Log("No Difficulty below");
                    }
                }
                else if (playerScore > e.DifficultyCheck.nectarMax)
                {
                    Debug.Log("Nectar too big");
                    if (settings.IndexOf(currentDifficulty) + 1 <= settings.Count - 1)
                    {
                        Debug.Log("Change diff above");
                        currentDifficulty = settings[settings.IndexOf(currentDifficulty) + 1];
                        EventQueue.eventQueue.AddEvent(new ChangeDifficultyEventData(currentDifficulty));
                    }
                    else
                    {
                        Debug.Log("No Difficulty above");
                    }
                }
            }
            else
            {
                throw new System.ArgumentOutOfRangeException("settings.Count", "No settings in the list");
            }
        }
        else
        {
            throw new System.ArgumentOutOfRangeException("eventData", "EventData is not CheckDifficultyEventData");
        }
    }


    private void OnChangeStateEvent(EventData eventData)
    {
        if (eventData is ChangeStateStartEventData)
        {
            if (zones.Count > 0)
            {
                int rand = Random.Range(0, zones.Count - 1);
                EventQueue.eventQueue.AddEvent(new ChangeStateEventData(zones[rand]));
                if (zones[rand].numberOfTimes == 0)
                {
                    Debug.Log("remove zone " + zones[rand].gameObject.name);
                    zones.RemoveAt(rand);
                }
                else
                {
                    Debug.Log("not remove zone " + zones[rand].gameObject.name + " number of times" + zones[rand].numberOfTimes);
                }
            }
            else
            {
                throw new System.ArgumentOutOfRangeException("zones.Count", "No Zones in the list");
            }
        }
        else
        {
            throw new System.ArgumentOutOfRangeException("eventData", "EventData is not ChangeStateStartEventData");
        }
    }
}
