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
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && this.enabled == true)
        {
            Tutorial.instance.SetIndex(6);
        }
    }
    public void OnNectarIsStored(EventData eventData)
    {
        if (eventData is NectarIsStoredEventData && this.enabled == true)
        {
            Debug.Log("nectar stored in tutorial");
            NectarIsStoredEventData e = eventData as NectarIsStoredEventData;
            //changeNectarAmount(e.nectarAmount);
            GetComponent<NectarTrunk>().enabled = true;
            GetComponent<NectarTrunk>().setNectarAmount(e.nectarAmount);
            Tutorial.instance.IncreasePanelIndex();
            this.enabled = false;
           // Destroy(this);
        }
    }
}
