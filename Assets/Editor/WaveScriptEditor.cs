using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CustomInspectorTest))]
public class ProgramListEditor : Editor 
{

	void OnEnable()
	{

	}


	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI ();



		GUILayout.Label ("My Custom Control");

		GUILayout.Space (20);

		if (GUILayout.Button ("Add")) {
			AddSomething ();
		}


		CustomInspectorTest myTarget = (CustomInspectorTest)target;

		GUI.color = Color.cyan;

		myTarget.NumEntriesUsed = EditorGUILayout.IntField("Experience", myTarget.NumEntriesUsed);

		//EditorGUILayout.LabelField("Level", myTarget.Level.ToString());

		GUI.color = Color.white;



	}

	void AddSomething()
	{
		Debug.Log ("Adding Something");
	}
}