using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerStateEventData : EventData
{

    public PlayerStates state;
    public ChangePlayerStateEventData(PlayerStates newState) : base(EventType.CHANGEPLAYERSTATE)
    {
        state = newState;
    }
}
