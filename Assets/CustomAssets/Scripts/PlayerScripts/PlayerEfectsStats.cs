using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerEfectsStats
{
    [Tooltip("Slowd forward speed when coliding with particles")]
    public float ForwardSpeedSlowed = 6f;
    [Tooltip("Slowd up speed when coliding with particles, while pressing space")]
    public float UpSpeedSlowed = 3f;
    [Tooltip("Slowd up speed when coliding with particles, while spamming space")]
    public float UpSpeedSpamSlowed = 6f;
    [Tooltip("How much time the efect lasts")]
    public int slowedTimer;
}
