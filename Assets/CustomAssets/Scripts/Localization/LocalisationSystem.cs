using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalisationSystem
{
    public enum Language
    {
        English,
        Dutch
    }

    public static Language language = Language.Dutch;

    private static Dictionary<string, string> localisedEN;
    private static Dictionary<string, string> localisedNL;

    public static bool isInit;

    public static void Init()
    {
        CSVLoader csvLoader = new CSVLoader();
        csvLoader.LoadCSV();

        localisedEN = csvLoader.GetDictionaryValues("en");
        localisedNL = csvLoader.GetDictionaryValues("nl");

        isInit = true;
    }

    public static string GetLocalisedValue(string key)
    {
        if (!isInit)
        {
            Init();
        }

        string value = key;
        switch (language)
        {
            case Language.English:
                localisedEN.TryGetValue(key, out value);
                break;
            case Language.Dutch:
                localisedNL.TryGetValue(key, out value);
                break;
        }

        return value;
    }
}
