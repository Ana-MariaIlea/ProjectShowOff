using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NectarCollect : MonoBehaviour
{
    [SerializeField]
    int nectarAmount = 0;
    [SerializeField]
    int maxNectarAmount;
    [SerializeField]
    TextMeshProUGUI text;

    private void Start()
    {
        EventQueue.eventQueue.Subscribe(EventType.NECTARCOLLECTEND, OnNectarIsCollected);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (other.GetComponent<NectarDistributor>())
            {
                
                EventQueue.eventQueue.AddEvent(new NectarCollectStartEventData(other.GetComponent<NectarDistributor>()));
            }

            if (other.GetComponent<NectarTrunk>() && nectarAmount > 0)
            {
                EventQueue.eventQueue.AddEvent(new NectarIsStoredEventData(nectarAmount));
                resetNectarAmount();
            }
        }
    }

    public void OnNectarIsCollected(EventData eventData)
    {
        if (eventData is NectarCollectEndEventData)
        {
            NectarCollectEndEventData e = eventData as NectarCollectEndEventData;
            if (nectarAmount+ e.nectarAmount < maxNectarAmount)
            { 
                changeNectarAmount(e.nectarAmount);
            }
            else
            {
                changeNectarAmount(maxNectarAmount);
            }
        }
    }

    private void changeNectarAmount(int amount)
    {
        nectarAmount += amount;
        Debug.Log("New nectar amount in player: " + nectarAmount);
        text.text = "Nectar on bee: " + nectarAmount.ToString();
    }

    private void resetNectarAmount()
    {
        nectarAmount = 0;
        text.text = "Nectar on bee: "+nectarAmount.ToString();
    }
}
