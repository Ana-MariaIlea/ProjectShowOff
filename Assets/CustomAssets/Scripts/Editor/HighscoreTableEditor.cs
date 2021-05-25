using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(HighscoreTable))]
public class HighscoreTableEditor : Editor
{
	public override void OnInspectorGUI()
	{
		HighscoreTable table = (HighscoreTable)target;


		if (GUILayout.Button("Reset scores"))
		{
			table.ResetScores();
		}

		DrawDefaultInspector();
	}
}
