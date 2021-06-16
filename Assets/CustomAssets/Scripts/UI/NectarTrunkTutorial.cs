using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NectarTrunkTutorial : MonoBehaviour
{
    [SerializeField]
    int nectarAmount = 0;
    [SerializeField]
    TextMeshProUGUI text;
    //[SerializeField]
   // GameObject gameTrunk;
    // Start is called before the first frame update
    void Start()
    {
        EventQueue.eventQueue.Subscribe(EventType.NECTARSTORED, OnNectarIsStored);
        GetComponent<NectarTrunk>().enabled = false;
        //gameTrunk.SetActive(false);
    }

    private void changeNectarAmount(int amount)
    {
        nectarAmount += amount;
        Debug.Log("New nectar amount in trunk: " + nectarAmount);
        text.text = nectarAmount.ToString();
    }

    public void OnNectarIsStored(EventData eventData)
    {
        if (eventData is NectarIsStoredEventData)
        {
            NectarIsStoredEventData e = eventData as NectarIsStoredEventData;
            changeNectarAmount(e.nectarAmount);
            GetComponent<NectarTrunk>().enabled = true;
            Tutorial.instance.EndTutorial();
            this.enabled = false;
           // Destroy(this);
        }
    }
}
