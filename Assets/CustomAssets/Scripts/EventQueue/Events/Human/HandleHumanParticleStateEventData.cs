using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleHumanParticleStateEventData : EventData
{
    public string NameOfObject;
    public bool Play;
    public HandleHumanParticleStateEventData(string nameOfObject, bool play) : base(EventType.HANDLEHUMANPARTICLE)
    {
        NameOfObject = nameOfObject;
        Play = play;
    }
}
