using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndEventData : EventData
{
    public string name;
    public int score;
    public GameEndEventData(string _name, int _score) : base(EventType.GAMEEND) 
    {
        name = _name;
        score = _score;
    }
}
