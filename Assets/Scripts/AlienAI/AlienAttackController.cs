using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;



//------------------------------------------------------
/*
 				Super Sprite Controller
*/
//------------------------------------------------------

public partial class AlienAttackController : MonoBehaviour 
{

	public GameObject StoragePoint;

	private List <GameObject> AlienAttackObjectList = null;

	private GameObject AlienAttackObjectContainer;

	public static AlienAttackController Instance;

	void Awake () 
	{
		Instance = this;

		AlienAttackObjectList = new List<GameObject>();
	}

	void Start () 
	{
		AlienAttackObjectContainer = GameObject.Find ("AlienAttackObjectContainer");


		LoadAttackManifest ();

		QuerySetObjectsLoaded ();

	}

	private void LoadAttackManifest()
	{
		string manifestName = "ManifestListArea1";

		GameObject _meObj = Instantiate (Resources.Load ("Prefabs/Manifests/" + manifestName, typeof(GameObject))) as GameObject;

		ManifestList manifestScript = _meObj.GetComponent<ManifestList> ();

		int numEntries = manifestScript.NumEntriesUsed;
		Debug.Log ("LoadAttackManifest : numEntries = " + numEntries.ToString());

		int runningIndex = 0;

		for (int e = 0; e < numEntries; e++) {

			ManifestEntry me = manifestScript.GetManifestEntryAtIndex (e);

			int numToLoad = me.NumToLoad;
			string alienPrefabName = me.PrefabName;
			WayPointList startingPoints = me.StartingPoints;

			GameObject _moduleDataObj = Instantiate (Resources.Load ("Prefabs/AlienModuleData/" + alienPrefabName, typeof(GameObject))) as GameObject;
			AlienModuleContainer amc = _moduleDataObj.GetComponent<AlienModuleContainer> ();
			AlienModuleData amd = amc.mData;

			for (int i = 0; i < numToLoad; i++) {

				GameObject _aaObj = Instantiate (Resources.Load ("Prefabs/AlienAttackObject", typeof(GameObject))) as GameObject;

				if (_aaObj != null) {

					if (AlienAttackObjectContainer != null) {
						_aaObj.transform.parent = AlienAttackObjectContainer.transform;
					}
					_aaObj.name = "attackObj" + runningIndex.ToString ();


					Vector3 startingVec = startingPoints.GetVector3AtIndex (i);
					//Debug.Log("vec = " + startingVec.x.ToString() + " "  + startingVec.y.ToString() + " " + startingVec.z.ToString());

					AlienAttackObject objectScript = _aaObj.GetComponent<AlienAttackObject> ();
					objectScript.StoragePosition = StoragePoint.transform.position;
					objectScript.StartPosition = startingVec;
					objectScript.AttachModuleData (amd);
					objectScript.FixUp ();

					//temp test
					objectScript.SetBaseSpriteScale (0.3f, 0.3f);

					AlienAttackObjectList.Add (_aaObj);

					runningIndex++;

				}

			}
				

		}
			
	}



	public void LaunchAttackShip(Vector3 pos, SpriteCanonObject.eType type) 
	{

	}


	void QuerySetObjectsLoaded() 
	{
		foreach(GameObject tObj in AlienAttackObjectList)
		{
			AlienAttackObject objectScript = tObj.GetComponent<AlienAttackObject> ();
			objectScript._State = AlienAttackObject.eState.Ready;
		}
	}







	/*
	void QueryForLaunchObject(Vector3 destination, SpriteCanonObject.eType type) 
	{
		foreach(GameObject tObj in AlienAttackObjectList)
		{
			AlienAttackObject objectScript = tObj.GetComponent<AlienAttackObject> ();

			if (objectScript._State == AlienAttackObject.eState.Ready) {

				//Debug.Log ("QueryForLaunchObject Object Found");

				//objectScript.SetLaunchParameters (destination, type);
				break;
			}
		}
	}



	GameObject QueryFindClosestToPos(Vector3 targetPos) 
	{
		GameObject closestObj = null;

		float min_distance = 1000f;

		foreach(GameObject tObj in AlienAttackObjectList)
		{
			Vector3 _pos = tObj.transform.position;

			float distance = Vector3.Distance (targetPos, _pos);

			if (distance < min_distance) {
				min_distance = distance;
				closestObj = tObj;
			}
		}

		return closestObj;
	}
	*/
		
	/*
	for (int t = 0; t < objectPoolSize; t++) {

		GameObject _sfObj = Instantiate (Resources.Load ("Prefabs/SuperSpriteObject", typeof(GameObject))) as GameObject;

		if (_sfObj != null) {

			if (AlienAttackObjectContainer != null) {
				_sfObj.transform.parent = AlienAttackObjectContainer.transform;
			}
			_sfObj.name = "attackObj" + t.ToString ();

			//float spacePosX = (float)UnityEngine.Random.Range (-mFieldVariables.spaceDeltaX, mFieldVariables.spaceDeltaX);
			//float spacePosY = (float)UnityEngine.Random.Range (-mFieldVariables.spaceDeltaY, mFieldVariables.spaceDeltaY);

			_sfObj.transform.position = new Vector2 (StoragePoint.transform.position.x, StoragePoint.transform.position.y);


			AlienAttackObject objectScript = _sfObj.GetComponent<AlienAttackObject> ();
			objectScript.ID = t;
			objectScript.velocity = 100f;
			objectScript.SetBaseSpriteScale (0.25f, 0.25f);


			AlienAttackObjectList.Add (_sfObj);

		} else {

			Debug.Log ("Couldn't load attack sprite prefab");
		}


	}
	*/

	/*
	[System.Serializable]
	public class FieldVariables
	{
		public float separationSphereRadiusSquared = 8f;

		public int numSprites = 32;
		public float spaceDeltaX = 32f;
		public float spaceDeltaY= 32f;
	}
	public FieldVariables mFieldVariables;
	*/



}

