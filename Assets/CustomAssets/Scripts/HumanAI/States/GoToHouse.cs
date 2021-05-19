using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToHouse : BaseState
{

    [SerializeField]
    Transform houseLocation;

    private void OnDrawGizmos()
    {

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(houseLocation.position, .3f);
        

    }
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
        target = houseLocation;
    }

    public override void FinishState()
    {
        base.FinishState();
        target = self;
    }
}
