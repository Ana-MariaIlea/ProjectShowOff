using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerControllerStats
{
    public float ForwardSpeed = 6f;
    public float UpSpeed = 3f;
    public float UpSpeedSpam = 6f;
    public float DownSpeed = 1f;
    public float turnSmoothTime = 0.1f;
    public bool spamSpaceKey = false;
}
