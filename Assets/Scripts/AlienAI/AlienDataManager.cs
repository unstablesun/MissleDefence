using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class AlienDataManager : MonoBehaviour 
{
	[SerializeField]
	public AlienModuleData[] mAlienModuleData = null;


	public static AlienDataManager Instance;

	void Awake()
	{
		Instance = this;
	}

	//void Start() 
	//{}

	//void Update() 
	//{}


	[MenuItem("AlienData/Save Module Data")]
	private static void SaveWayPointPrefab()
	{
		if(AlienDataManager.Instance != null) {

			if (AlienDataManager.Instance.mAlienModuleData != null) {

				AlienDataManager.Instance.CreateAndSavePrefab ();

			}
		}
	}

	private void CreateAndSavePrefab () 
	{
		foreach (AlienModuleData module in mAlienModuleData) {

			GameObject objectPrefab = new GameObject (module.PrefabName);

			AlienModuleContainer scriptRef = objectPrefab.AddComponent<AlienModuleContainer> () as AlienModuleContainer;

			scriptRef.mData = module;

			UnityEngine.Object prefab = PrefabUtility.CreateEmptyPrefab ("Assets/Resources/Prefabs/AlienModuleData/" + objectPrefab.name + ".prefab");
			PrefabUtility.ReplacePrefab (objectPrefab, prefab, ReplacePrefabOptions.ConnectToPrefab);

		}

	}
		




}