using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TestFlower : MonoBehaviour
{
    public Material TestMaterial;
    // Start is called before the first frame update
    void Start()
    {
        EventQueue.eventQueue.Subscribe(EventType.DESTROYFLOWER, OnFlowerDestroy);
    }

    public void OnFlowerDestroy(EventData eventData)
    {
        if(eventData is FlowerDestroyEventData)
        {
            FlowerDestroyEventData e = eventData as FlowerDestroyEventData;
            if (e.f == this.gameObject)
            {
                EventQueue.eventQueue.UnSubscribe(EventType.DESTROYFLOWER, OnFlowerDestroy);
                Destroy(this.gameObject);
            }
        }
        
    }

    
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Pesticide")
        {
            // EventQueue.eventQueue.AddEvent(new PlayerPesticideCollisionEventData());
            GetComponent<Renderer>().material = TestMaterial;
            List<NectarDistributor> distributors = GetComponentsInChildren<NectarDistributor>().ToList();
            for (int i = distributors.Count-1; i > 0; i--)
            {
                NectarDistributor d = distributors[i];

            }
        }
    }
}
