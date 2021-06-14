using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputName : MonoBehaviour
{
    string name = null;
    void Start()
    {
        
    }
    public void ChangeText(string text)
    {
        Debug.Log("name is changed");
        if (GameManager.instance != null)
        {
            GameManager.instance.ChangeText(text);
        }
        else
        {
            name = text;
        }
    }
}
