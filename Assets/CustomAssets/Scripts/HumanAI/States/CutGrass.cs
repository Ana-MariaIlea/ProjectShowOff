using System.Collections;
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

    private void OnDrawGizmos()
    {
        foreach (PathWay waypoint in path)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(waypoint.pathHolder.transform.position, .3f);
        }

    }

    public override void HandleTargetreached()
    {
        if (path[walkPoint].pathHolder == pointToStartCutting)
        {
            if (!particles.isPlaying)
                particles.Play();
            if (lawnmower.activeSelf == false)
            {
                lawnmower.SetActive(true);
            }
        }

        if (path[walkPoint].pathHolder == pointToEndCutting)
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
        EventQueue.eventQueue.AddEvent(new ChangeStateEventData(HumanStates.GoToHouse));
    }
}
