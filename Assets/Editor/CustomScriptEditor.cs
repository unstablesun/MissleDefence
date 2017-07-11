using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CustomInspectorTest))]
public class ProgramListEditor : Editor 
{

	private SerializedObject m_object;
	public GUIStyle mystyle;
	int mWaveCount = 1;
	int mNumWaves;
	int mNumSquadsPer;
	private Color _color;
	private ColorSet _scheme;

	void OnEnable()
	{
		m_object = new SerializedObject(target);
	}
		
	public override void OnInspectorGUI()
	{
		//base.OnInspectorGUI (); //this causes target to draw it's controls as well.  creating dups

		mWaveCount = 1;
		m_object.Update();

		CustomInspectorTest myTarget = (CustomInspectorTest)target;

		mystyle = new GUIStyle ();


		var prop = m_object.FindProperty("ColorScheme");
		EditorGUILayout.PropertyField(prop, true);
		_scheme = (ColorSet)myTarget.ColorScheme as ColorSet;


		GUI.color = _scheme.SixthColor;


		myTarget.eAttackModules = (CustomInspectorTest.AttackModule)EditorGUILayout.EnumPopup("AttackModule", myTarget.eAttackModules);

		myTarget.HitPoints = EditorGUILayout.Slider (myTarget.HitPoints, 1f, 1000f);
		Color curveColor = new Color(1.0f, 0.5f, 0.75f);
		Rect curveRect = new Rect(0, 0, 10, 10);
		myTarget.curveX = EditorGUILayout.CurveField("Variable X", myTarget.curveX, curveColor, curveRect);


		myTarget.NumWaves = EditorGUILayout.IntField("Num Waves", myTarget.NumWaves);
		mNumWaves = myTarget.NumWaves;

		myTarget.NumSquadsPer = EditorGUILayout.IntField("Num Squads Per", myTarget.NumSquadsPer);
		mNumSquadsPer = myTarget.NumSquadsPer;

		GUI.color = _scheme.SeventhColor;

		mystyle.fontStyle = FontStyle.Bold;
		mystyle.alignment = TextAnchor.MiddleCenter;

		_color = _scheme.QuinaryColor;
		mystyle.normal.textColor = new Color(_color.r, _color.g, _color.b, _color.a);

		_color = _scheme.PrimaryColour;
		mystyle.normal.background = MakeTex(600, 1, new Color(_color.r, _color.g, _color.b, _color.a));

		GUILayout.Space (10);
		GUILayout.Label ("---------------- PROGRAM DATA ----------------", mystyle);
		GUILayout.Space (10);

		if (GUILayout.Button ("Process Data")) {
			//ProcessData ();

			myTarget.ProcessData ();
		}
			
		GUI.color = Color.white;

		for (int w = 1; w < mNumWaves + 1; w++) {

			AddHeader (w);
			AddNumSquads (mNumSquadsPer);

		}
			
		m_object.ApplyModifiedProperties();

	}

	void ProcessData()
	{
		Debug.Log ("Process Data");
	}


	private void AddHeader(int WaveIndex)
	{
		mystyle.alignment = TextAnchor.MiddleCenter;

		_color = _scheme.QuinaryColor;
		mystyle.normal.textColor = new Color(_color.r, _color.g, _color.b, _color.a);

		_color = _scheme.PrimaryColour;
		mystyle.normal.background = MakeTex(600, 1, new Color(_color.r, _color.g, _color.b, _color.a));

		GUILayout.Space (5);
		GUILayout.Label ("WAVE " + WaveIndex.ToString(), mystyle);
		GUILayout.Space (10);

		mystyle.alignment = TextAnchor.MiddleLeft;

		_color = _scheme.QuaternaryColor;
		mystyle.normal.background = MakeTex(600, 1, new Color(_color.r, _color.g, _color.b, _color.a));
	}


	private void AddNumSquads(int numSquads)
	{
		for(int i = 0; i < numSquads; i++) {
			_color = _scheme.TertiaryColour;
			mystyle.normal.textColor = new Color(_color.r, _color.g, _color.b, _color.a);
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