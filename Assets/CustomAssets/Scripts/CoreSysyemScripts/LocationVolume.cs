using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationVolume : MonoBehaviour
{
    public HumanStates zone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EventQueue.eventQueue.AddEvent(new ChangePlayerLocationEventData(zone));

        }
    }

    void OnDrawGizmos()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 1);
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
