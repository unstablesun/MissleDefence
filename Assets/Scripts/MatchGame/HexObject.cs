using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexObject : MonoBehaviour 
{
	public enum eState 
	{
		NoOp,
		Loaded,
		Empty,
		Full
	};
	public eState _State = eState.NoOp;

	public enum eType 
	{
		Null,
		Main,
		Edge,
		Top,
		Side,
		Meter
	};
	public eType _Type = eType.Null;

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

	public GameObject backingSprite;
	public GameObject tapPadSprite;

	private int _id = 0;
	public int ID {
		get {return _id; } 
		set {_id = value; }
	}


	public List <GameObject> HexLinkList = null;

	public bool isNullObject = false;


	//[HideInInspector]
	public GameObject GemRef = null;



	void Start () 
	{

	}

	public void SetToNullObject () 
	{
		isNullObject = true;

		tapPadSprite.SetActive(false);
	}


	public void InitHexLinkList () 
	{
		HexLinkList = new List<GameObject>();

	}

	public void AttachGem (GameObject go) 
	{
		Debug.Log("AttachGem");
		GemRef = go;

		GemRef.transform.position = new Vector3( transform.position.x, transform.position.y, -1);
	}

	public bool NoGemAttached()
	{
		if(GemRef == null)
			return true;
		else
			return false;
	}
		
	public void AddLinkedObject(GameObject go)
	{
		//Debug.Log ("AddLinkedObject");
		if (HexLinkList != null) {
		
			//Debug.Log ("           Added");
			HexLinkList.Add (go);
		}
	}

	public void SetHexPosition(Vector3 vec)
	{
		transform.position = new Vector3(vec.x, vec.y, vec.z);
	}
	
	void Update () 
	{
		
	}






	public void SetObjectColor(float red, float green, float blue, float alpha) 
	{
		if (backingSprite != null) {
			backingSprite.GetComponent<Renderer> ().material.color = new Color32 ((byte)red, (byte)green, (byte)blue, (byte)alpha);
		}
	}		

}
