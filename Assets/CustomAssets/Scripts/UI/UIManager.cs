using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI nectarOnBeeText;
    [SerializeField]
    TextMeshProUGUI nectarOnTrunkText;

    private void Start()
    {
        EventQueue.eventQueue.Subscribe(EventType.NECTARONBEETEXTCHANGE, OnNectarOnBeeTextChange);
        EventQueue.eventQueue.Subscribe(EventType.NECTARONTRUNKTEXTCHANGE, OnNectarOnTrunkTextChange);
    }
    public void OnNectarOnBeeTextChange(EventData eventData)
    {
        if (eventData is NectarOnBeeTextChangeEventData)
        {
            NectarOnBeeTextChangeEventData e = eventData as NectarOnBeeTextChangeEventData;
            nectarOnBeeText.text = e.number.ToString();
        }
    }

    public void OnNectarOnTrunkTextChange(EventData eventData)
    {
        if (eventData is NectarOnTrunkTextChangeEventData)
        {
            NectarOnTrunkTextChangeEventData e = eventData as NectarOnTrunkTextChangeEventData;
            nectarOnTrunkText.text = e.number.ToString();
        }
    }
}
