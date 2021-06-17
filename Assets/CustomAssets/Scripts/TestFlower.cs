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

    

}
