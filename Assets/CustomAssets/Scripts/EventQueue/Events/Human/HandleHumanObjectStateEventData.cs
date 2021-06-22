using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleHumanObjectStateEventData : EventData
{
    public string NameOfObject;
    public bool State;
    public HandleHumanObjectStateEventData(string nameOfObject, bool state) : base(EventType.HANDLEHUMANOBJECT) 
    {
        NameOfObject = nameOfObject;
        State = state;
    }
}
