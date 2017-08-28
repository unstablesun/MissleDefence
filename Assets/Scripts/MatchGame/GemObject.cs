using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;

public class GemObject : MonoBehaviour 
{
	public enum eState 
	{
		NoOp,
		Loaded,
		Prepare,
		Active,
		Exploding,
		Dead
	};
	public eState _State = eState.NoOp;

	public GameObject gemSprite;

	private int _id = 0;
	public int ID {
		get {return _id; } 
		set {_id = value; }
	}


	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

	public void SetGemSprite (GameObject go) 
	{
		gemSprite.GetComponent<SpriteRenderer>().sprite= go.GetComponent<SpriteRenderer>().sprite;
	}
		

	public void StartAnim (float endx, float endy, float z, float offsetx, float speed) 
	{
		transform.DOMove(new Vector3(endx + offsetx, endy, z), speed).SetLoops(1, LoopType.Restart).OnComplete(GemDone);
	}

	void GemDone () 
	{
		//Debug.Log ("CLOUD DONE");
		gameObject.SetActive (false);
	}

}
