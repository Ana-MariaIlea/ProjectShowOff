﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CutGrass : BaseState
{
    [SerializeField]
    private GameObject pointToStartCutting;
    [SerializeField]
    private GameObject pointToEndCutting;
    [Tooltip("Add Smoke Particle Effect attached to the Human")]
    [SerializeField]
    private ParticleSystem particles;
    [Tooltip("Add LawnMower GameObject attached to the Human")]
    [SerializeField]
    private GameObject lawnmower;
    [Tooltip("Add The GameObject that represents the launge area")]
    [SerializeField]
    private GameObject launge;
    [Tooltip("Add The GameObject that represents the gradd to be eliminated")]
    [SerializeField]
    private GameObject grass;


    public CutGrass(NavMeshAgent ag, ParticleSystem particles, GameObject mawer, GameObject launge,GameObject plants, List<GameObject> path) : base(ag)
    {
        this.particles = particles;
        this.lawnmower = mawer;
        this.launge = launge;
        this.grass = plants;
        this.path = path;
    }


    public override void HandleTargetreached()
    {
        if (path[walkPoint] == pointToStartCutting)
        {
            if (!particles.isPlaying)
                particles.Play();
            if (lawnmower.activeSelf == false)
            {
                lawnmower.SetActive(true);
            }
        }

        if (path[walkPoint] == pointToEndCutting)
        {
            if (particles.isPlaying)
                particles.Stop();
            if (lawnmower.activeSelf == true)
            {
                lawnmower.SetActive(false);
            }
            if (launge.activeSelf == false)
            {
                launge.SetActive(true);
            }
            if (grass.activeSelf == true)
            {
                grass.SetActive(false);
            }

            FinishState();
        }
        base.HandleTargetreached();
    }


    public override void FinishState()
    {
        base.FinishState();
        EventQueue.eventQueue.AddEvent(new EndStateEventData());
    }
}
