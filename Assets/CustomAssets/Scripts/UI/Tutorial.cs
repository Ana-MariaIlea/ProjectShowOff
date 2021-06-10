using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;
    [SerializeField]
    private List<TutorialPanel> panels;
    [Tooltip("time to wait in between the panels")]
    public float time;
    int index = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {

    }
    bool[] panel1Condition = new bool[2];
    bool[] panel2Condition = new bool[2];

    // Update is called once per frame
    void Update()
    {
        if (panels.Count > 0)
        {
            switch (index)
            {
                case 0:
                    CheckConditionPanel1();
                    break;
                case 1:
                    CheckConditionPanel2();
                    break;
                case 4:
                    ChackConditionBasedOnTime();
                    break;
                case 5:
                    ChackSpecialConditionBasedOnTime();
                    break;
                case 6:
                    ChackConditionBasedOnTime();
                    break;
                case 7:
                    ChackConditionBasedOnTime();
                    break;
                default:
                    gameObject.SetActive(false);
                    break;



            }
        }
    }

    private void CheckConditionPanel2()
    {
        if (panels[index].condition == Condition.Condition)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                panel2Condition[0] = true;
            }
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                panel2Condition[1] = true;
            }


            if (panel2Condition[0] == true && panel2Condition[1] == true)
            {
                IncreasePanelIndex();
            }
        }
        else
        {
            ChackConditionBasedOnTime();
        }
    }

    private void CheckConditionPanel1()
    {
        if (panels[index].condition == Condition.Condition)
        {
            if (Input.GetButton("FlyUp"))
            {
                panel1Condition[0] = true;
            }
            if (Input.GetButton("FlyDown"))
            {
                panel1Condition[1] = true;
            }


            if (panel1Condition[0] == true && panel1Condition[1] == true)
            {
                IncreasePanelIndex();
            }
        }
        else
        {
            ChackConditionBasedOnTime();
        }
    }

    private void ChackConditionBasedOnTime()
    {
        if (panels[index].timeToShowPanel < 0)
        {
            IncreasePanelIndex();
        }
        else
        {
            panels[index].timeToShowPanel -= Time.deltaTime;
        }
    }

    private void ChackSpecialConditionBasedOnTime()
    {
        if (panels[index].timeToShowPanel < 0)
        {
            if (panels[index].timeToShowNextPanel < 0)
            {
                index++;
                panels[index].panel.SetActive(true);
            }
            else
            {
                panels[index].timeToShowNextPanel -= Time.deltaTime;
            }
            panels[index].panel.SetActive(false);
        }
        else
        {
            panels[index].timeToShowPanel -= Time.deltaTime;
        }
    }

    public void IncreasePanelIndex()
    {
        panels[index].panel.SetActive(false);
        index++;
        panels[index].panel.SetActive(true);
    }

    public void SetIndex(int i)
    {
        panels[index].panel.SetActive(false);
        index = i;
        panels[index].panel.SetActive(true);
    }

    public void EndTutorial()
    {
        gameObject.SetActive(false);
    }
}
