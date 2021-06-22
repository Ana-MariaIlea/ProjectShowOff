using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanObjectHandler : MonoBehaviour
{
    [SerializeField]
    List<HumanObject> objects;

    [SerializeField]
    List<HumanParticle> particles;

    [System.Serializable]
    public class HumanObject
    {
        public string Name;
        public GameObject gameObject;
    }

    [System.Serializable]
    public class HumanParticle
    {
        public string Name;
        public ParticleSystem gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        EventQueue.eventQueue.Subscribe(EventType.HANDLEHUMANOBJECT, OnHandleStateOfObject);
    }

    public void OnHandleStateOfObject(EventData eventData)
    {
        if (eventData is HandleHumanObjectStateEventData)
        {
            HandleHumanObjectStateEventData e = eventData as HandleHumanObjectStateEventData;
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].Name == e.NameOfObject)
                {
                    if (e.State == true)
                    {
                        if (objects[i].gameObject.activeSelf==false)
                        {
                            objects[i].gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        if (objects[i].gameObject.activeSelf == true)
                        {
                            objects[i].gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }

    public void HandleStateOfParticle(EventData eventData)
    {
        if (eventData is HandleHumanParticleStateEventData)
        {
            HandleHumanParticleStateEventData e = eventData as HandleHumanParticleStateEventData;
            for (int i = 0; i < particles.Count; i++)
            {
                if (particles[i].Name == e.NameOfObject)
                {
                    if (e.Play == true)
                    {
                        //if()
                    }
                }
            }
        }
    }

}
