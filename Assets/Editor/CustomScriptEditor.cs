using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CustomInspectorTest))]
public class ProgramListEditor : Editor 
{


	private SerializedObject m_object;

	void OnEnable()
	{
		m_object = new SerializedObject(target);

	}

	public GUIStyle mystyle;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI ();

		m_object.Update();


		mystyle = new GUIStyle ();

		mystyle.normal.textColor = Color.green; 
		GUILayout.Label ("My Custom Control", mystyle);

		GUILayout.Space (20);

		if (GUILayout.Button ("Add")) {
			AddSomething ();
		}


		CustomInspectorTest myTarget = (CustomInspectorTest)target;

		//GUI.contentColor = Color.white;

		GUI.color = Color.cyan;

		myTarget.NumEntriesUsed = EditorGUILayout.IntField("Experience", myTarget.NumEntriesUsed);

		GUI.color = new Color(0.25f, 0.35f, 0.45f);

		//GUI.
		EditorGUILayout.Vector3Field ("V3", new Vector3(0,0,0));

		//EditorGUILayout.LabelField("Level", myTarget.Level.ToString());

		GUI.color = Color.white;


		var prop = m_object.FindProperty("mColorContainer");
		EditorGUILayout.PropertyField(prop, true);


		m_object.ApplyModifiedProperties();

	}

	void AddSomething()
	{
		Debug.Log ("Adding Something");
	}
}