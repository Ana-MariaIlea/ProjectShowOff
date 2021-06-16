using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerStates
{
    Movement,
    QTEEvent,
    Bounderies
}
public class PlayerStateMachine : MonoBehaviour
{

    PlayerMotor motor;
    QTESystem system;
    NectarCollect collect;
    BounderyDetection boundery;
    PlayerStates currentState;
    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        system = GetComponent<QTESystem>();
        collect = GetComponent<NectarCollect>();
        boundery = GetComponent<BounderyDetection>();
        system.enabled = false;
        currentState = PlayerStates.Movement;
        EventQueue.eventQueue.Subscribe(EventType.CHANGEPLAYERSTATE, OnStateChange);
    }

    public void OnStateChange(EventData eventData)
    {
        if(eventData is ChangePlayerStateEventData)
        {
            //Debug.Log("Change player state");
            ChangePlayerStateEventData e = eventData as ChangePlayerStateEventData;
            if (e.state == PlayerStates.Movement&& currentState != PlayerStates.Movement)
            {
                if(motor!=null)
                motor.enabled = true;
                if (collect != null)
                    collect.enabled = true;
                if (boundery != null)
                    boundery.enabled = false;
                if (system != null)
                    system.enabled = false;
                currentState = PlayerStates.Movement;
            }
            else if(e.state == PlayerStates.QTEEvent && currentState != PlayerStates.QTEEvent)
            {
                if (motor != null)
                    motor.enabled = false;
                if (collect != null)
                    collect.enabled = false;
                if (boundery != null)
                    boundery.enabled = false;
                if (system != null)
                    system.enabled = true;
                currentState = PlayerStates.QTEEvent;

            }
            else if (e.state == PlayerStates.Bounderies && currentState != PlayerStates.Bounderies)
            {
                if (motor != null)
                    motor.enabled = false;
                if (collect != null)
                    collect.enabled = false;
                if (system != null)
                    system.enabled = false;
                if (boundery != null)
                    boundery.enabled = true;
                currentState = PlayerStates.Bounderies;
            }
        }
    }

}
