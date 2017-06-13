using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.IO;
using System;

public class WayPointManager : MonoBehaviour 
{
	public static WayPointManager Instance = null;

	void Awake () 
	{
		Instance = this;
	}

	//void Start() 
	//{}

	//void Update() 
	//{}

	[SerializeField]
	public string PrefabName = "WayPointX";

	[SerializeField]
	public int NumPointsUsed = 0;

	[SerializeField]
	public GameObject[] mWayPointStartPos = null;


	[MenuItem("WayPoints/Save WayPoint Prefab")]
	private static void SaveWayPointPrefab()
	{
		if(WayPointManager.Instance != null) {

			if (WayPointManager.Instance.mWayPointStartPos != null) {

				WayPointManager.Instance.CreateAndSavePrefab ();

			}
		}
	}

	[MenuItem("WayPoints/Load Current Prefabs")]
	private static void LoadCurrentPrefabs () 
	{

		DirectoryInfo dirInfo = new DirectoryInfo("Assets/Resources/Prefabs/WayPoints/");
		FileInfo[] fileInf = dirInfo.GetFiles("*.prefab");
		foreach (FileInfo file in fileInf) {

			//Debug.Log("file.Name = " + file.Name);

			UnityEngine.Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Resources/Prefabs/WayPoints/" + file.Name, typeof(GameObject));
			PrefabUtility.InstantiatePrefab(prefab);

			/*
			GameObject WayPointContainer = GameObject.Find ("LoadedWayPoints");
			Debug.Log ("WayPointContainer = " + WayPointContainer.name);

			if (WayPointContainer != null) {
				prefab.transform.parent = WayPointContainer.transform;
			}
			*/

		}
	}

	[MenuItem("WayPoints/Set Current WayPoint")]
	private static void SetCurrentWatPoint () 
	{

		GameObject selectedGameObject = Selection.activeGameObject;
		Debug.Log ("selectedGameObject = " + selectedGameObject.name);

		//this is the WayPointList
		WayPointList wpList = selectedGameObject.GetComponent<WayPointList> ();


		//this is the WayPointManager

		GameObject WayPointManagerObject = GameObject.Find ("WayPointManager");
		WayPointManager wpManager = WayPointManagerObject.GetComponent<WayPointManager> ();
		string  pName = wpManager.PrefabName;

		Debug.Log ("wpManager pName = " + pName);

	}


	private void CreateAndSavePrefab () 
	{
		GameObject objectPrefab = new GameObject(PrefabName);

		WayPointList scriptRef = objectPrefab.AddComponent<WayPointList>() as WayPointList;

		int length = WayPointManager.Instance.mWayPointStartPos.GetLength (0);
		scriptRef.InitList (length);
		for (int i = 0; i < length; i++) {

			Vector3 vec = WayPointManager.Instance.mWayPointStartPos [i].transform.position;

			scriptRef.AddVector3 (vec, i);

			Debug.Log ("vec.x = " + vec.x + " vec.y = " + vec.y);

		}
		scriptRef.NumPointsUsed = NumPointsUsed;
			
		UnityEngine.Object prefab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/Prefabs/WayPoints/"+objectPrefab.name+".prefab");
		PrefabUtility.ReplacePrefab(objectPrefab, prefab, ReplacePrefabOptions.ConnectToPrefab);
	}

}

