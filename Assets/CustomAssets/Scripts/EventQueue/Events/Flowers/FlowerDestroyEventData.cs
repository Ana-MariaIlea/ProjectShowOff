using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerDestroyEventData : EventData
{
    public GameObject f;
    public FlowerDestroyEventData(GameObject fl) : base(EventType.DESTROYFLOWER) { f = fl; }
}
