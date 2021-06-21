using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerLocationEventData : EventData
{
    public HumanStates zone;
    public ChangePlayerLocationEventData(HumanStates newZone) : base(EventType.CHANGEZONE) { zone = newZone; }
}
