using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAnimationEvents : MonoBehaviour
{
    public void PlayFootSteps()
    {
        Debug.Log("animation event play footsteps");
        EventQueue.eventQueue.AddEvent(new PlayFootstepsSoundEventData());
    }

    public void PickFlower()
    {

    }
}
