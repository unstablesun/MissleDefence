using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexManager : MonoBehaviour 
{
	public int objectPoolSize = 128;

	public float HexGridWidth = 4f;
	public float HexGridHeight = 4f;
	public float HexGridDX = 1f;
	public float HexGridDY = 1f;
	public float HexSkipDY = 0.5f;

	public GameObject StoragePosition;
	public GameObject StartGridPosition;


	[HideInInspector]
	public List <GameObject> HexObjectList = null;
	private GameObject HexObjectContainer;

	public static HexManager Instance;

	private GameObject _nullObj = null;

	private float gridStartX, gridStartY;


	private int _scanType = 0;
	public int ScanType {
		get {return _scanType; } 
		set {_scanType = value; }
	}

	private int _runningScanIndex;


	void Awake () 
	{
		Instance = this;

		HexObjectList = new List<GameObject>();
	}

	void Start () 
	{
		HexObjectContainer = GameObject.Find ("HexObjectContainer");

		gridStartX = StartGridPosition.transform.position.x;
		gridStartY = StartGridPosition.transform.position.y;

		LoadHexObjects ();

		QuerySetObjectsLoaded ();

		QuerySetObjectsPosition();

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
		_nullObj.transform.position = new Vector2 (StoragePosition.transform.position.x, StoragePosition.transform.position.y);
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

	void QuerySetObjectsPosition() 
	{
		float xOffset = 0f;
		float yOffset = 0f;
		int lineCount = 0;
		int rowCount = 0;

		foreach(GameObject tObj in HexObjectList)
		{
			HexObject objectScript = tObj.GetComponent<HexObject> ();

			float x = gridStartX + xOffset;
			float y = gridStartY + yOffset;
			if(lineCount % 2 == 1) {
				y += HexSkipDY;
			}

			objectScript.SetHexPosition (new Vector3(x,y,1));


			if(lineCount == 0 || lineCount == HexGridWidth-1 || rowCount == 0 || rowCount == HexGridHeight-1) {
				objectScript.SetToNullObject();
				objectScript.SetObjectColor(128, 128, 128, 200);

				objectScript._Type = HexObject.eType.Edge;

			} else {
			
				objectScript._Type = HexObject.eType.Main;
			}
				
			xOffset += HexGridDX;
			lineCount++;
			if(lineCount >= HexGridWidth) {
				lineCount = 0;
				xOffset = 0f;
				yOffset += HexGridDY;
				rowCount++;
			}

			if(rowCount >= HexGridHeight) {
				break;
			}
		}
	}


	private GameObject QueryFindObjectByID(int id) 
	{
		foreach(GameObject tObj in HexObjectList)
		{
			HexObject objectScript = tObj.GetComponent<HexObject> ();
			if (objectScript.ID == id && objectScript._Type == HexObject.eType.Main) {
				
				return tObj;
			}
		}
		return null;
	}


	void QueryLinkHexObjects() 
	{
		float w = HexGridWidth;
		float h = HexGridHeight;
		float max = w * h;

		foreach(GameObject tObj in HexObjectList)
		{
			HexObject objectScript = tObj.GetComponent<HexObject> ();

			int id = objectScript.ID;

			float _id = (float)id;
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


			//Link Main Sequence
			//0
			float lookupID = _id - w;
			if (lookupID >= 0 && lookupID < max) 
			{
				GameObject go = QueryFindObjectByID ((int)lookupID);
				if(go != null) {
					objectScript.AddLinkedObject (go);
				} else {
					objectScript.AddLinkedObject (_nullObj);
				}
			}

			//1
			lookupID = _id - (w - 1);
			if (lookupID >= 0 && lookupID < max) 
			{
				GameObject go = QueryFindObjectByID ((int)lookupID);
				if(go != null) {
					objectScript.AddLinkedObject (go);
				} else {
					objectScript.AddLinkedObject (_nullObj);
				}
			}

			//2
			lookupID = _id + 1;
			if (lookupID >= 0 && lookupID < max) 
			{
				GameObject go = QueryFindObjectByID ((int)lookupID);
				if(go != null) {
					objectScript.AddLinkedObject (go);
				} else {
					objectScript.AddLinkedObject (_nullObj);
				}
			}

			//3
			lookupID = _id + w;
			if (lookupID >= 0 && lookupID < max) 
			{
				GameObject go = QueryFindObjectByID ((int)lookupID);
				if(go != null) {
					objectScript.AddLinkedObject (go);
				} else {
					objectScript.AddLinkedObject (_nullObj);
				}
			}

			//4
			lookupID = _id - 1;
			if (lookupID >= 0 && lookupID < max) 
			{
				GameObject go = QueryFindObjectByID ((int)lookupID);
				if(go != null) {
					objectScript.AddLinkedObject (go);
				} else {
					objectScript.AddLinkedObject (_nullObj);
				}
			}

			//5
			lookupID = _id - (w + 1);
			if (lookupID >= 0 && lookupID < max) 
			{
				GameObject go = QueryFindObjectByID ((int)lookupID);
				if(go != null) {
					objectScript.AddLinkedObject (go);
				} else {
					objectScript.AddLinkedObject (_nullObj);
				}
			}

		}
	}


	public void QueryAttachGemToHex(GameObject go) 
	{
		foreach(GameObject tObj in HexObjectList)
		{
			HexObject objectScript = tObj.GetComponent<HexObject> ();
			if (objectScript._Type == HexObject.eType.Main) {



			}
		}
	}

	public void SetScanSetting(int t)
	{
		_runningScanIndex = 0;
	}

	public GameObject QueryScanNextHex() 
	{
		int count = HexObjectList.Count;
		GameObject hexObj = null;
		while(_runningScanIndex < count)
		{
			hexObj = HexObjectList[_runningScanIndex++];
			if(hexObj != null) {
				HexObject objectScript = hexObj.GetComponent<HexObject> ();
				if (objectScript._Type == HexObject.eType.Main) {

					break;
				}
			}
		}

		return hexObj;
	}





}
