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
    // [Tooltip("Add LawnMower GameObject attached to the Human")]
    //[SerializeField]
    //private GameObject lawnmower;
    [Tooltip("Add The GameObject that represents the launge area")]
    [SerializeField]
    private GameObject launge;
    [Tooltip("Add The GameObject that represents the gradd to be eliminated")]
    [SerializeField]
    private List<GameObject> ObjectsToTurnOff;

    bool firstState;
    bool secondState;
    public CutGrass(NavMeshAgent ag, ParticleSystem particles, GameObject launge, List<GameObject> objectsToTurnOff, List<Waypoint> path) : base(ag)
    {
        this.particles = particles;


        this.particles.Stop();
        firstState = false;
        secondState = false;

        this.launge = launge;
        this.ObjectsToTurnOff = objectsToTurnOff;
        for (int i = 0; i < path.Count; i++)
        {
            this.path.Add(path[i].gameObject);
        }
        this.pointToStartCutting = this.path[0];
        this.pointToEndCutting = this.path[this.path.Count - 1];
        Debug.Log("cut grass change animation");
        //ag.gameObject.GetComponent<HumanAnimatorState>().ChangeAnimatorState(HumanAnimationStates.WALK);
    }


    public override void HandleTargetreached()
    {
        if (path[walkPoint] == pointToStartCutting)
        {
            if (firstState==false)
            {
                firstState = true;
                particles.Play();
                Debug.Log("cut grass change walk mower animation");
                agent.gameObject.GetComponent<HumanAnimatorState>().ChangeAnimatorState(HumanAnimationStates.WALKMOWER);
                EventQueue.eventQueue.AddEvent(new HandleHumanObjectStateEventData("Lawnmower", true));
                EventQueue.eventQueue.AddEvent(new PlayLawnmowerSoundEventData());
            }
            else
            {
                Debug.Log("Particles are player for some reason");
            }


        }

        if (path[walkPoint] == pointToEndCutting)
        {
            if (secondState==false)
            {
                secondState = true;
                particles.Stop();
                Debug.Log("cut grass change walk  animation");
                //agent.gameObject.GetComponent<HumanAnimatorState>().ChangeAnimatorState(HumanAnimationStates.WALK);
                EventQueue.eventQueue.AddEvent(new HandleHumanObjectStateEventData("Lawnmower", false));
                EventQueue.eventQueue.AddEvent(new StopLawnmowerSoundEventData());
            }

            if (launge.activeSelf == false)
            {
                launge.SetActive(true);
            }
            foreach (GameObject item in ObjectsToTurnOff)
            {
                if (item.activeSelf == true)
                {
                    item.SetActive(false);
                }
            }


            FinishState();
        }
        agent.SetDestination(agent.transform.position);
        walkPointSet = false;
        //base.HandleTargetreached();
    }


    public override void FinishState()
    {
        base.FinishState();
        EventQueue.eventQueue.AddEvent(new EndStateEventData());
    }
}
