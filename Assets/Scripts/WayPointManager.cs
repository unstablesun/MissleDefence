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
	public bool HasData = false;

	[SerializeField]
	public int MaxPoints = 16;

	[SerializeField]
	public GameObject[] mWayPointEditList = null;

	[SerializeField]
	public WayPoint[] mWayPointEditStructs = null;


	//This must be called at runtime
	[MenuItem("WayPoints/Save WayPoint Prefab (Runtime)")]
	private static void SaveWayPointPrefab()
	{
		if(WayPointManager.Instance != null) {

			if (WayPointManager.Instance.mWayPointEditList != null) {

				WayPointManager.Instance.CreateAndSavePrefab ();

			}
		}
	}

	private void CreateAndSavePrefab () 
	{
		GameObject objectPrefab = new GameObject(PrefabName);

		WayPointList scriptRef = objectPrefab.AddComponent<WayPointList>() as WayPointList;

		//int length = WayPointManager.Instance.mWayPointEditList.GetLength (0);
		int length = NumPointsUsed;
		scriptRef.InitList (length);
		scriptRef.HasData = HasData;
		for (int i = 0; i < length; i++) {

			Vector3 vec = WayPointManager.Instance.mWayPointEditList [i].transform.position;
			scriptRef.AddVector3 (vec, i);

			if (HasData == true) {
				WayPoint wp = WayPointManager.Instance.mWayPointEditStructs [i];
				scriptRef.AddWayPointData (wp, i);
			}

		}
		scriptRef.PrefabName = PrefabName;
		scriptRef.NumPointsUsed = NumPointsUsed;

		UnityEngine.Object prefab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/Prefabs/WayPoints/"+objectPrefab.name+".prefab");
		PrefabUtility.ReplacePrefab(objectPrefab, prefab, ReplacePrefabOptions.ConnectToPrefab);
	}



	//This can be called when editing
	[MenuItem("WayPoints/Load WayPoint Prefabs (Editor)")]
	private static void LoadWayPointPrefabs () 
	{

		DirectoryInfo dirInfo = new DirectoryInfo("Assets/Resources/Prefabs/WayPoints/");
		FileInfo[] fileInf = dirInfo.GetFiles("*.prefab");
		foreach (FileInfo file in fileInf) {

			UnityEngine.Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Resources/Prefabs/WayPoints/" + file.Name, typeof(GameObject));
			PrefabUtility.InstantiatePrefab(prefab);

		}
	}

	//This must be called at runtime
	[MenuItem("WayPoints/Edit Selected WayPoint (Runtime)")]
	private static void EditSelectedWayPoint () 
	{

		GameObject selectedGameObject = Selection.activeGameObject;
		Debug.Log ("selectedGameObject = " + selectedGameObject.name);

		//this is the WayPointList
		WayPointList wpList = selectedGameObject.GetComponent<WayPointList> ();


		//this is the WayPointManager
		GameObject WayPointManagerObject = GameObject.Find ("WayPointManager");
		WayPointManager wpManager = WayPointManagerObject.GetComponent<WayPointManager> ();
		string  pName = wpManager.PrefabName;
		int numPoints = wpList.NumPointsUsed;
		bool HasData = wpList.HasData;

		wpManager.PrefabName = wpList.PrefabName;
		wpManager.NumPointsUsed = wpList.NumPointsUsed;
		wpManager.HasData = wpList.HasData;

		Debug.Log ("EditSelectedWayPoint : numPoints = " + numPoints);
		for (int i = 0; i < numPoints; i++) {

			Vector3 vec = wpList.GetVector3AtIndex (i);
			WayPointManager.Instance.mWayPointEditList [i].transform.position = new Vector3(vec.x, vec.y, vec.z);

			if (HasData == true) {
				WayPoint wp = wpList.GetWayPointDataAtIndex (i);
				WayPointManager.Instance.mWayPointEditStructs [i] = wp;
			}

		}
			
		Debug.Log ("wpManager pName = " + pName);

	}


}

