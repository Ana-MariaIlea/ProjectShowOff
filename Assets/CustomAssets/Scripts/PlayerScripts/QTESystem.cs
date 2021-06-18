using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class QTESystem : MonoBehaviour
{
    [HideInInspector]
    public int QTEGen;
    [HideInInspector]
    public int WaitingForKey;
    [HideInInspector]
    public int CorrectKey;
    [HideInInspector]
    public int CountingDown;
    public float WaitTime;
    public float CountDownTimer;
    public List<Sprite> icons;
    public Image Frame;
    public Image Letter;
    public int numberOfAtemptes;
    [SerializeField]
    int atemptes;
    float timer;


    private void Awake()
    {
        timer = CountDownTimer;
        atemptes = numberOfAtemptes;
        GetComponent<QTESounds>().PlayStartSound();
    }

    private void OnEnable()
    {
        Debug.Log("Script enabled");
        timer = CountDownTimer;
        atemptes = numberOfAtemptes;
        //Frame.gameObject.SetActive(true);
        //Letter.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        Frame.gameObject.SetActive(false);
        Letter.gameObject.SetActive(false);
    }

    void Update()
    {
        if (atemptes > 0)
        {
            if (WaitingForKey == 0)
            {
                QTEGen = UnityEngine.Random.Range(1, 6);
                CountingDown = 1;
                StartCoroutine(CountDown());
                switch (QTEGen)
                {
                    case 1:
                        WaitingForKey = 1;
                        //change prefab on the screen
                        timer = CountDownTimer;
                        Letter.sprite = icons[0];
                        Frame.fillAmount = 1;
                        Frame.gameObject.SetActive(true);
                        Letter.gameObject.SetActive(true);

                        break;
                    case 2:
                        WaitingForKey = 1;
                        //change prefab on the screen
                        timer = CountDownTimer;
                        Letter.sprite = icons[1];
                        Frame.fillAmount = 1;
                        Frame.gameObject.SetActive(true);
                        Letter.gameObject.SetActive(true);

                        break;
                    case 3:
                        WaitingForKey = 1;
                        //change prefab on the screen
                        timer = CountDownTimer;
                        Letter.sprite = icons[2];
                        Frame.fillAmount = 1;
                        Frame.gameObject.SetActive(true);
                        Letter.gameObject.SetActive(true);

                        break;
                    case 4:
                        WaitingForKey = 1;
                        //change prefab on the screen
                        timer = CountDownTimer;
                        Letter.sprite = icons[3];
                        Frame.fillAmount = 1;
                        Frame.gameObject.SetActive(true);
                        Letter.gameObject.SetActive(true);

                        break;
                    case 5:
                        WaitingForKey = 1;
                        //change prefab on the screen
                        timer = CountDownTimer;
                        Letter.sprite = icons[4];
                        Frame.fillAmount = 1;
                        Frame.gameObject.SetActive(true);
                        Letter.gameObject.SetActive(true);

                        break;
                }

            }
        }
        else
        {
            Debug.Log("Get Out");
            Debug.Log(atemptes);
            if (atemptes == 0)
            {
                Debug.Log("Collect Nectar in QTE");
                GetComponent<QTESounds>().PlayWinSound();
                try
                {

                    EventQueue.eventQueue.AddEvent(new CollectNectarEventData());
                }
                catch (Exception e)
                {
                    Debug.LogWarning(e);
                }
                atemptes=-4;
            }
            Debug.Log(atemptes);
            EventQueue.eventQueue.AddEvent(new ChangePlayerStateEventData(PlayerStates.Movement));

        }


        switch (QTEGen)
        {
            case 1:
                if (Input.anyKeyDown)
                {
                    if (Input.GetButtonDown("QTEKey1"))
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
                    if (Input.GetButtonDown("QTEKey2"))
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
                    if (Input.GetButtonDown("QTEKey3"))
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
            case 4:
                if (Input.anyKeyDown)
                {
                    if (Input.GetButtonDown("QTEKey4"))
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
            case 5:
                if (Input.anyKeyDown)
                {
                    if (Input.GetButtonDown("QTEKey5"))
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
            Frame.gameObject.SetActive(false);
            Letter.gameObject.SetActive(false);
            //if (atemptes > 1)
                GetComponent<QTESounds>().PlayPassSound();
            //Debug.Log("correct key pressed");
            if (atemptes > 0)
                yield return new WaitForSeconds(WaitTime);
            else yield return new WaitForSeconds(0.1f);
            CorrectKey = 0;
            //Reset texts
            timer = CountDownTimer;
            WaitingForKey = 0;
            CountingDown = 1;
            atemptes--;
        }
        else if (CorrectKey == 2)
        {
            CountingDown = 2;
            //Screen update fail key;
            Frame.gameObject.SetActive(false);
            Letter.gameObject.SetActive(false);
            atemptes = -4;
            GetComponent<QTESounds>().PlayLoseSound();
            Debug.Log("wrong key pressed");

            yield return new WaitForSeconds(WaitTime);
            CorrectKey = 0;
            //Reset texts

            WaitingForKey = 0;
            CountingDown = 1;
            EventQueue.eventQueue.AddEvent(new ChangePlayerStateEventData(PlayerStates.Movement));

        }
    }

    IEnumerator CountDown()
    {

        //yield return new WaitForSeconds(4f);
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            Frame.fillAmount = timer / CountDownTimer;
            yield return null;
        }

        timer = CountDownTimer;

        if (CountingDown == 1&&atemptes>1)
        {
            QTEGen = 14;
            CountingDown = 2;
            //fail update on the screen
            Debug.Log("wrong key time done");
            GetComponent<QTESounds>().PlayLoseSound();
            Frame.gameObject.SetActive(false);
            Letter.gameObject.SetActive(false);
            atemptes = -4;
            yield return new WaitForSeconds(WaitTime);
            CorrectKey = 0;
            //Reset texts

            WaitingForKey = 0;
            CountingDown = 1;
            timer = CountDownTimer;
            EventQueue.eventQueue.AddEvent(new ChangePlayerStateEventData(PlayerStates.Movement));

        }
    }
}
