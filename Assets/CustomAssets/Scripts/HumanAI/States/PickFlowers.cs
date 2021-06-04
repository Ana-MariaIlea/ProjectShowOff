using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PickFlowers : BaseState
{


    public PickFlowers(NavMeshAgent ag,List<GameObject> path) : base(ag) 
    {
        this.path = path;
    }
    public override void UpdateBehavior()
    {
        if (path.Count > 0)
            Patroling();
        else FinishState();
    }



    public override void StayPut()
    {
        Debug.Log("Pick flower-Destination reached");
        agent.SetDestination(agent.transform.position);
        if (timer <= 0)
        {
            pickUpFlower();
            walkPointSet = false;
        }
        else
        {
            timer -= Time.fixedDeltaTime;
        }
    }

    void pickUpFlower()
    {
        Debug.Log("Pick flower");
        target = null;
        GameObject f = path[walkPoint].gameObject;
        path.RemoveAt(walkPoint);
        EventQueue.eventQueue.AddEvent(new FlowerDestroyEventData(f));
        //Destroy(f);
    }


    public override void FinishState()
    {
        base.FinishState();
        EventQueue.eventQueue.AddEvent(new EndStateEventData());
    }
}
