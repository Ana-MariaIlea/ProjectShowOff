using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public enum HumanStates
{
    WalkInTheGarden,
    PickFlowes,
    SprayGarden,
    CutGrass,
    GoToHouse,
    NoState
}

public class HumanStateMachine : MonoBehaviour
{

    [SerializeField]
    ParticleSystem pesticides;
    [SerializeField]
    ParticleSystem smoke;
    [SerializeField]
    GameObject lawnmower;
    [SerializeField]
    GameObject houseLocation;

    


    [SerializeField]
    float timeToStartTheFirstEvent;
    bool firstEventFire = false;


    [Tooltip("Time to stay in the house")]
    [SerializeField]
    float time;
    BaseState currentState;
    // Start is called before the first frame update
    void Start()
    {
        EventQueue.eventQueue.Subscribe(EventType.CHANGESTATE, OnStateChange);
        EventQueue.eventQueue.Subscribe(EventType.ENDSTATE, OnGoToHouse);
        EventQueue.eventQueue.Subscribe(EventType.CHANGEDIFFICULTY, OnDifficultyChange);
        EventQueue.eventQueue.Subscribe(EventType.GAMEEND, OnGameEnd);



        //GetComponent<FMODUnity.StudioEventEmitter>().PlayInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
            currentState.UpdateBehavior();

        StartFirstEvent();
    }

    private void StartFirstEvent()
    {
        if (timeToStartTheFirstEvent < 0 && firstEventFire == false)
        {
            EventQueue.eventQueue.AddEvent(new ChangeStateStartEventData());
            firstEventFire = true;
        }
        else
        {
            timeToStartTheFirstEvent -= Time.deltaTime;
        }
    }

    public void OnStateChange(EventData eventData)
    {
        if (eventData is ChangeStateEventData)
        {
            ChangeStateEventData e = eventData as ChangeStateEventData;
            switch (e.zoneSettings.stateOfZone)
            {
                case HumanStates.PickFlowes:
                   // Debug.Log("Change state to pick Flowers");
                    currentState = new PickFlowers(GetComponent<NavMeshAgent>(),e.zoneSettings.currentFlowers);
                    e.zoneSettings.InitializeEventStats();
                    break;
                case HumanStates.SprayGarden:
                   // Debug.Log("Change state to spray garden");
                    currentState = new SprayPesticides(GetComponent<NavMeshAgent>(),pesticides, e.zoneSettings.currentWaypoints, GetComponent<FMODUnity.StudioEventEmitter>());
                    e.zoneSettings.InitializeEventStats();
                    break;
                case HumanStates.CutGrass:
                  //  Debug.Log("Change state to cut grass "+ e.zoneSettings.gameObject.name);
                   //Debug.Log(e.zoneSettings.gameObject.name);
                    currentState = new CutGrass(GetComponent<NavMeshAgent>(),smoke,e.zoneSettings.launge,e.zoneSettings.plants, e.zoneSettings.currentWaypoints);
                    e.zoneSettings.InitializeEventStats();
                    break;
                default:
                    currentState = null;
                    break;
            }

            GetComponent<HumanAnimatorState>().ChangeAnimatorState(HumanAnimationStates.WALK);

        }
    }

    public void OnGoToHouse(EventData eventData)
    {
        if (eventData is EndStateEventData)
        {
            currentState = new GoToHouse(GetComponent<NavMeshAgent>(),houseLocation,time);
        }
    }

    public void OnDifficultyChange(EventData eventData)
    {
        if (eventData is ChangeDifficultyEventData)
        {
            ChangeDifficultyEventData e = eventData as ChangeDifficultyEventData;
            GetComponent<NavMeshAgent>().speed = e.Difficulty.HumanSpeed;
        }
    }

    public void OnGameEnd(EventData eventData)
    {
        if (eventData is GameEndEventData)
        {
            EventQueue.eventQueue.UnSubscribe(EventType.CHANGESTATE, OnStateChange);
            EventQueue.eventQueue.UnSubscribe(EventType.ENDSTATE, OnGoToHouse);
            EventQueue.eventQueue.UnSubscribe(EventType.CHANGEDIFFICULTY, OnDifficultyChange);
            EventQueue.eventQueue.UnSubscribe(EventType.GAMEEND, OnGameEnd);
        }
    }
}
