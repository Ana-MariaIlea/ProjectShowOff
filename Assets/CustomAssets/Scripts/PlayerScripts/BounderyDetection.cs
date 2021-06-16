using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounderyDetection : MonoBehaviour
{
    [SerializeField]
    Transform centerOfMap;
    [SerializeField]
    float speed;
    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceToLocation = centerOfMap.position - transform.position;
        characterController.Move(distanceToLocation.normalized * speed * Time.deltaTime);
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.tag == "BounderyInner")
        {
            EventQueue.eventQueue.AddEvent(new ChangePlayerStateEventData(PlayerStates.Movement));
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "BounderyOuter")
        {
            EventQueue.eventQueue.AddEvent(new ChangePlayerStateEventData(PlayerStates.Bounderies));
        }
    }


}
