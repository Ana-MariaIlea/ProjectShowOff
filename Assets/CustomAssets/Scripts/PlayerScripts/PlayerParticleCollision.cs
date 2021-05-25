using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleCollision : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Pesticide")
        {
            EventQueue.eventQueue.AddEvent(new PlayerPesticideCollisionEventData());
        }
    }

    
}
