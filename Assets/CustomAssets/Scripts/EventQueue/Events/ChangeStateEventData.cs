using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStateEventData : EventData
{
    public HumanStates state;
    public ZoneSettings zoneSettings;
    public ChangeStateEventData(ZoneSettings newZoneSettings=null) : base(EventType.CHANGESTATE)
    {
        zoneSettings = newZoneSettings;
    }
}
