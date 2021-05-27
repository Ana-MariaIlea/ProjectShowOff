using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTESystem : MonoBehaviour
{

    public int QTEGen;
    public int WaitingForKey;
    public int CorrectKey;
    public int CountingDown;

    void Update()
    {
        if (WaitingForKey == 0)
        {
            QTEGen = Random.Range(1, 4);
            CountingDown = 1;
            StartCoroutine(CountDown());
            switch (QTEGen)
            {
                case 1:
                    WaitingForKey = 1;
                    //change prefab on the screen
                    break;
                case 2:
                    WaitingForKey = 1;
                    //change prefab on the screen
                    break;
                case 3:
                    WaitingForKey = 1;
                    //change prefab on the screen
                    break;
            }
        }

        switch (QTEGen)
        {
            case 1:
                if (Input.anyKeyDown)
                {
                    if (Input.GetButtonDown("RKey"))
                    {
                        CorrectKey = 1;
                        StartCoroutine(KeyPressing());
                    }
                    else
                    {
                        CorrectKey = 2;
                        StartCoroutine(KeyPressing());
                    }
                }
                break;
            case 2:
                if (Input.anyKeyDown)
                {
                    if (Input.GetButtonDown("FKey"))
                    {
                        CorrectKey = 1;
                        StartCoroutine(KeyPressing());
                    }
                    else
                    {
                        CorrectKey = 2;
                        StartCoroutine(KeyPressing());
                    }
                }
                break;
            case 3:
                if (Input.anyKeyDown)
                {
                    if (Input.GetButtonDown("TKey"))
                    {
                        CorrectKey = 1;
                        StartCoroutine(KeyPressing());
                    }
                    else
                    {
                        CorrectKey = 2;
                        StartCoroutine(KeyPressing());
                    }
                }
                break;
        }

    }

    IEnumerator KeyPressing()
    {
        QTEGen = 10;
        if (CorrectKey == 1)
        {
            CountingDown = 2;
            //Screen update correct key;
            yield return new WaitForSeconds(1.5f);
            CorrectKey = 0;
            //Reset texts
            WaitingForKey = 0;
            CountingDown = 1;
        }
        else if (CorrectKey == 2)
        {
            CountingDown = 2;
            //Screen update fail key;
            yield return new WaitForSeconds(1.5f);
            CorrectKey = 0;
            //Reset texts
            WaitingForKey = 0;
            CountingDown = 1;
        }
    }

    IEnumerator CountDown()
    {

    }
}
