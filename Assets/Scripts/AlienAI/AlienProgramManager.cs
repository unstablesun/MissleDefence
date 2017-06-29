using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.IO;
using System;
using UnityEditor;

public class AlienProgramManager : MonoBehaviour 
{
	[SerializeField]
	public string PrefabName = "AlienProgramX";

	[SerializeField]
	public int NumEntriesUsed = 0;


	[SerializeField]
	public ProgramList.WaveEntry[] mWaves = null;


	public static AlienProgramManager Instance;

	void Awake()
	{
		Instance = this;
	}


	public void InitList(int size)
	{
		mWaves = new ProgramList.WaveEntry[size];
	}




	//This must be called at runtime
	[MenuItem("AlienData/Save Program List Prefab (Runtime)")]
	private static void SaveProgramListPrefab()
	{
		if(AlienProgramManager.Instance != null) {

			if (AlienProgramManager.Instance.NumEntriesUsed > 0) {

				AlienProgramManager.Instance.CreateAndSavePrefab ();

			}
		}
	}

	private void CreateAndSavePrefab () 
	{
		GameObject objectPrefab = new GameObject(PrefabName);

		ProgramList scriptRef = objectPrefab.AddComponent<ProgramList>() as ProgramList;

		//int length = WayPointManager.Instance.mWayPointEditList.GetLength (0);
		int length = NumEntriesUsed;
		scriptRef.InitList (length);
		for (int i = 0; i < length; i++) {

			ProgramList.WaveEntry we = AlienProgramManager.Instance.mWaves [i];
			scriptRef.AddProgramData (we, i);

		}
		scriptRef.PrefabName = PrefabName;
		scriptRef.NumEntriesUsed = NumEntriesUsed;

		UnityEngine.Object prefab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/Prefabs/Programs/"+objectPrefab.name+".prefab");
		PrefabUtility.ReplacePrefab(objectPrefab, prefab, ReplacePrefabOptions.ConnectToPrefab);
	}


}
