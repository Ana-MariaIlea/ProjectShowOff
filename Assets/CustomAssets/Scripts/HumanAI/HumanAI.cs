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
    GoToHouse
}

public class HumanAI : MonoBehaviour
{
    [SerializeField]
    GameObject stateHolder;
    BaseState currentState;
    // Start is called before the first frame update
    void Start()
    {
        EventQueue.eventQueue.Subscribe(EventType.CHANGESTATE, OnStateChange);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState!=null)
        currentState.UpdateBehavior();
    }

    public void OnStateChange(EventData eventData)
    {
        if(eventData is ChangeStateEventData)
        {
            ChangeStateEventData e = eventData as ChangeStateEventData;
            switch (e.state)
            {
                case HumanStates.PickFlowes:
                    Debug.Log("Change state to pick Flowers");
                    currentState = stateHolder.GetComponent<PickFlowers>();
                    break;
                case HumanStates.SprayGarden:
                    Debug.Log("Change state to spray garden");
                    currentState = stateHolder.GetComponent<SprayPesticides>();
                    break;
                case HumanStates.CutGrass:
                    Debug.Log("Change state to cut grass");
                    currentState = stateHolder.GetComponent<CutGrass>();
                    break;
                case HumanStates.GoToHouse:
                    Debug.Log("Change state to go to house");
                    stateHolder.GetComponent<GoToHouse>().ResetTarget();
                    currentState = stateHolder.GetComponent<GoToHouse>();
                    break;
            }

        }
    }
}
