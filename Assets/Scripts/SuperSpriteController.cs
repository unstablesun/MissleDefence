using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;



//------------------------------------------------------
/*
 				Star Field Controller
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


	//private List <Vector3> ObjectVectorList = null;

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


		//QueryStarFieldQuadSetVertexColors ();

		QueryStarFieldObjectsSetColor ();
	}


	bool QuerySuperSpriteObjectsForOverlap(GameObject currentObj) 
	{
		Vector3 pos = currentObj.transform.position;

		bool overlap = false;

		foreach(GameObject tObj in SuperSpriteObjectList)
		{
			Vector3 _pos = tObj.transform.position;

			float distance = Vector3.Distance (pos, _pos);

			if (distance < mFieldVariables.separationSphereRadiusSquared) {
				overlap = true;
				break;
			}
		}

		return overlap;
	}

	GameObject QueryFindClosestToObj(GameObject currentObj) 
	{
		Vector3 pos = currentObj.transform.position;

		SuperSpriteObject starFieldObjectScript = currentObj.GetComponent<SuperSpriteObject> ();
		int ID = starFieldObjectScript.ID;
		bool checkSelf = true;
		GameObject closestObj = null;

		float min_distance = 1000f;

		foreach(GameObject tObj in SuperSpriteObjectList)
		{
			Vector3 _pos = tObj.transform.position;

			if (checkSelf == true) {
				starFieldObjectScript = currentObj.GetComponent<SuperSpriteObject> ();
				int id = starFieldObjectScript.ID;

				if (id == ID) {
					checkSelf = false;
					continue;
				}
			}

			float distance = Vector3.Distance (pos, _pos);

			if (distance < min_distance) {
				min_distance = distance;
				closestObj = tObj;
			}
		}

		return closestObj;
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

			if (starFieldObjectScript._type == SuperSpriteObject.eType.quad) {
				
				starFieldObjectScript.SetObjectColor (0);
			}
		}
	}



	void QueryStarFieldObjectsCreateConnections() 
	{
		foreach(GameObject tObj in SuperSpriteObjectList)
		{
			SuperSpriteObject starFieldObjectScript = tObj.GetComponent<SuperSpriteObject> ();

			if (starFieldObjectScript._type == SuperSpriteObject.eType.cube) {




			}
		}
	}


}

