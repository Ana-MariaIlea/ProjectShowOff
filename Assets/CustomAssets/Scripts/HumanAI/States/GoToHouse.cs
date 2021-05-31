using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToHouse : BaseState
{

    [SerializeField]
    Transform houseLocation;
    float timerToStay;
    public GoToHouse(NavMeshAgent ag, GameObject house,float time) : base(ag)
    {
        houseLocation = house.transform;
        timerToStay = time;
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
        if (timerToStay < 0)
        {
            EventQueue.eventQueue.AddEvent(new ChangeStateStartEventData());
        }
        else
        {
            timerToStay -= Time.deltaTime;
        }
    }
}
