using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MoveInGarden : BaseState
{
    public MoveInGarden(NavMeshAgent ag) : base(ag) { }
    private void OnDrawGizmos()
    {
        foreach (Waypoint waypoint in path)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(waypoint.transform.position, .3f);
        }

    }


    public override void FinishState()
    {
        base.FinishState();
        EventQueue.eventQueue.AddEvent(new ChangeStateEventData(HumanStates.GoToHouse));
    }

}
