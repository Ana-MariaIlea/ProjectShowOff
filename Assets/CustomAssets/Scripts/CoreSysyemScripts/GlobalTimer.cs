using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum GardenZones
{
    WildPatch,
    FlowerGarden,
    VegetableGarden
}

public class GlobalTimer : MonoBehaviour
{
    [SerializeField]
    int minutes;
    [SerializeField]
    int seconds;

    [SerializeField]
    int minutesForFirstEvent;
    [SerializeField]
    int secondsForFirstEvent;

    [SerializeField]
    int minutesForSecondEvent;
    [SerializeField]
    int secondsForSecondEvent;

    [SerializeField]
    int minutesForThirdEvent;
    [SerializeField]
    int secondsForThirdEvent;

    [SerializeField]
    List<EventStates> eventStates;

    [SerializeField]
    private List<DifficultyCheck> checksForDifficulty;

    [Tooltip("Do NOT modify, preview avaliable for testing only")]
    [SerializeField]
    float timer = 0;

    [System.Serializable]
    public class EventStates
    {
        [SerializeField]
        public HumanStates eventType;

        [SerializeField]
        public bool overridePlayerPosition;


        [SerializeField]
        public HumanStates otherEventType1;
        [SerializeField]
        public int chanceForOtherEvent1;

        [SerializeField]
        public HumanStates otherEventType2;
        [SerializeField]
        public int chanceForOtherEvent2;

    }

    float timeForFirstEvent;
    float timeForSecondEvent;
    float timeForThirdEvent;

    bool firstEventFired = false;
    bool secondEventFired = false;
    bool thirdEventFired = false;

    float MaxTimer;
    int difficultyCheckIndex = 0;

    private void Awake()
    {
        MaxTimer = minutes * 60 + seconds;

        timeForFirstEvent = timer - (minutesForFirstEvent * 60 + secondsForFirstEvent);
        timeForSecondEvent = timer - (minutesForSecondEvent * 60 + secondsForSecondEvent);
        timeForThirdEvent = timer - (minutesForThirdEvent * 60 + secondsForThirdEvent);

        InitializeDifficultyChecks();
    }

    private void InitializeDifficultyChecks()
    {
        foreach (var item in checksForDifficulty)
        {
            item.Initiallize();
        }

        for (int i = 0; i < checksForDifficulty.Count; i++)
        {
            for (int j = i + 1; j < checksForDifficulty.Count; j++)
            {
                if (checksForDifficulty[j].time > checksForDifficulty[i].time)
                {
                    DifficultyCheck aux = checksForDifficulty[i];
                    checksForDifficulty[i] = checksForDifficulty[j];
                    checksForDifficulty[j] = aux;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;


        if (timer < timeForFirstEvent && firstEventFired == false)
        {
            Debug.Log("FirstEvent");
            ChangeStateEvent(eventStates[0]);
            firstEventFired = true;
        }
        if (timer < timeForSecondEvent && secondEventFired == false)
        {
            Debug.Log("SecondEvent");
            ChangeStateEvent(eventStates[1]);
            secondEventFired = true;
        }
        if (timer < timeForThirdEvent && thirdEventFired == false)
        {
            Debug.Log("ThirdtEvent");
            ChangeStateEvent(eventStates[2]);
            thirdEventFired = true;
        }

        if (timer < timer - checksForDifficulty[difficultyCheckIndex].time)
        {
            difficultyCheckIndex++;
            EventQueue.eventQueue.AddEvent(new CheckDifficultyEventData(checksForDifficulty[difficultyCheckIndex]));
        }
    }

    private static void ChangeStateEvent(EventStates customState)
    {
        HumanStates state = GameSessionStats.instance.GetPlayerPositionZone();
        if (customState.overridePlayerPosition)
        {
            int r = Random.Range(0, 100);

            if (r < customState.chanceForOtherEvent1 && customState.chanceForOtherEvent1 < 0)
            {
                state = customState.otherEventType1;
            }
            else if (r < customState.chanceForOtherEvent2 && customState.chanceForOtherEvent2 < 0)
            {
                state = customState.otherEventType2;
            }
            else
            {
                state = customState.eventType;
            }

        }
        Debug.Log(state);
        EventQueue.eventQueue.AddEvent(new ChangeStateEventData(state));
    }
}
