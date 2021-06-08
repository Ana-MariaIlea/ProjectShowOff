using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighscoreTable : MonoBehaviour
{
    public static HighscoreTable instance;
    //[SerializeField]
    private Transform entryContainer;
    [SerializeField]
    private Transform entryTemplate;
    [SerializeField]
    float templateHeight = 20f;


    private List<Transform> highscoreEntryTransformList=new List<Transform>();
    private void Awake()
    {
        if (instance == null)
            instance = this;

        entryContainer = transform.Find("HighScoreEntryContainer");

        entryTemplate.gameObject.SetActive(false);


        string jsonString = PlayerPrefs.GetString("highscoreTable");

        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            List<HighscoreEntry> entries = new List<HighscoreEntry>();
            for (int i = 0; i < 10; i++)
            {
                entries.Add(new HighscoreEntry(0, "N/A"));
            }
            Highscores h=new Highscores { highscoreEntryList = entries };

            string json = JsonUtility.ToJson(h);
            PlayerPrefs.SetString("highscoreTable", json);
            PlayerPrefs.Save();
        }

        jsonString = PlayerPrefs.GetString("highscoreTable");
        highscores = JsonUtility.FromJson<Highscores>(jsonString);
        foreach (HighscoreEntry entry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntry(entry, entryContainer, highscoreEntryTransformList);
        }

    }

    public void ResetHighscoreTable()
    {
       var childern = entryContainer.GetComponentsInChildren<Transform>();
        //var childern = entryContainer.;

        for (int i = childern.Length-1; i >= 0; i--)
        {
            if(childern[i]!=entryContainer)
            Destroy(childern[i].gameObject);
        }

        

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        highscoreEntryTransformList = new List<Transform>();

        foreach (HighscoreEntry entry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntry(entry, entryContainer, highscoreEntryTransformList);
        }
    }

    public void AddHighScoreEntry(int score, string name)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry(score, name);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        highscores.highscoreEntryList.Add(highscoreEntry);

        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i+1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    HighscoreEntry aux = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = aux;
                }
            }
        }

        if (highscores.highscoreEntryList.Count > 10)
        {
            
            highscores.highscoreEntryList.RemoveAt(highscores.highscoreEntryList.Count - 1);
        }
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }


    public void AddHighScoreEntryAndReset(int score, string name)
    {
        AddHighScoreEntry(score, name);
        ResetHighscoreTable();
    }

    private void CreateHighscoreEntry(HighscoreEntry highscoreEntry, Transform container,List<Transform> transformList)
    {

        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);
        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            case 1:
                rankString = "1ST";
                break;
            case 2:
                rankString = "2ND";
                break;
            case 3:
                rankString = "3RD";
                break;
            default:
                rankString = rank + "TH";
                break;

        }

        entryTransform.Find("Rank").GetComponent<TextMeshProUGUI>().text = rankString;
        int score = highscoreEntry.score;
        entryTransform.Find("Score").GetComponent<TextMeshProUGUI>().text = score.ToString(); ;
        entryTransform.Find("Name").GetComponent<TextMeshProUGUI>().text = highscoreEntry.name;

        transformList.Add(entryTransform);
    }

    public void ResetScores()
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        highscores.resetList();
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    public void AddTestEntry()
    {
        AddHighScoreEntry(1000, "Scene good");
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;

        public void resetList()
        {

            highscoreEntryList = new List<HighscoreEntry>();
            for (int i = 0; i < 10; i++)
            {
                highscoreEntryList.Add(new HighscoreEntry(0, "N/A"));
            }
        }
    }

    //<summary>
    // Represents a highscore entry
    //<summary
    [System.Serializable]
    private class HighscoreEntry
    {


        public int score;
        public string name;

        public HighscoreEntry(int _score, string _name)
        {
            score = _score;
            name = _name;
        }
    }
}
