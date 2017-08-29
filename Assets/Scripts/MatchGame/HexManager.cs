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

	public List <GameObject> ScannedLinkedList = null;



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

		ScannedLinkedList = new List<GameObject>();
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

				objectScript.isSkipHex = true;
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
				
			} else {
			
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
		
	private void SetLink(float lookupID, HexObject objectScript)
	{
		GameObject go = QueryFindObjectByID ((int)lookupID);
		if(go != null) {
			HexObject objScript = go.GetComponent<HexObject> ();
			if (objScript._Type == HexObject.eType.Main) {
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
			HexObject objectScript = tObj.GetComponent<HexObject> ();
			if (objectScript._Type == HexObject.eType.Main) {



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




	//-------------------------------------------------
	//				Scan for Matches
	//-------------------------------------------------

	public void QueryClearMarked() 
	{
		foreach(GameObject tObj in HexObjectList)
		{
			HexObject objectScript = tObj.GetComponent<HexObject> ();
			if (objectScript._Type == HexObject.eType.Main) {
				objectScript.MarkedColor = (int)GemObject.eColorType.Black;
			}
		}
	}

	public void QueryClearScan() 
	{
		foreach(GameObject tObj in HexObjectList)
		{
			HexObject objectScript = tObj.GetComponent<HexObject> ();
			if (objectScript._Type == HexObject.eType.Main) {
				objectScript.ScanColor = (int)GemObject.eColorType.Black;
			}
		}
	}

	public void QueryScanToMarked() 
	{
		foreach(GameObject tObj in HexObjectList)
		{
			HexObject objectScript = tObj.GetComponent<HexObject> ();
			if (objectScript._Type == HexObject.eType.Main) {

				if (objectScript.MarkedColor == (int)GemObject.eColorType.Black) {
					objectScript.MarkedColor = objectScript.ScanColor;
				}
			}
		}
	}
		
	public int QueryCountScanColors(int scanColor) 
	{
		int count = 0;
		foreach(GameObject tObj in HexObjectList)
		{
			HexObject objectScript = tObj.GetComponent<HexObject> ();
			if (objectScript._Type == HexObject.eType.Main) {
				if (objectScript.ScanColor == scanColor) {
				
					count++;
				}
			}
		}

		return count;
	}


	public void QueryScanAndMark() 
	{
		QueryClearMarked ();

		foreach(GameObject tObj in HexObjectList)
		{
			HexObject objectScript = tObj.GetComponent<HexObject> ();
			if (objectScript._Type == HexObject.eType.Main && objectScript.MarkedColor == (int)GemObject.eColorType.Black) {

				//new target so clear scan colors
				QueryClearScan ();
				ScannedLinkedList.Clear ();

				//do link walk
				GemObject.eColorType colorType = objectScript.GetGemRefColorType ();

				//debug
				if (colorType == GemObject.eColorType.Red) {
					Debug.Log ("Link Walk for Color " + colorType.ToString() + " Object ID = " + objectScript.ID);
				}

				objectScript.ScanColor = (int)colorType;

				//get link list for this target object
				List <GameObject> linkList = objectScript.HexLinkList;

				bool eval = EvaluateLinkedList (linkList, colorType);

				if (colorType == GemObject.eColorType.Red) {
					Debug.Log ("<<eval = true>> <<ScannedLinkedList Count = " + ScannedLinkedList.Count);
					foreach(GameObject debugObj in ScannedLinkedList){

						HexObject debugScript = debugObj.GetComponent<HexObject> ();

						Debug.Log ("____________________ScannedLinkedList Obj ID = " + debugScript.ID);

					}
						
				}

				while (eval == true) {

					if (colorType == GemObject.eColorType.Red) {
						Debug.Log ("<<In eval loop>>");
					}

					List<GameObject> evalList = new List<GameObject>(ScannedLinkedList);
					ScannedLinkedList.Clear ();

					if (evalList.Count == 0) {
						eval = false;
					}

					foreach (GameObject linkObj in evalList) {

						if (colorType == GemObject.eColorType.Red) {
							HexObject tempScript = linkObj.GetComponent<HexObject> ();
							Debug.Log ("    $$$ linkObj ID  = " + tempScript.ID);
						}

						
						HexObject objScript = linkObj.GetComponent<HexObject> ();

						List <GameObject> sublinkList = objScript.HexLinkList;
						eval = EvaluateLinkedList (sublinkList, colorType);

						//not sure if this is needed
						//if (eval == false) {
						//	break;
						//}
					}
						
				}

				int count = QueryCountScanColors ((int)colorType);
				if (count >= 4) {

					//if >= 5 -> add powerup
					QueryScanToMarked ();

					Debug.Log ("########### QueryScanToMarked for color " + colorType.ToString() + "  count = " + count);

				}
			}
		}
	}

	private bool EvaluateLinkedList(List <GameObject> linkList, GemObject.eColorType colorType)
	{

		bool eval = false;
		foreach (GameObject linkObj in linkList) {

			HexObject objectScript = linkObj.GetComponent<HexObject> ();

			if (objectScript._Type == HexObject.eType.Main) {

				if (objectScript.ScanColor == (int)GemObject.eColorType.Black && objectScript.MarkedColor == (int)GemObject.eColorType.Black) {

					GemObject.eColorType cType = objectScript.GetGemRefColorType ();

					if (cType == colorType) {

						//debug
						if (colorType == GemObject.eColorType.Red) {
							Debug.Log ("......Writing Color " + colorType.ToString() + " Object ID = " + objectScript.ID);
						}


						objectScript.ScanColor = (int)colorType;

						ScannedLinkedList.Add (linkObj);

						eval = true;
					}

				}
			}
		}

		return eval;
	}


	public void QueryShowMarkedHexes() 
	{
		foreach(GameObject tObj in HexObjectList)
		{
			HexObject objectScript = tObj.GetComponent<HexObject> ();
			if (objectScript._Type == HexObject.eType.Main) {
				if (objectScript.MarkedColor != (int)GemObject.eColorType.Black) {
				
					objectScript.SetObjectColor (255, 0, 0, 255);
				}
			}
		}
	}




}
