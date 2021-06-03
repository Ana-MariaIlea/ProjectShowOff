using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerStates
{
    Movement,
    QTEEvent
}
public class PlayerStateMachine : MonoBehaviour
{

    PlayerMotor motor;
    QTESystem system;
    NectarCollect collect;
    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        system = GetComponent<QTESystem>();
        collect = GetComponent<NectarCollect>();
        system.enabled = false;
        EventQueue.eventQueue.Subscribe(EventType.CHANGEPLAYERSTATE, OnStateChange);
    }

    public void OnStateChange(EventData eventData)
    {
        if(eventData is ChangePlayerStateEventData)
        {
            //Debug.Log("Change player state");
            ChangePlayerStateEventData e = eventData as ChangePlayerStateEventData;
            if (e.state == PlayerStates.Movement)
            {
                motor.enabled = true;
                collect.enabled = true;
                system.enabled = false;
            }
            else if(e.state == PlayerStates.QTEEvent)
            {
                motor.enabled = false;
                collect.enabled = false;
                system.enabled = true;
            }
        }
    }

}
