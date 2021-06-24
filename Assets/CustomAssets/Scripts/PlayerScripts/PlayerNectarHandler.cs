using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNectarHandler : MonoBehaviour
{
    [SerializeField]
    int nectarAmount = 0;
    [SerializeField]
    int maxNectarAmount;

    private void Start()
    {
        EventQueue.eventQueue.Subscribe(EventType.NECTARCOLLECTEND, OnNectarIsCollected);
        EventQueue.eventQueue.Subscribe(EventType.GAMEEND, OnGameEnd);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("NectarKey"))
        {
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
        EventQueue.eventQueue.AddEvent(new NectarOnBeeTextChangeEventData(nectarAmount));
    }

    private void resetNectarAmount()
    {
        nectarAmount = 0;
        EventQueue.eventQueue.AddEvent(new NectarOnBeeTextChangeEventData(nectarAmount));
    }

    public int GetNectarAmount()
    {
        return nectarAmount;
    }

    public void OnGameEnd(EventData eventData)
    {
        if (eventData is GameEndEventData)
        {
            EventQueue.eventQueue.UnSubscribe(EventType.NECTARCOLLECTEND, OnNectarIsCollected);

            EventQueue.eventQueue.UnSubscribe(EventType.GAMEEND, OnGameEnd);
        }
    }
}
