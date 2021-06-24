using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunusLevelTimer : MonoBehaviour
{

    [SerializeField] 
    int minutes;
    [SerializeField]
    int seconds;

    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = minutes * 60 + seconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (time < 0)
        {
            Time.timeScale = 0f;
        }
        else
        {
            time -= Time.deltaTime;
        }
    }
}
