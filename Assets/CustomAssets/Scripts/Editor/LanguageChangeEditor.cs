using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LanguageChangeUI))]
public class LanguageChangeEditor : Editor
{
	public override void OnInspectorGUI()
	{
		LanguageChangeUI language = (LanguageChangeUI)target;


		if (GUILayout.Button("English"))
		{
			language.ChangeLanguageToEnglish();
		}

		if (GUILayout.Button("Dutch"))
		{
			language.ChangeLanguageToDucth();
		}

		DrawDefaultInspector();
	}
}
