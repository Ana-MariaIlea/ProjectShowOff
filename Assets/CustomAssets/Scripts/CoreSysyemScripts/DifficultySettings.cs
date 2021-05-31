﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]

[CreateAssetMenu(fileName = "DifficultySettings", menuName = "ScriptableObjects/DifficultySettings", order = 1)]
public class DifficultySettings:ScriptableObject 
{
    public string DifficultyLabel;
    [Tooltip("The difficulties will be sorted by this value")]
    public int DifficultyLevel;
    public PlayerControllerStats PlayerControllerStats;
    public PlayerEfectsStats PlayerEfectsStats;
    public bool FlowersGlow;
    public bool FlowersHaveParticles;
    public bool MinigameActive;
    public int NectarCooldownTime;
}

[System.Serializable]
public class DifficultyCheck
{
    public int minutes;
    public int seconds;
    public int nectarMin;
    public int nectarMax;
    [HideInInspector]
    public float time;

    public void Initiallize()
    {
        time = minutes * 60 + seconds;
    }
}
