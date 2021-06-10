using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum Condition
{
    Condition,
    Time
}

[System.Serializable]
public class TutorialPanel 
{
    [Tooltip("The tutorial panel will be depended on timer or a condition")]
    public Condition condition;
    [Tooltip("Gameobject that represents the panel")]
    public GameObject panel;
    [Tooltip("Time after which the panel will dissapear if condition is time")]
    public float timeToShowPanel;
    [Tooltip("After the panel dissapears, time to show next panel")]
    public float timeToShowNextPanel;
}
