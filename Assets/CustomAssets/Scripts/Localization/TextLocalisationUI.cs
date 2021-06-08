using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextLocalisationUI : MonoBehaviour
{

    TextMeshProUGUI textField;
    [SerializeField]
    private string key;
    // Start is called before the first frame update
    void Start()
    {
        textField = GetComponent<TextMeshProUGUI>();
        string value = LocalisationSystem.GetLocalisedValue(key);
        textField.text = value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
