using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FlowerParticleCollision : MonoBehaviour
{

    public Material TestMaterial;
    public GameObject nectarsHolder;
    private void OnParticleCollision(GameObject other)
    {
       // Debug.Log("Particle colision");
        if (other.tag == "Pesticide")
        {
            //Debug.Log("Particle colision with pesticide");
            // EventQueue.eventQueue.AddEvent(new PlayerPesticideCollisionEventData());
            GetComponent<Renderer>().material = TestMaterial;
            List<NectarDistributor> distributors = nectarsHolder.GetComponentsInChildren<NectarDistributor>().ToList();
            for (int i = 0; i < distributors.Count; i++)
            {
                //NectarDistributor d = distributors[i];
                //distributors.Remove(distributors[i]);
                //Debug.Log("Destroy distrib");
                distributors[i].DestroyDistribuitor();
            }
        }
    }

}
