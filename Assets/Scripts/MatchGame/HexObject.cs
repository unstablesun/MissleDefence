using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexObject : MonoBehaviour 
{
	public enum eState 
	{
		NoOp,
		Loaded,
		Fire,
		InFlight,
		Exploding,
		Dead
	};
	public eState _State = eState.NoOp;

	public enum eLinkDirection 
	{
		North,
		NorthEast,
		SouthEast,
		South,
		SouthWest,
		NorthWest
	};
	public eLinkDirection _LinkDirection = eLinkDirection.North;

	private int _id = 0;
	public int ID {
		get {return _id; } 
		set {_id = value; }
	}


	public List <GameObject> HexLinkList = null;

	public bool isNullObject = false;

	void Start () 
	{

	}

	public void InitHexLinkList () 
	{
		HexLinkList = new List<GameObject>();

	}


	public void AddLinkedObject(GameObject go)
	{
		Debug.Log ("AddLinkedObject");
		if (HexLinkList != null) {
		
			Debug.Log ("           Added");
			HexLinkList.Add (go);
		}
	}
	
	void Update () 
	{
		
	}
}
