using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleCollision : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Pesticide"|| other.tag == "Smoke")
        {
           // Debug.Log("particle collision");
            EventQueue.eventQueue.AddEvent(new PlayerPesticideCollisionEventData());
        }
    }

    
}
