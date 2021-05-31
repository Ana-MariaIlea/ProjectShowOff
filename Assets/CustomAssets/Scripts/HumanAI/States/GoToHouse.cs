using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToHouse : BaseState
{

    [SerializeField]
    Transform houseLocation;
    public GoToHouse(NavMeshAgent ag) : base(ag)
    {

    }
    private void OnDrawGizmos()
    {

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(houseLocation.position, .3f);
        

    }
    public override void UpdateBehavior()
    {
        agent.SetDestination(target.position);


        Vector3 distanceToLocation = agent.transform.position - target.position;


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
        target = agent.transform;
    }
}
