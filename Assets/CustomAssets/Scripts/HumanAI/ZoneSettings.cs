using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ZoneSettings : MonoBehaviour
{
    public HumanStates stateOfZone;
    
    public int numberOfTimes;

    public GameObject launge;
    public GameObject plants;

    List<TestFlower> flowers;
    List<Waypoint> waypoints;

    [HideInInspector]
    public List<GameObject> currentFlowers;
    [HideInInspector]
    public List<Waypoint> currentWaypoints;

    int numberFlowers;
    int numberWaypoints;

    // Start is called before the first frame update
    void Start()
    {
        flowers = GetComponentsInChildren<TestFlower>().ToList();
        waypoints = GetComponentsInChildren<Waypoint>().ToList();
        if (numberOfTimes > 0)
        {
            numberFlowers = flowers.Count / numberOfTimes;
            numberWaypoints = waypoints.Count / numberOfTimes;
        }
        InitializeEventStats();
    }


    public void InitializeEventStats()
    {
        if (numberOfTimes > 1)
        {
            InitStateInProgress();
        }
        else if (numberOfTimes == 1)
        {
            InitLastState();
        }
    }

    private void InitLastState()
    {
        switch (stateOfZone)
        {
            case HumanStates.PickFlowes:
                currentFlowers = new List<GameObject>();
                for (int i = 0; i < flowers.Count; i++)
                {
                    currentFlowers.Add(flowers[i].gameObject);
                }
                break;
            case HumanStates.SprayGarden:
                currentWaypoints = new List<Waypoint>();
                for (int i = 0; i < waypoints.Count; i++)
                {
                    currentWaypoints.Add(waypoints[i]);
                }
                break;
            case HumanStates.CutGrass:
                currentWaypoints = new List<Waypoint>();
                for (int i = 0; i < waypoints.Count; i++)
                {
                    currentWaypoints.Add(waypoints[i]);
                }
                break;
        }
        numberOfTimes--;
    }

    private void InitStateInProgress()
    {
        switch (stateOfZone)
        {
            case HumanStates.PickFlowes:
                currentFlowers = new List<GameObject>();
                for (int i = 0; i < numberFlowers; i++)
                {
                    int rand = Random.Range(0, flowers.Count);
                    currentFlowers.Add(flowers[rand].gameObject);
                    Debug.Log("Flowers to be collected " + currentFlowers.Count);
                    flowers.RemoveAt(rand);
                }
                break;
            case HumanStates.SprayGarden:
                currentWaypoints = new List<Waypoint>();
                for (int i = 0; i < numberWaypoints; i++)
                {
                    int rand = Random.Range(0, waypoints.Count);
                    currentWaypoints.Add(waypoints[rand]);
                    waypoints.RemoveAt(rand);
                }
                break;
            case HumanStates.CutGrass:
                currentWaypoints = new List<Waypoint>();
                for (int i = 0; i < numberWaypoints; i++)
                {
                    int rand = Random.Range(0, waypoints.Count);
                    currentWaypoints.Add(waypoints[rand]);
                    waypoints.RemoveAt(rand);
                }
                break;
        }

        numberOfTimes--;
    }
}
