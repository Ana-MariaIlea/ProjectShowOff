using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
