using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDifficultyEventData : EventData
{
    public DifficultySettings Difficulty;
    public ChangeDifficultyEventData(DifficultySettings difficulty) : base(EventType.CHANGEDIFFICULTY)
    {
        Difficulty = difficulty;
    }
}
