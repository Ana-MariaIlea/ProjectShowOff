using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SprayPesticides : BaseState
{

    [Tooltip("Add Pesticide Particle Effect attached to the Human")]
    [SerializeField]
    private ParticleSystem particles;
    private List<float> times=new List<float>();
    private FMODUnity.StudioEventEmitter emitter;


    public SprayPesticides(NavMeshAgent ag, ParticleSystem particle, List<Waypoint> path, FMODUnity.StudioEventEmitter e) : base(ag)
    {
        this.particles = particle;
        
        for (int i = 0; i < path.Count; i++)
        {
            this.path.Add(path[i].gameObject);
            times.Add(path[i].timeToStay);
            emitter = e;
        }

    }


    public override void StayPut()
    {
       // Debug.Log(timer);
       // Debug.Log("Waypoint reached");
        agent.SetDestination(agent.transform.position);
        if (!particles.isPlaying)
        {
           // Debug.Log("Play particles");
            particles.Play();
            //emitter.enabled = true;
            EventQueue.eventQueue.AddEvent(new PlaySprayParticlesSoundEventData());
        }
        if (timer <= 0)
        {
            if (particles.isPlaying)
            {
                // Debug.Log("Stop particles");
               // emitter.enabled = false;
                particles.Stop();
            }
            walkPointSet = false;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    public override void SearchWalkPoint()
    {
        //if(walkPoint>0)
        //Debug.Log("Waypoint last "+ path[walkPoint].gameObject.name);
        walkPoint++;


        // if (walkPoint > path.Count - 1 || walkPoint < 0) walkPoint = 0;
        if (walkPoint > path.Count - 1) FinishState();
        else
        {
            target = path[walkPoint].transform;
            timer = times[walkPoint];
           // Debug.Log("Waypoint new " + path[walkPoint].gameObject.name);

            walkPointSet = true;
        }

    }

    public override void FinishState()
    {
        base.FinishState();
        EventQueue.eventQueue.AddEvent(new EndStateEventData());
    }

}
