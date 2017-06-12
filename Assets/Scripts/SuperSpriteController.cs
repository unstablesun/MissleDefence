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

public partial class SuperSpriteController : MonoBehaviour 
{

	public GameObject CenterPoint;//default position


	[System.Serializable]
	public class FieldVariables
	{
		public float separationSphereRadiusSquared = 8f;

		public int numSprites = 32;
		public float spaceDeltaX = 32f;
		public float spaceDeltaY= 32f;
	}
	public FieldVariables mFieldVariables;

	private List <GameObject> SuperSpriteObjectList = null;



	private GameObject SuperSpriteObjectContainer;

	private const int objectPoolSize = 32;


	public static SuperSpriteController Instance;

	void Awake () 
	{
		Instance = this;

		SuperSpriteObjectList = new List<GameObject>();
	}

	void Start () 
	{
		SuperSpriteObjectContainer = GameObject.Find ("SuperSpriteObjectContainer");


		LoadSuperSprites ();

		QuerySetObjectsLoaded ();
		//QueryStarFieldQuadSetVertexColors ();

		QueryStarFieldObjectsSetColor ();
	}


	public void LaunchAttackShip(Vector3 pos, SpriteCanonObject.eType type) 
	{
		
	}


	void QuerySetObjectsLoaded() 
	{
		foreach(GameObject tObj in SuperSpriteObjectList)
		{
			SuperSpriteObject objectScript = tObj.GetComponent<SuperSpriteObject> ();
			objectScript._State = SuperSpriteObject.eState.Loaded;
		}
	}

	void QueryForLaunchObject(Vector3 destination, SpriteCanonObject.eType type) 
	{
		foreach(GameObject tObj in SuperSpriteObjectList)
		{
			SuperSpriteObject objectScript = tObj.GetComponent<SuperSpriteObject> ();

			if (objectScript._State == SuperSpriteObject.eState.Loaded) {

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

		foreach(GameObject tObj in SuperSpriteObjectList)
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




	void QueryStarFieldQuadSetVertexColors() 
	{
		foreach(GameObject tObj in SuperSpriteObjectList)
		{
			SuperSpriteObject starFieldQuadScript = tObj.GetComponent<SuperSpriteObject> ();
			starFieldQuadScript.SetVertexColors (0);
		}
	}

	void QueryStarFieldObjectsSetColor() 
	{
		foreach(GameObject tObj in SuperSpriteObjectList)
		{
			SuperSpriteObject starFieldObjectScript = tObj.GetComponent<SuperSpriteObject> ();

			starFieldObjectScript.SetObjectColor (0);

		}
	}



	void QueryStarFieldObjectsCreateConnections() 
	{
		foreach(GameObject tObj in SuperSpriteObjectList)
		{
			//SuperSpriteObject starFieldObjectScript = tObj.GetComponent<SuperSpriteObject> ();

		}
	}


}

