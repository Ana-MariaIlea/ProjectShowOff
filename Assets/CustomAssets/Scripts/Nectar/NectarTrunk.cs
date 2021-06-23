using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NectarTrunk : MonoBehaviour
{
    [SerializeField]
    int nectarAmount = 0;
    //[SerializeField]
    //TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        EventQueue.eventQueue.Subscribe(EventType.NECTARSTORED, OnNectarIsStored);
    }

    private void changeNectarAmount(int amount)
    {
        nectarAmount += amount;
        Debug.Log("New nectar amount in trunk: " + nectarAmount);
        GameSessionStats.instance.SetPlayerScore(nectarAmount);
        EventQueue.eventQueue.AddEvent(new PlayScoreIncreaseSoundEventData());
        EventQueue.eventQueue.AddEvent(new NectarOnTrunkTextChangeEventData(nectarAmount));
        //text.text = nectarAmount.ToString();
    }

    public void OnNectarIsStored(EventData eventData)
    {
        if(eventData is NectarIsStoredEventData)
        {
            NectarIsStoredEventData e = eventData as NectarIsStoredEventData;
            Debug.Log("nectar stored in trunk");
            changeNectarAmount(e.nectarAmount);
        }
    }

    public void setNectarAmount(int amount)
    {
        nectarAmount = amount;
        EventQueue.eventQueue.AddEvent(new PlayScoreIncreaseSoundEventData());
        EventQueue.eventQueue.AddEvent(new NectarOnTrunkTextChangeEventData(nectarAmount));
        //text.text = nectarAmount.ToString();
    }
}
