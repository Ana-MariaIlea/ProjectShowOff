using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounderyDetection : MonoBehaviour
{
    [SerializeField]
    Transform centerOfMap;
    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BounderyInner")
        {

        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BounderyOuter")
        {

        }
    }
}
