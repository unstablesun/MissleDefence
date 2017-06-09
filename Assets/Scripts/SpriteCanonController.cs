using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;



//------------------------------------------------------
/*
 				Sprite Canon Controller
*/
//------------------------------------------------------

public partial class SpriteCanonController : MonoBehaviour 
{

	public int objectPoolSize = 128;

	public GameObject StoragePosition;
	public GameObject FiringPosition;

	[System.Serializable]
	public class VelocityVariables
	{
		public float projectile = 100f;
		public float clustorBomb= 75f;
		public float nuke= 50f;
	}
	public VelocityVariables mVelocityVariables;

	private List <GameObject> SpriteCanonObjectList = null;
	private GameObject SpriteCanonObjectContainer;

	public static SpriteCanonController Instance;

	void Awake () 
	{
		Instance = this;

		SpriteCanonObjectList = new List<GameObject>();
	}

	void Start () 
	{
		SpriteCanonObjectContainer = GameObject.Find ("SpriteCanonObjectContainer");

		LoadCanonSprites ();
		QuerySetObjectsLoaded ();

		QuerySetObjectsDefaultPositions ();

		//test
		//QueryStarFieldObjectsSetColor ();

		Debug.Log ("SpriteCanonController Start");

	}

	//void Update() 
	//{}

	public void LaunchProjectile(Vector3 pos, SpriteCanonObject.eType type) 
	{
		Debug.Log ("SpriteCanonController LaunchProjectile");
		QueryForLaunchObject (pos, type);
	}





	//------------------------------------------------------
	/*
 							QUERIES
	*/
	//------------------------------------------------------

	GameObject QueryFindClosestToPos(Vector3 targetPos) 
	{
		GameObject closestObj = null;

		float min_distance = 1000f;

		foreach(GameObject tObj in SpriteCanonObjectList)
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



	void QuerySetObjectsDefaultPositions() 
	{
		foreach(GameObject tObj in SpriteCanonObjectList)
		{
			SpriteCanonObject objectScript = tObj.GetComponent<SpriteCanonObject> ();
			objectScript.StoragePosition = StoragePosition.transform.position;
			objectScript.FiringPosition = FiringPosition.transform.position;
		}
	}

	void QuerySetObjectsLoaded() 
	{
		foreach(GameObject tObj in SpriteCanonObjectList)
		{
			SpriteCanonObject objectScript = tObj.GetComponent<SpriteCanonObject> ();
			objectScript._State = SpriteCanonObject.eState.Loaded;
		}
	}

	void QueryForLaunchObject(Vector3 destination, SpriteCanonObject.eType type) 
	{
		foreach(GameObject tObj in SpriteCanonObjectList)
		{
			SpriteCanonObject objectScript = tObj.GetComponent<SpriteCanonObject> ();

			if (objectScript._State == SpriteCanonObject.eState.Loaded) {

				Debug.Log ("QueryForLaunchObject Object Found");

				objectScript.SetLaunchParameters (destination, type);
				break;
			}
		}
	}

	void QueryStarFieldObjectsSetColor() 
	{
		foreach(GameObject tObj in SpriteCanonObjectList)
		{
			SpriteCanonObject objectScript = tObj.GetComponent<SpriteCanonObject> ();
			objectScript.SetRandomObjectColor ();
		}
	}



	void QueryStarFieldObjectsCreateConnections() 
	{
		foreach(GameObject tObj in SpriteCanonObjectList)
		{
			SpriteCanonObject objectScript = tObj.GetComponent<SpriteCanonObject> ();

			if (objectScript._Type == SpriteCanonObject.eType.projectile) {




			}
		}
	}


}

