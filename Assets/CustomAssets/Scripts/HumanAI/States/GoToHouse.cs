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
        target = house.transform;
        timerToStay = time;
        ag.gameObject.GetComponent<HumanAnimatorState>().ChangeAnimatorState(HumanAnimationStates.WALK);
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

    bool sendEvent = false;
    public override void FinishState()
    {
        base.FinishState();
        target = agent.transform;
        agent.gameObject.GetComponent<HumanAnimatorState>().ChangeAnimatorState(HumanAnimationStates.IDLE);
        if (timerToStay < 0)
        {
            if (sendEvent == false)
            {
                EventQueue.eventQueue.AddEvent(new ChangeStateStartEventData());
                sendEvent = true;
            }
        }
        else
        {
            timerToStay -= Time.deltaTime;
        }
    }
}
