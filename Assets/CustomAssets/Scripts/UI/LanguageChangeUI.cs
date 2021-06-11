using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LanguageChangeUI : MonoBehaviour
{
   // public List<Sprite> Flags;
    public Sprite englishFlag;
    public Sprite dutchFlag;
    public Image FlagImage;
    int index = 0;

    public static Action ChangeLanguage;

    void Start()
    {
        switch (LocalisationSystem.language)
        {
            case LocalisationSystem.Language.English:
                FlagImage.sprite = englishFlag;
                break;
            case LocalisationSystem.Language.Dutch:
                FlagImage.sprite = dutchFlag;
                break;
        }
        //LocalisationSystem.language = LocalisationSystem.Language.English;

    }

    public void ChangeLanguageButtonPressed()
    {
        switch (index)
        {
            case 0:
                index++;
                FlagImage.sprite = dutchFlag;
                LocalisationSystem.ChangeLanguage(LocalisationSystem.Language.Dutch);
                ChangeLanguage?.Invoke();
                break;
            case 1:
                index--;
                FlagImage.sprite = englishFlag;
                LocalisationSystem.ChangeLanguage(LocalisationSystem.Language.English);
                ChangeLanguage?.Invoke();
                break;
        }
    }

    public void ChangeLanguageToEnglish()
    {
        LocalisationSystem.ChangeLanguage(LocalisationSystem.Language.English);
    }

    public void ChangeLanguageToDucth()
    {
        LocalisationSystem.ChangeLanguage(LocalisationSystem.Language.Dutch);
    }
}
