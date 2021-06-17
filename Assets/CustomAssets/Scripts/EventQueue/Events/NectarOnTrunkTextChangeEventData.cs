using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NectarOnTrunkTextChangeEventData : EventData
{
    public int number;
    public NectarOnTrunkTextChangeEventData(int n) : base(EventType.NECTARONTRUNKTEXTCHANGE)
    {
        number = n;
    }
}
