using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SprayPesticides : BaseState
{

    [Tooltip("Add Pesticide Particle Effect attached to the Human")]
    [SerializeField]
    private ParticleSystem particles;


    public SprayPesticides(NavMeshAgent ag) : base(ag) { }
    //private void OnDrawGizmos()
    //{
    //    foreach (Waypoint waypoint in path)
    //    {
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawSphere(waypoint.transform.position, .3f);
    //    }

    //}
    public override void StayPut()
    {
        agent.SetDestination(agent.transform.position);
        if(!particles.isPlaying)
        particles.Play();
        if (timer <= 0)
        {
            if (particles.isPlaying)
                particles.Stop();
            walkPointSet = false;
        }
        else
        {
            timer -= Time.fixedDeltaTime;
        }
    }

    public override void SearchWalkPoint()
    {
        walkPoint++;
       // if (walkPoint > path.Count - 1 || walkPoint < 0) walkPoint = 0;
        if (walkPoint > path.Count - 1) FinishState();
        else
        {
            target = path[walkPoint].transform;
            timer = path[walkPoint].timeToStay;
            walkPointSet = true;
        }
        
    }

    public override void FinishState()
    {
        base.FinishState();
        EventQueue.eventQueue.AddEvent(new ChangeStateEventData(HumanStates.GoToHouse));
    }

}
