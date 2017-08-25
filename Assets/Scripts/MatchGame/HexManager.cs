using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexManager : MonoBehaviour 
{
	public int objectPoolSize = 128;
	public int HexGridWidth = 8;
	public int HexGridHeight = 8;

	public GameObject StoragePosition;
	public GameObject StartGridPosition;


	[HideInInspector]
	public List <GameObject> HexObjectList = null;
	private GameObject HexObjectContainer;

	public static HexManager Instance;

	private GameObject _nullObj = null;

	void Awake () 
	{
		Instance = this;

		HexObjectList = new List<GameObject>();
	}

	void Start () 
	{
		HexObjectContainer = GameObject.Find ("HexObjectContainer");

		LoadHexObjects ();
		QuerySetObjectsLoaded ();

		QueryLinkHexObjects ();
	}

	
	void Update () 
	{
		
	}


	private void LoadHexObjects()
	{

		for (int t = 0; t < objectPoolSize; t++) {

			GameObject _sfObj = Instantiate (Resources.Load ("Prefabs/HexObject", typeof(GameObject))) as GameObject;

			if (_sfObj != null) {

				if (HexObjectContainer != null) {
					_sfObj.transform.parent = HexObjectContainer.transform;
				}
				_sfObj.name = "hexObj" + t.ToString ();

				//default storage location
				_sfObj.transform.position = new Vector2 (StoragePosition.transform.position.x, StoragePosition.transform.position.y);

				HexObject objectScript = _sfObj.GetComponent<HexObject> ();
				objectScript.ID = t;

				HexObjectList.Add (_sfObj);

			} else {

				Debug.Log ("Couldn't load hex object prefab");
			}
		}

		_nullObj = Instantiate (Resources.Load ("Prefabs/HexObject", typeof(GameObject))) as GameObject;
		HexObject nullObjectScript = _nullObj.GetComponent<HexObject> ();
		nullObjectScript.ID = -1;
		nullObjectScript.isNullObject = true;

	}


	void QuerySetObjectsLoaded() 
	{
		foreach(GameObject tObj in HexObjectList)
		{
			HexObject objectScript = tObj.GetComponent<HexObject> ();
			objectScript._State = HexObject.eState.Loaded;

			objectScript.InitHexLinkList ();
		}
	}

	private GameObject QueryFindObjectByID(int id) 
	{
		foreach(GameObject tObj in HexObjectList)
		{
			HexObject objectScript = tObj.GetComponent<HexObject> ();
			if (objectScript.ID == id) {
				return tObj;
			}
		}
		return null;
	}


	void QueryLinkHexObjects() 
	{
		int w = HexGridWidth;
		int h = HexGridHeight;
		int max = w * h;

		foreach(GameObject tObj in HexObjectList)
		{
			HexObject objectScript = tObj.GetComponent<HexObject> ();

			int id = objectScript.ID;

			//surrounding
			//0 = id - w
			//1 = id - (w - 1)
			//2 = id + 1
			//3 = id + w
			//4 = id - 1
			//5 = id - (w + 1)

			//edges
			//top id < w 
			//L side id % w == 0
			//R side id+1 % w == 0
			//bot id > (w * h - w)

			//0
			int lookupID = id - w;
			if (lookupID >= 0 && lookupID < max) {
				GameObject go = QueryFindObjectByID (lookupID);
				objectScript.AddLinkedObject (go);
			} else {
				objectScript.AddLinkedObject (_nullObj);
			}

			//1
			lookupID = id - (w - 1);
			if (lookupID >= 0 && lookupID < max) {
				GameObject go = QueryFindObjectByID (lookupID);
				objectScript.AddLinkedObject (go);
			} else {
				objectScript.AddLinkedObject (_nullObj);
			}

			//2
			lookupID = id + 1;
			if (lookupID >= 0 && lookupID < max) {
				GameObject go = QueryFindObjectByID (lookupID);
				objectScript.AddLinkedObject (go);
			} else {
				objectScript.AddLinkedObject (_nullObj);
			}

			//3
			lookupID = id + w;
			if (lookupID >= 0 && lookupID < max) {
				GameObject go = QueryFindObjectByID (lookupID);
				objectScript.AddLinkedObject (go);
			} else {
				objectScript.AddLinkedObject (_nullObj);
			}

			//4
			lookupID = id - 1;
			if (lookupID >= 0 && lookupID < max) {
				GameObject go = QueryFindObjectByID (lookupID);
				objectScript.AddLinkedObject (go);
			} else {
				objectScript.AddLinkedObject (_nullObj);
			}

			//5
			lookupID = id - (w + 1);
			if (lookupID >= 0 && lookupID < max) {
				GameObject go = QueryFindObjectByID (lookupID);
				objectScript.AddLinkedObject (go);
			} else {
				objectScript.AddLinkedObject (_nullObj);
			}

		}
	}


}
