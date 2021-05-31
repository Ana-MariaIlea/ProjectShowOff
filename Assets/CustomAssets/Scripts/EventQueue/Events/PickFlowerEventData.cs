using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickFlowerEventData : EventData
{
    public NectarDistributor distributor;
    public PickFlowerEventData(NectarDistributor flower) : base(EventType.PICKFLOWER)
    {
        distributor = flower;
    }
}
