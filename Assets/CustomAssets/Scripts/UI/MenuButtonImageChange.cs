using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonImageChange : MonoBehaviour
{

    public Sprite buttonUnpressed;
    public Sprite buttonPressed;

    Image imageComponent;
    // Start is called before the first frame update
    void Start()
    {
        imageComponent = GetComponent<Image>();
        ChangeButtonUnPressed();
    }

    public void ChangeButtonPressed()
    {
        imageComponent.sprite = buttonPressed;
    }

    public void ChangeButtonUnPressed()
    {
        imageComponent.sprite = buttonUnpressed;
    }
}
