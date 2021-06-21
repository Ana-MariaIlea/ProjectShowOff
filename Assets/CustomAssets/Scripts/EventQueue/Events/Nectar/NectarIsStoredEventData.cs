using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NectarIsStoredEventData : EventData
{
    public int nectarAmount;
    public NectarIsStoredEventData(int amount) : base(EventType.NECTARSTORED)
    {
        nectarAmount = amount;
    }
}

