using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NectarOnBeeTextChangeEventData : EventData
{
    public int number;
    public NectarOnBeeTextChangeEventData(int n) : base(EventType.NECTARONBEETEXTCHANGE) 
    {
        number = n;
    }
}
