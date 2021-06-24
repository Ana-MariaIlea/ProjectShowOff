using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerScoreEventData : EventData
{
    public int nectarAmount;
    public ChangePlayerScoreEventData(int amount) : base(EventType.SCORESET)
    {
        nectarAmount = amount;
    }
}
