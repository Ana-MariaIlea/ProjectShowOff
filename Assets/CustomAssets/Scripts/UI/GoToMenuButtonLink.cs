using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMenuButtonLink : MonoBehaviour
{
    public void OnButtonIsCLicked()
    {
        if(GameManager.instance)
        GameManager.instance.BackToMenuFromMainScene();
    }

    public void OnButtonIsCLickedBonus()
    {
        if (GameManager.instance)
            GameManager.instance.BackToMenuFromBonusScene();
    }
}
