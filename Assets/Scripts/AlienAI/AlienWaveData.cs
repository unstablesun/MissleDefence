using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class AlienWaveData : MonoBehaviour 
{
	public ColorSet ColorScheme;

	public string PrefabName;

	public int NumWaves;

	public int NumSquadsPer;

	public ProgramEntry ProgramEntryWave1;
	public ProgramEntry ProgramEntryWave2;
	public ProgramEntry ProgramEntryWave3;
	public ProgramEntry ProgramEntryWave4;
	public ProgramEntry ProgramEntryWave5;

	public ProgramEntry ProgramEntryWave6;
	public ProgramEntry ProgramEntryWave7;
	public ProgramEntry ProgramEntryWave8;
	public ProgramEntry ProgramEntryWave9;
	public ProgramEntry ProgramEntryWave10;

	public ProgramEntry ProgramEntryWave11;
	public ProgramEntry ProgramEntryWave12;
	public ProgramEntry ProgramEntryWave13;
	public ProgramEntry ProgramEntryWave14;
	public ProgramEntry ProgramEntryWave15;

	public ProgramEntry ProgramEntryWave16;
	public ProgramEntry ProgramEntryWave17;
	public ProgramEntry ProgramEntryWave18;
	public ProgramEntry ProgramEntryWave19;
	public ProgramEntry ProgramEntryWave20;

	public ProgramEntry ProgramEntryWave21;
	public ProgramEntry ProgramEntryWave22;
	public ProgramEntry ProgramEntryWave23;
	public ProgramEntry ProgramEntryWave24;
	public ProgramEntry ProgramEntryWave25;

	public ProgramEntry ProgramEntryWave26;
	public ProgramEntry ProgramEntryWave27;
	public ProgramEntry ProgramEntryWave28;
	public ProgramEntry ProgramEntryWave29;
	public ProgramEntry ProgramEntryWave30;

	public ProgramEntry ProgramEntryWave31;
	public ProgramEntry ProgramEntryWave32;
	public ProgramEntry ProgramEntryWave33;
	public ProgramEntry ProgramEntryWave34;
	public ProgramEntry ProgramEntryWave35;

	public ProgramEntry ProgramEntryWave36;
	public ProgramEntry ProgramEntryWave37;
	public ProgramEntry ProgramEntryWave38;
	public ProgramEntry ProgramEntryWave39;
	public ProgramEntry ProgramEntryWave40;

	public ProgramEntry ProgramEntryWave41;
	public ProgramEntry ProgramEntryWave42;
	public ProgramEntry ProgramEntryWave43;
	public ProgramEntry ProgramEntryWave44;
	public ProgramEntry ProgramEntryWave45;

	public ProgramEntry ProgramEntryWave46;
	public ProgramEntry ProgramEntryWave47;
	public ProgramEntry ProgramEntryWave48;
	public ProgramEntry ProgramEntryWave49;
	public ProgramEntry ProgramEntryWave50;



	void Awake()
	{
	}




	public void ProcessData()
	{
		int size = NumWaves * NumSquadsPer;
		CreateAndSavePrefab (size);
	}


	private void CreateAndSavePrefab (int size) 
	{
		ProgramEntry[] mProgramEntries = 
		{
			ProgramEntryWave1,
			ProgramEntryWave2,
			ProgramEntryWave3,
			ProgramEntryWave4,
			ProgramEntryWave5,
			ProgramEntryWave6,
			ProgramEntryWave7,
			ProgramEntryWave8,
			ProgramEntryWave9,
			ProgramEntryWave10,

			ProgramEntryWave11,
			ProgramEntryWave12,
			ProgramEntryWave13,
			ProgramEntryWave14,
			ProgramEntryWave15,
			ProgramEntryWave16,
			ProgramEntryWave17,
			ProgramEntryWave18,
			ProgramEntryWave19,
			ProgramEntryWave20,

			ProgramEntryWave21,
			ProgramEntryWave22,
			ProgramEntryWave23,
			ProgramEntryWave24,
			ProgramEntryWave25,
			ProgramEntryWave26,
			ProgramEntryWave27,
			ProgramEntryWave28,
			ProgramEntryWave29,
			ProgramEntryWave30,

			ProgramEntryWave31,
			ProgramEntryWave32,
			ProgramEntryWave33,
			ProgramEntryWave34,
			ProgramEntryWave35,
			ProgramEntryWave36,
			ProgramEntryWave37,
			ProgramEntryWave38,
			ProgramEntryWave39,
			ProgramEntryWave40,

			ProgramEntryWave41,
			ProgramEntryWave42,
			ProgramEntryWave43,
			ProgramEntryWave44,
			ProgramEntryWave45,
			ProgramEntryWave46,
			ProgramEntryWave47,
			ProgramEntryWave48,
			ProgramEntryWave49,
			ProgramEntryWave50
		};

		GameObject objectPrefab = new GameObject(PrefabName);

		AlienWave scriptRef = objectPrefab.AddComponent<AlienWave>() as AlienWave;

		scriptRef.InitProgramEntries (size);

		for (int s = 0; s < size; s++) {

			scriptRef.AddProgramEntry(mProgramEntries[s]);

		}
			
		scriptRef.PrefabName = PrefabName;
		scriptRef.NumWaves = NumWaves;
		scriptRef.NumSquadsPer = NumSquadsPer;

		UnityEngine.Object prefab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/Prefabs/Programs/"+objectPrefab.name+".prefab");
		PrefabUtility.ReplacePrefab(objectPrefab, prefab, ReplacePrefabOptions.ConnectToPrefab);
	}
		
}

