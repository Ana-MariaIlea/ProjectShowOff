using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDifficultyEventData : EventData
{
    public DifficultyCheck DifficultyCheck;
    public CheckDifficultyEventData(DifficultyCheck difficultyCheck) : base(EventType.CHECKDIFFICULTY) {
        DifficultyCheck = difficultyCheck;
    }
}
