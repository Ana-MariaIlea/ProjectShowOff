using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NectarDistribuitorTutorial : MonoBehaviour
{
    private int nectarAmount;
   // [SerializeField]
    //GameObject gameDistribuitor;
    void OnDrawGizmos()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
    private void Start()
    {
        nectarAmount = GetComponent<NectarDistributor>().GetNectarAmount();
        EventQueue.eventQueue.Subscribe(EventType.NECTARCOLLECTSTART, OnNectarIsCollected);
        EventQueue.eventQueue.Subscribe(EventType.NECTARCOLLECTTUTORIAL, OnNectartCollectTutorialDone);
        GetComponent<NectarDistributor>().enabled = false;

        //gameDistribuitor.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Tutorial.instance.SetIndex(3);
        }
    }

    public void OnNectarIsCollected(EventData eventData)
    {
        if (eventData is NectarCollectStartEventData)
        {
            NectarCollectStartEventData e = eventData as NectarCollectStartEventData;
            if (e.dis == GetComponent<NectarDistributor>())
            {
                Debug.Log("Tutorial nectar is ending");
                EventQueue.eventQueue.AddEvent(new NectarCollectEndEventData(nectarAmount));
                Tutorial.instance.IncreasePanelIndex();
                EventQueue.eventQueue.AddEvent(new NectarCollectTutorialEventData());
            }
        }
    }

    public void OnNectartCollectTutorialDone(EventData eventData)
    {
        if (eventData is NectarCollectTutorialEventData)
        {
            Debug.Log("Tutorial nectar end");
            //gameDistribuitor.SetActive(true);
            GetComponent<NectarDistributor>().enabled = true;
            Debug.Log(this.enabled);
            this.enabled = false;
            Debug.Log(this.enabled);
            //Destroy(this);
        }
    }
}
