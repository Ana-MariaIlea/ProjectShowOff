using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MoveInGarden : BaseState
{
    public MoveInGarden(NavMeshAgent ag) : base(ag) { }



    public override void FinishState()
    {
        base.FinishState();
        //EventQueue.eventQueue.AddEvent(new ChangeStateEventData(HumanStates.GoToHouse));
    }

}
