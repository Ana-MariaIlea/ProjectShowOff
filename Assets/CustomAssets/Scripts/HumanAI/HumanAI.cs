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
                    currentState = stateHolder.GetComponent<PickFlowers>();
                    break;
                case HumanStates.SprayGarden:
                    currentState = stateHolder.GetComponent<SprayPesticides>();
                    break;
                case HumanStates.CutGrass:
                    currentState = stateHolder.GetComponent<CutGrass>();
                    break;
            }

        }
    }
}
