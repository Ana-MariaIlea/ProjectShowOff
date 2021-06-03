﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NectarTrunk : MonoBehaviour
{
    [SerializeField]
    int nectarAmount = 0;
    [SerializeField]
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        EventQueue.eventQueue.Subscribe(EventType.NECTARSTORED, OnNectarIsStored);
    }

    private void changeNectarAmount(int amount)
    {
        nectarAmount += amount;
        Debug.Log("New nectar amount in trunk: " + nectarAmount);
        //text.text = "Nectar in deposit: " + nectarAmount.ToString();
    }

    public void OnNectarIsStored(EventData eventData)
    {
        if(eventData is NectarIsStoredEventData)
        {
            NectarIsStoredEventData e = eventData as NectarIsStoredEventData;
            changeNectarAmount(e.nectarAmount);
        }
    }
}
