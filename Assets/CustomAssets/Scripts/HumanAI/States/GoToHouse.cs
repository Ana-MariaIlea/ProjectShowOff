using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToHouse : BaseState
{

    [SerializeField]
    GameObject houseLocation;


    public override void UpdateBehavior()
    {
        agent.SetDestination(target.position);


        Vector3 distanceToLocation = self.position - target.position;


        if (distanceToLocation.magnitude < 1f)
        {
            FinishState();
        }
    }

    public void ResetTarget()
    {
        target = houseLocation.transform;
    }

    public override void FinishState()
    {
        base.FinishState();
        target = self;
    }
}
