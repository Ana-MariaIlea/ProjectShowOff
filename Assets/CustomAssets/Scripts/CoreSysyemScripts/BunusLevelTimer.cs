using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunusLevelTimer : MonoBehaviour
{
    [SerializeField]
    GameObject screen;
    [SerializeField] 
    int minutes;
    [SerializeField]
    int seconds;

    private int playerScore;
    private string playerName;

    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = minutes * 60 + seconds;
        if (GameManager.instance != null)
        {
            playerName = GameManager.instance.playerName;
            playerScore = GameManager.instance.playerScore;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (time < 0)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            screen.SetActive(true);
        }
        else
        {
            time -= Time.deltaTime;
        }
    }
}
