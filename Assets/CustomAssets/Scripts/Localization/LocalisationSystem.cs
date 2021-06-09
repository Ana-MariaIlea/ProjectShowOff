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

    public static Language language = Language.English;

    private static Dictionary<string, string> localisedEN;
    private static Dictionary<string, string> localisedNL;

    public static bool isInit;
    public static CSVLoader csvLoader;

    public static void ChangeLanguage(Language lan)
    {
        language = lan;
        Debug.Log(language);
    }

    public static void Init()
    {
        csvLoader = new CSVLoader();
        csvLoader.LoadCSV();
        UpdateDictionaries();

        isInit = true;
    }

    private static void UpdateDictionaries()
    {
        localisedEN = csvLoader.GetDictionaryValues("en");
        localisedNL = csvLoader.GetDictionaryValues("nl");
    }
    public static Dictionary<string,string> GetDictionaryForEditor()
    {
        if (!isInit)
        {
            Init();
        }

        return localisedEN;
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

    public static string GetLocalisedValueBySpecificDictionary(string key,Language language)
    {
        if (!isInit)
        {
            Init();
        }

        string value = null;
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

    public static void Add(string key, string value)
    {
        if (value.Contains("\""))
        {
            value.Replace('"', '\"');
        }

        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }

        csvLoader.LoadCSV();
        csvLoader.Add(key, value);
        csvLoader.LoadCSV();

        UpdateDictionaries();
    }

    public static void Replace(string key, string value)
    {
        if (value.Contains("\""))
        {
            value.Replace('"', '\"');
        }

        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }

        csvLoader.LoadCSV();
        csvLoader.Edit(key, value);
        csvLoader.LoadCSV();

        UpdateDictionaries();
    }

    public static void Remove(string key)
    {
        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }

        csvLoader.LoadCSV();
        csvLoader.Remove(key);
        csvLoader.LoadCSV();

        UpdateDictionaries();
    }
}
