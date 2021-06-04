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

    NectarDistributor lastKnownDistribuitor = null;

    private void Start()
    {
        EventQueue.eventQueue.Subscribe(EventType.NECTARCOLLECTEND, OnNectarIsCollected);
        EventQueue.eventQueue.Subscribe(EventType.COLECTNECTAR, OnCollectNectar);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("NectarKey"))
        {
            if (other.GetComponent<NectarDistributor>())
            {


                EventQueue.eventQueue.AddEvent(new ChangePlayerStateEventData(PlayerStates.QTEEvent));
                lastKnownDistribuitor = other.GetComponent<NectarDistributor>();

            }

            if (other.GetComponent<NectarTrunk>() && nectarAmount > 0)
            {
                EventQueue.eventQueue.AddEvent(new NectarIsStoredEventData(nectarAmount));
                resetNectarAmount();
            }
        }
    }

    public void OnCollectNectar(EventData eventData)
    {
        if (eventData is CollectNectarEventData)
        {
            if (lastKnownDistribuitor != null)
            {
                EventQueue.eventQueue.AddEvent(new NectarCollectStartEventData(lastKnownDistribuitor));
                lastKnownDistribuitor = null;
            }
        }
    }

    public void OnNectarIsCollected(EventData eventData)
    {
        if (eventData is NectarCollectEndEventData)
        {
            NectarCollectEndEventData e = eventData as NectarCollectEndEventData;
            if (nectarAmount + e.nectarAmount < maxNectarAmount)
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
        text.text = nectarAmount.ToString();
    }

    private void resetNectarAmount()
    {
        nectarAmount = 0;
        text.text =nectarAmount.ToString();
    }


}
