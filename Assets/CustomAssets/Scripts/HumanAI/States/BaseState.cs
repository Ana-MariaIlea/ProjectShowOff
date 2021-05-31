using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseState
{
    public bool isStateFinished = false;
    public HumanStates state;
    public virtual void UpdateBehavior()
    {
        Patroling();
    }

    [SerializeField]
    protected List<GameObject> path;


    protected int walkPoint = -1;
    protected bool walkPointSet;
    [HideInInspector]
    protected NavMeshAgent agent;
    //[HideInInspector]
   // protected Transform self;
    protected Transform target;
    protected float timer = 5;

    public BaseState(NavMeshAgent ag)
    {
        agent = ag;
    }

    public void Patroling()
    {
        // Debug.Log("patrol");
        if (walkPointSet == false)
        {
            SearchWalkPoint();
        }
        else
        {
            agent.SetDestination(target.position);
        }

        Vector3 distanceToLocation = agent.transform.position - target.position;


        if (distanceToLocation.magnitude < 1f)
        {
            HandleTargetreached();
        }

    }

    public virtual void HandleTargetreached()
    {
        StayPut();
    }
    public virtual void StayPut()
    {
        agent.SetDestination(agent.transform.position);
        if (timer <= 0)
        {
            walkPointSet = false;
        }
        else
        {
            timer -= Time.fixedDeltaTime;
        }
    }

    public virtual void SearchWalkPoint()
    {
        walkPoint++;
        if (walkPoint > path.Count - 1 || walkPoint < 0) walkPoint = 0;
        target = path[walkPoint].transform;
        timer = 2f;
        walkPointSet = true;
    }

    public bool IsStateFinished()
    {
        return isStateFinished;
    }

    public virtual void FinishState()
    {
        isStateFinished = true;
    }



}
