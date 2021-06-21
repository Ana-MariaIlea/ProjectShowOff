using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NectarDistribuitorTutorial : MonoBehaviour
{
    private int nectarAmount;
    private bool test = true;
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
        //EventQueue.eventQueue.Subscribe(EventType.NECTARCOLLECTTUTORIAL, OnNectartCollectTutorialDone);
        GetComponent<NectarDistributor>().enabled = false;

        //gameDistribuitor.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && this.enabled == true)
        {
            Tutorial.instance.SetIndex(3);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (test == true)
        {
            if (Input.GetButtonDown("NectarKey"))
            {
                Debug.Log("tutorial nectar key pressed");
                if (other.tag == "Player")
                {
                    EventQueue.eventQueue.AddEvent(new ChangePlayerStateEventData(PlayerStates.QTEEvent));
                    GetComponent<NectarDistributor>().SetIsDistribuitorSelectes(true);

                }
            }
        }
    }

    public void OnNectarIsCollected(EventData eventData)
    {
        if (test && this != null)
        {
            Debug.Log("Unsubscribe tutorial");
            EventQueue.eventQueue.UnSubscribe(EventType.NECTARCOLLECTSTART, OnNectarIsCollected);
            if (eventData is NectarCollectStartEventData)
            {
                if (GetComponent<NectarDistributor>().GetIsDistribuitorSelectes())
                {
                    EventQueue.eventQueue.AddEvent(new NectarCollectEndEventData(nectarAmount));
                    Tutorial.instance.IncreasePanelIndex();
                    GetComponent<NectarDistributor>().SetIsDistribuitorSelectes(false);
                }
            }
            GetComponent<NectarDistributor>().enabled = true;
            this.enabled = false;
            test = false;
        }
    }
}
