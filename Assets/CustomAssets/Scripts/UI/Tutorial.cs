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
        panels[index].panel.SetActive(true);
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
                case 7:
                    ChackConditionBasedOnTime();
                    
                    break;
                case 8:
                    ChackSpecialConditionBasedOnTime();
                    break;
                case 9:
                    ChackConditionBasedOnTime();
                    break;
                //default:
                //    gameObject.SetActive(false);
                //    break;



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
            panels[index].panel.SetActive(false);
            if (panels[index].timeToShowNextPanel < 0)
            {
                if (index != 8)
                {
                    index = 8;
                }
                else
                {
                    index = 9;
                }
                panels[index].panel.SetActive(true);
                Debug.Log(panels[index].panel.activeSelf+ panels[index].panel.name);
            }
            else
            {
                panels[index].timeToShowNextPanel -= Time.deltaTime;
            }
            
        }
        else
        {
            panels[index].timeToShowPanel -= Time.deltaTime;
        }
    }

    public void IncreasePanelIndex()
    {
        Debug.Log(panels[index].panel.activeSelf + panels[index].panel.name);
        panels[index].panel.SetActive(false);
        Debug.Log(panels[index].panel.activeSelf + panels[index].panel.name);
        if (index != 7)
        {
            index++;
            if (index < panels.Count)
            {
                panels[index].panel.SetActive(true);
                Debug.Log(panels[index].panel.name);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void SetIndex(int i)
    {
        panels[index].panel.SetActive(false);
        index = i;
        panels[index].panel.SetActive(true);
        Debug.Log(panels[index].panel.name);
    }

    public void EndTutorial()
    {
        gameObject.SetActive(false);
    }
}
