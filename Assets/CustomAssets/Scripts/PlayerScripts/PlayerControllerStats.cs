using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerControllerStats
{
    public float ForwardSpeedFly = 6f;
    public float ForwardSpeedWalk = 6f;
    public float Acceleration = 0.5f;
    public float UpSpeed = 3f;
    public float UpSpeedSpam = 6f;
    public float Gravity = 1f;
    public float DownSpeed = 1f;
    public float turnSmoothTime = 0.1f;
    public bool spamSpaceKey = false;
}
