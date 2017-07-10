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
	int mWaveCount = 1;

	public override void OnInspectorGUI()
	{
		//base.OnInspectorGUI (); //this causes type to draw it's controls to.  creating dups
		mWaveCount = 1;
		m_object.Update();


		mystyle = new GUIStyle ();

		mystyle.normal.textColor = new Color(0.15f, 0.15f, 0.15f);
		mystyle.normal.background = MakeTex(600, 1, new Color(0.15f, 0.65f, 0.95f, 1.0f));

		mystyle.fontStyle = FontStyle.Bold;
		mystyle.alignment = TextAnchor.MiddleCenter;


		GUILayout.Space (10);

		GUILayout.Label ("WAVE DATA", mystyle);

		GUILayout.Space (10);

		if (GUILayout.Button ("Process Data")) {
			AddSomething ();
		}


		CustomInspectorTest myTarget = (CustomInspectorTest)target;

		//GUI.contentColor = Color.white;

		GUI.color = Color.cyan;

		//myTarget.NumEntriesUsed = EditorGUILayout.IntField("Experience", myTarget.NumEntriesUsed);

		GUI.color = new Color(0.95f, 0.35f, 0.45f);

		//GUI.
		//EditorGUILayout.Vector3Field ("V3a", new Vector3(0,1,2));
		//EditorGUILayout.Vector3Field ("V3b", new Vector3(0,1,2));
		//EditorGUILayout.Vector3Field ("V3c", new Vector3(0,1,2));

		//EditorGUILayout.LabelField("Level", myTarget.Level.ToString());

		GUI.color = Color.white;


		//var prop = m_object.FindProperty("mColorContainer");
		//EditorGUILayout.PropertyField(prop, true);


		//----------------------------------------------------------------------------
		/*
		mystyle.normal.textColor = new Color(0.15f, 0.15f, 0.15f);
		mystyle.normal.background = MakeTex(600, 1, new Color(0.2f, 0.6f, 0.4f, 1.0f));
		GUILayout.Space (5);
		GUILayout.Label ("AREA 1", mystyle);
		GUILayout.Space (10);


		mystyle.alignment = TextAnchor.MiddleLeft;
		mystyle.normal.background = MakeTex(600, 1, new Color(0.2f, 0.2f, 0.2f, 1.0f));

		mystyle.normal.textColor = new Color(0.65f, 1.0f, 0.95f);
		GUILayout.Label ("    WAVE 1", mystyle);
		var prop = m_object.FindProperty("ProgramEntryWave1");
		EditorGUILayout.PropertyField(prop, true);

		mystyle.normal.textColor = new Color(0.65f, 1.0f, 0.95f);
		GUILayout.Label ("    WAVE 2", mystyle);
		prop = m_object.FindProperty("ProgramEntryWave2");
		EditorGUILayout.PropertyField(prop, true);

		mystyle.normal.textColor = new Color(0.65f, 1.0f, 0.9f);
		GUILayout.Label ("    WAVE 3", mystyle);
		prop = m_object.FindProperty("ProgramEntryWave3");
		EditorGUILayout.PropertyField(prop, true);

		mystyle.normal.textColor = new Color(0.65f, 1.0f, 0.85f);
		GUILayout.Label ("    WAVE 4", mystyle);
		prop = m_object.FindProperty("ProgramEntryWave4");
		EditorGUILayout.PropertyField(prop, true);

		mystyle.normal.textColor = new Color(0.65f, 1.0f, 0.8f);
		GUILayout.Label ("    WAVE 5", mystyle);
		prop = m_object.FindProperty("ProgramEntryWave5");
		EditorGUILayout.PropertyField(prop, true);

		GUILayout.Space (5);

		//----------------------------------------------------------------------------

		mystyle.alignment = TextAnchor.MiddleCenter;
		mystyle.normal.textColor = new Color(0.15f, 0.15f, 0.15f);
		mystyle.normal.background = MakeTex(600, 1, new Color(0.2f, 0.6f, 0.4f, 1.0f));
		GUILayout.Space (5);
		GUILayout.Label ("AREA 2", mystyle);
		GUILayout.Space (10);

		mystyle.alignment = TextAnchor.MiddleLeft;
		mystyle.normal.background = MakeTex(600, 1, new Color(0.2f, 0.2f, 0.2f, 1.0f));

		mystyle.normal.textColor = new Color(0.55f, 1.0f, 0.8f);
		GUILayout.Label ("    WAVE 6", mystyle);
		prop = m_object.FindProperty("ProgramEntryWave6");
		EditorGUILayout.PropertyField(prop, true);

		mystyle.normal.textColor = new Color(0.50f, 1.0f, 0.8f);
		GUILayout.Label ("    WAVE 7", mystyle);
		prop = m_object.FindProperty("ProgramEntryWave7");
		EditorGUILayout.PropertyField(prop, true);

		mystyle.normal.textColor = new Color(0.45f, 1.0f, 0.8f);
		GUILayout.Label ("    WAVE 8", mystyle);
		prop = m_object.FindProperty("ProgramEntryWave8");
		EditorGUILayout.PropertyField(prop, true);

		mystyle.normal.textColor = new Color(0.4f, 1.0f, 0.8f);
		GUILayout.Label ("    WAVE 9", mystyle);
		prop = m_object.FindProperty("ProgramEntryWave9");
		EditorGUILayout.PropertyField(prop, true);

		mystyle.normal.textColor = new Color(0.35f, 1.0f, 0.8f);
		GUILayout.Label ("    WAVE 10", mystyle);
		prop = m_object.FindProperty("ProgramEntryWave10");
		EditorGUILayout.PropertyField(prop, true);
		*/


		AddHeader(1);
		AddNumSquads(5);

		AddHeader(2);
		AddNumSquads(5);

		AddHeader(3);
		AddNumSquads(5);

		AddHeader(4);
		AddNumSquads(5);

		m_object.ApplyModifiedProperties();

	}

	void AddSomething()
	{
		Debug.Log ("Adding Something");
	}

	private void AddHeader(int WaveIndex)
	{
		mystyle.alignment = TextAnchor.MiddleCenter;
		mystyle.normal.textColor = new Color(0.15f, 0.15f, 0.15f);
		mystyle.normal.background = MakeTex(600, 1, new Color(0.2f, 0.6f, 0.4f, 1.0f));
		GUILayout.Space (5);
		GUILayout.Label ("WAVE " + WaveIndex.ToString(), mystyle);
		GUILayout.Space (10);

		mystyle.alignment = TextAnchor.MiddleLeft;
		mystyle.normal.background = MakeTex(600, 1, new Color(0.2f, 0.2f, 0.2f, 1.0f));
	}


	private void AddNumSquads(int numSquads)
	{
		for(int i = 0; i < numSquads; i++) {
			mystyle.normal.textColor = new Color(0.65f, 1.0f, 0.95f);
			GUILayout.Label ("    SQUAD " + mWaveCount.ToString(), mystyle);
			string lookUp = "ProgramEntryWave" + mWaveCount.ToString();
			var prop = m_object.FindProperty(lookUp);
			EditorGUILayout.PropertyField(prop, true);

			mWaveCount++;
		}
	}






	private Texture2D MakeTex(int width, int height, Color col)
	{
		Color[] pix = new Color[width*height];

		for(int i = 0; i < pix.Length; i++)
			pix[i] = col;

		Texture2D result = new Texture2D(width, height);
		result.SetPixels(pix);
		result.Apply();

		return result;
	}

}