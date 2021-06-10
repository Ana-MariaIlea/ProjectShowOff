using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NectarDistribuitorTutorial : MonoBehaviour
{
    private int nectarAmount;
    [SerializeField]
    GameObject gameDistribuitor;
    void OnDrawGizmos()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
    private void Start()
    {
        nectarAmount = gameDistribuitor.GetComponent<NectarDistributor>().GetNectarAmount();
        EventQueue.eventQueue.Subscribe(EventType.NECTARCOLLECTSTART, OnNectarIsCollected);
        EventQueue.eventQueue.Subscribe(EventType.NECTARCOLLECTTUTORIAL, OnNectartCollectTutorialDone);
        gameDistribuitor.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Tutorial.instance.SetIndex(4);
        }
    }

    public void OnNectarIsCollected(EventData eventData)
    {
        if (eventData is NectarCollectStartEventData)
        {
            NectarCollectStartEventData e = eventData as NectarCollectStartEventData;
            if (e.dis == this)
            {
                EventQueue.eventQueue.AddEvent(new NectarCollectEndEventData(nectarAmount));
                EventQueue.eventQueue.AddEvent(new NectarCollectTutorialEventData());
            }
        }
    }

    public void OnNectartCollectTutorialDone(EventData eventData)
    {
        if (eventData is NectarCollectTutorialEventData)
        {
            gameDistribuitor.SetActive(true);
            Tutorial.instance.IncreasePanelIndex();
            Destroy(this);
        }
    }
}
