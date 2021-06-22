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
        agent.SetDestination(agent.transform.position);
        if (!particles.isPlaying)
        {
            particles.Play();
            EventQueue.eventQueue.AddEvent(new PlaySprayParticlesSoundEventData());
        }
        if (timer <= 0)
        {
            if (particles.isPlaying)
            {
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
        walkPoint++;
        if (walkPoint > path.Count - 1) FinishState();
        else
        {
            target = path[walkPoint].transform;
            timer = times[walkPoint];
            walkPointSet = true;
        }

    }

    public override void FinishState()
    {
        base.FinishState();
        EventQueue.eventQueue.AddEvent(new EndStateEventData());
    }

}
