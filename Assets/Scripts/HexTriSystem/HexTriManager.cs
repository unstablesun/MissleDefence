
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTriManager : MonoBehaviour 
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
	public static HexTriManager Instance;
	private GameObject _nullObj = null;
	private float gridStartX, gridStartY;
	public List <GameObject> ScannedLinkedList = null;


	public List <GameObject> TriObjectList = null;
	private GameObject TriObjectContainer;

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
		TriObjectList = new List<GameObject>();

		ScannedLinkedList = new List<GameObject>();
	}

	void Start () 
	{
		HexObjectContainer = GameObject.Find ("HexObjectContainer");
		TriObjectContainer = GameObject.Find ("TriObjectContainer");

		gridStartX = StartGridPosition.transform.position.x;
		gridStartY = StartGridPosition.transform.position.y;

		LoadHexObjects ();

		QuerySetObjectsLoaded ();

		QuerySetObjectsPosition();

		QueryLinkHexObjects ();


		LoadTriObjects ();

	}


	void Update () 
	{

	}


	private void LoadHexObjects()
	{

		for (int t = 0; t < objectPoolSize; t++) {

			GameObject _sfObj = Instantiate (Resources.Load ("Prefabs/HexTriObject", typeof(GameObject))) as GameObject;

			if (_sfObj != null) {

				if (HexObjectContainer != null) {
					_sfObj.transform.parent = HexObjectContainer.transform;
				}
				_sfObj.name = "hexObj" + t.ToString ();

				//default storage location
				_sfObj.transform.position = new Vector2 (StoragePosition.transform.position.x, StoragePosition.transform.position.y);

				HexTriObject objectScript = _sfObj.GetComponent<HexTriObject> ();
				objectScript.ID = t;

				HexObjectList.Add (_sfObj);

			} else {

				Debug.Log ("Couldn't load hex object prefab");
			}
		}

		_nullObj = Instantiate (Resources.Load ("Prefabs/HexTriObject", typeof(GameObject))) as GameObject;
		_nullObj.transform.position = new Vector2 (StoragePosition.transform.position.x, StoragePosition.transform.position.y);
		HexTriObject nullObjectScript = _nullObj.GetComponent<HexTriObject> ();
		nullObjectScript.ID = -1;
		nullObjectScript.isNullObject = true;

	}

	private void LoadTriObjects()
	{

		for (int t = 0; t < objectPoolSize; t++) {

			GameObject _sfObj = Instantiate (Resources.Load ("Prefabs/TriObject", typeof(GameObject))) as GameObject;

			if (_sfObj != null) {

				if (TriObjectContainer != null) {
					_sfObj.transform.parent = TriObjectContainer.transform;
				}
				_sfObj.name = "triObj" + t.ToString ();

				//default storage location
				_sfObj.transform.position = new Vector2 (StoragePosition.transform.position.x, StoragePosition.transform.position.y);

				TriObject objectScript = _sfObj.GetComponent<TriObject> ();
				objectScript.ID = t;

				TriObjectList.Add (_sfObj);

			} else {

				Debug.Log ("Couldn't load hex object prefab");
			}
		}


	}



	void QuerySetObjectsLoaded() 
	{
		foreach(GameObject tObj in HexObjectList)
		{
			HexTriObject objectScript = tObj.GetComponent<HexTriObject> ();
			objectScript._State = HexTriObject.eState.Loaded;

			objectScript.InitHexLinkList ();
		}
	}



	int[] BoardFrame1 = 
	{ 
		1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
		1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
		1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
		1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
		1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
		1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
		1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
		1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
		1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
		1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
		1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
		1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
		1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
	};

	void QuerySetObjectsPosition() 
	{
		float xOffset = 0f;
		float yOffset = 0f;
		int lineCount = 0;
		int rowCount = 0;

		foreach(GameObject tObj in HexObjectList)
		{
			HexTriObject objectScript = tObj.GetComponent<HexTriObject> ();

			float x = gridStartX + xOffset;
			float y = gridStartY + yOffset;
			if(lineCount % 2 == 1) {

				y += HexSkipDY;

				objectScript.isSkipHex = true;
			}

			objectScript.SetHexPosition (new Vector3(x,y,1));

			int gIndex = (int)(rowCount * HexGridWidth + lineCount);

			if(BoardFrame1[gIndex] == 1) {
				
				objectScript.SetToNullObject();
				objectScript.SetObjectColor(128, 128, 128, 200);
				objectScript._Type = HexTriObject.eType.Edge;

			} else {

				objectScript._Type = HexTriObject.eType.Main;
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
			HexTriObject objectScript = tObj.GetComponent<HexTriObject> ();
			if (objectScript.ID == id && objectScript._Type == HexTriObject.eType.Main) {

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
			HexTriObject objectScript = tObj.GetComponent<HexTriObject> ();

			int id = objectScript.ID;
			bool isSkip = objectScript.isSkipHex;

			float _id = (float)id;

			//Link Main Sequence
			if (isSkip) {

				//0
				float lookupID = _id - w;
				if (lookupID >= 0 && lookupID < max)
					SetLink (lookupID, objectScript);

				//1
				lookupID = _id - (w - 1);
				if (lookupID >= 0 && lookupID < max)
					SetLink (lookupID, objectScript);

				//2
				lookupID = _id + 1;
				if (lookupID >= 0 && lookupID < max)
					SetLink (lookupID, objectScript);

				//3
				lookupID = _id + w;
				if (lookupID >= 0 && lookupID < max)
					SetLink (lookupID, objectScript);

				//4
				lookupID = _id - 1;
				if (lookupID >= 0 && lookupID < max)
					SetLink (lookupID, objectScript);

				//5
				lookupID = _id - (w + 1);
				if (lookupID >= 0 && lookupID < max)
					SetLink (lookupID, objectScript);

			} else { //non skip

				//0
				float lookupID = _id - w;
				if (lookupID >= 0 && lookupID < max)
					SetLink (lookupID, objectScript);

				//1
				lookupID = _id + 1;
				if (lookupID >= 0 && lookupID < max)
					SetLink (lookupID, objectScript);

				//2
				lookupID = _id + (w + 1);
				if (lookupID >= 0 && lookupID < max)
					SetLink (lookupID, objectScript);

				//3
				lookupID = _id + w;
				if (lookupID >= 0 && lookupID < max)
					SetLink (lookupID, objectScript);

				//4
				lookupID = _id + (w - 1);
				if (lookupID >= 0 && lookupID < max)
					SetLink (lookupID, objectScript);

				//5
				lookupID = _id - 1;
				if (lookupID >= 0 && lookupID < max)
					SetLink (lookupID, objectScript);

			}

		}
	}

	private void SetLink(float lookupID, HexTriObject objectScript)
	{
		GameObject go = QueryFindObjectByID ((int)lookupID);
		if(go != null) {
			HexTriObject objScript = go.GetComponent<HexTriObject> ();
			if (objScript._Type == HexTriObject.eType.Main) {
				objectScript.AddLinkedObject (go);
			} else {
				objectScript.AddLinkedObject (_nullObj);
			}
		} else {
			objectScript.AddLinkedObject (_nullObj);
		}

	}


	public void QueryAttachGemToHex(GameObject go) 
	{
		foreach(GameObject tObj in HexObjectList)
		{
			HexTriObject objectScript = tObj.GetComponent<HexTriObject> ();
			if (objectScript._Type == HexTriObject.eType.Main) {



			}
		}
	}

	public void SetScanSetting(int t)
	{
		_runningScanIndex = 0;
	}

	//this scans and returns hexs on the main sequence left to right top to bottom
	public GameObject QueryScanNextHex() 
	{
		int valve = 0;
		int count = HexObjectList.Count;
		GameObject hexObj = null;
		while(_runningScanIndex < count)
		{
			hexObj = HexObjectList[_runningScanIndex++];
			if(hexObj != null) {
				HexTriObject objectScript = hexObj.GetComponent<HexTriObject> ();
				if (objectScript._Type == HexTriObject.eType.Main) {

					break;
				}
			}

			if (++valve >= 100)
				break;
		}

		return hexObj;
	}


}
