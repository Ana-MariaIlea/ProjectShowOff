using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInformationTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
           // test();
        }
    }

    public void test()
    {
        Cursor.lockState = CursorLockMode.None;
        GameManager.instance.BackToMenuFromMainScene();
        HighscoreTable.instance.AddTestEntry();
    }
}
