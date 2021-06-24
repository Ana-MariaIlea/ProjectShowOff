using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PickFlowers : BaseState
{


    public PickFlowers(NavMeshAgent ag,List<GameObject> path) : base(ag) 
    {
        this.path = path;
        ag.gameObject.GetComponent<HumanAnimatorState>().ChangeAnimatorState(HumanAnimationStates.WALK);
        EventQueue.eventQueue.Subscribe(EventType.PICKFLOWERANIMATION, pickUpFlower);
        EventQueue.eventQueue.Subscribe(EventType.CHANGEWAYPOINTANIMATION, ChangeWaypoint);

    }
    public override void UpdateBehavior()
    {
        if (path.Count > 0)
            Patroling();
        else FinishState();
    }



    public override void StayPut()
    {
        //Debug.Log("Pick flower-Destination reached");
        agent.SetDestination(agent.transform.position);
        agent.gameObject.GetComponent<HumanAnimatorState>().ChangeAnimatorState(HumanAnimationStates.PICKFLOWER);
        //if (timer <= 0)
        //{
           // pickUpFlower();
            
        //}
        //else
        //{
        //    timer -= Time.fixedDeltaTime;
        //}
    }

    public void pickUpFlower(EventData eventData)
    {
        if (eventData is PickFlowerAnimationEventData)
        {
            //Debug.Log("Pick flower");
            target = null;
            walkPointSet = false;
            GameObject f = path[walkPoint].gameObject;
            path.RemoveAt(walkPoint);
            agent.gameObject.GetComponent<HumanAnimatorState>().ChangeAnimatorState(HumanAnimationStates.WALK);
            EventQueue.eventQueue.AddEvent(new FlowerDestroyEventData(f));
            //Destroy(f);
        }
    }

    public void ChangeWaypoint(EventData eventData)
    {
        if (eventData is ChangeWaypointAnimationEventData)
        {
            walkPointSet = false;
        }
    }


    public override void FinishState()
    {
        EventQueue.eventQueue.UnSubscribe(EventType.PICKFLOWERANIMATION, pickUpFlower);
        EventQueue.eventQueue.UnSubscribe(EventType.CHANGEWAYPOINTANIMATION, ChangeWaypoint);
        base.FinishState();
        EventQueue.eventQueue.AddEvent(new EndStateEventData());
    }
}
