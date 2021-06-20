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

    int difficultyCheckIndex = 0;

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
                if (checksForDifficulty[j].time > checksForDifficulty[i].time)
                {
                    DifficultyCheck aux = checksForDifficulty[i];
                    checksForDifficulty[i] = checksForDifficulty[j];
                    checksForDifficulty[j] = aux;
                }
            }
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        if (checksForDifficulty.Count != 0)
            if (timer < timer - checksForDifficulty[difficultyCheckIndex].time)
            {
                difficultyCheckIndex++;
                EventQueue.eventQueue.AddEvent(new CheckDifficultyEventData(checksForDifficulty[difficultyCheckIndex]));
            }
    }


    public int GetDifficultyChecksListCount()
    {
        return checksForDifficulty.Count;
    }

}
