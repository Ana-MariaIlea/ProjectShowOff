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

[System.Serializable]
public class Timer
{
    public int minutes;
    public int seconds;
}

public class GlobalTimer : MonoBehaviour
{
    Timer time;

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

    float MaxTimer;
    int difficultyCheckIndex = 0;

    private void Awake()
    {
        MaxTimer = time.minutes * 60 + time.seconds;
        //timeForFirstEvent = timer - (minutesForFirstEvent * 60 + secondsForFirstEvent);
       // timeForSecondEvent = timer - (minutesForSecondEvent * 60 + secondsForSecondEvent);
       // timeForThirdEvent = timer - (minutesForThirdEvent * 60 + secondsForThirdEvent);

       // InitializeDifficultyChecks();
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


    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;

        if (timer < timer - checksForDifficulty[difficultyCheckIndex].time)
        {
            difficultyCheckIndex++;
            EventQueue.eventQueue.AddEvent(new CheckDifficultyEventData(checksForDifficulty[difficultyCheckIndex]));
        }
    }

   
}
