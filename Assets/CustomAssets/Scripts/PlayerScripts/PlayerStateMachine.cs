using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{

    PlayerMotor motor;
    QTESystem system;
    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        system = GetComponent<QTESystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
