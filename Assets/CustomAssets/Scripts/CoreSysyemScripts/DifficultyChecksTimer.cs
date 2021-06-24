using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum GardenZones
{
    WildPatch,
    FlowerGarden,
    VegetableGarden
}

[System.Serializable]
public class Timer
{
    public int minutes;
    public int seconds;
}

public class DifficultyChecksTimer : MonoBehaviour
{

    [SerializeField]
    private List<DifficultyCheck> checksForDifficulty;



    [Tooltip("Do NOT modify, preview avaliable for testing only")]
    [SerializeField]
    float timer = 0;
    [Tooltip("Do NOT modify, preview avaliable for testing only")]
    [SerializeField]
    float maxTimer = 0;
    public int difficultyCheckIndex = 0;

    private void Awake()
    {
        if (checksForDifficulty.Count != 0)
        {
            InitializeDifficultyChecks();
        }
    }

    private void InitializeDifficultyChecks()
    {
        foreach (var item in checksForDifficulty)
        {
            item.Initiallize();
        }

        for (int i = 0; i < checksForDifficulty.Count; i++)
        {
            for (int j = i + 1; j < checksForDifficulty.Count; j++)
            {
                if (checksForDifficulty[j].time < checksForDifficulty[i].time)
                {
                    DifficultyCheck aux = checksForDifficulty[i];
                    checksForDifficulty[i] = checksForDifficulty[j];
                    checksForDifficulty[j] = aux;
                }
            }
        }

        //for (int i = 0; i < checksForDifficulty.Count; i++)
        //{
        //    Debug.Log(checksForDifficulty[i].time);
        //    maxTimer += checksForDifficulty[i].time;
        //}

        maxTimer = checksForDifficulty[checksForDifficulty.Count-1].time+1;

    }


    // Update is called once per frame
    void Update()
    {
        // if (timer > 0)
        //  timer -= Time.deltaTime;
        if (checksForDifficulty.Count != 0)
            if (difficultyCheckIndex<checksForDifficulty.Count)
                if (timer > checksForDifficulty[difficultyCheckIndex].time)
                {
                   // Debug.Log("Start difficulty check "+ difficultyCheckIndex);
                    EventQueue.eventQueue.AddEvent(new CheckDifficultyEventData(checksForDifficulty[difficultyCheckIndex]));
                    difficultyCheckIndex++;
                }
                else
                {
                    timer += Time.deltaTime;
                }
    }


    public int GetDifficultyChecksListCount()
    {
        return checksForDifficulty.Count;
    }

}
