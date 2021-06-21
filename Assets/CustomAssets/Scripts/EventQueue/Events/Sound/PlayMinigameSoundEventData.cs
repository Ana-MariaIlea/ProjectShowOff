using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MinigameSounds
{
    Start,
    Pass,
    Win,
    Lose
}
public class PlayMinigameSoundEventData : EventData
{
    public MinigameSounds type;
    public PlayMinigameSoundEventData(MinigameSounds type) : base(EventType.PLAYMINIGAMESOUND) { this.type = type; }
}
