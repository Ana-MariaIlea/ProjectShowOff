using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextLocalisationUI : MonoBehaviour
{

    TextMeshProUGUI textField;
    [SerializeField]
    private LocalisedString localisedString;
    // Start is called before the first frame update
    void Start()
    {
        textField = GetComponent<TextMeshProUGUI>();
        textField.text = localisedString.value;
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            LanguageChangeUI.ChangeLanguage += OnLanguageChangeText;
        }
        
    }

    public void OnLanguageChangeText()
    {
        Debug.Log("TextChange");
        textField.text = localisedString.value;
    }
}
