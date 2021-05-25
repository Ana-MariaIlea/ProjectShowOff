using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSwitcher : MonoBehaviour
{
    public CanvasType desiredCanvasType;

    public void OnScreenChange()
    {
        if (CanvasManager.instance == null)
        {
            return;
        }
        CanvasManager.instance.SwitchCanvas(desiredCanvasType);
    }
}
