using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NectarCollectEndEventData : EventData
{
    public int nectarAmount;
    
    public NectarCollectEndEventData(int amount) : base(EventType.NECTARCOLLECTEND)
    {
        nectarAmount = amount;
        
    }
}

