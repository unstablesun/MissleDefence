using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;
using DG.Tweening;

public class GemManager : MonoBehaviour 
{

	public EZObjectPool objectPool;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}


	private void InsertGem (Vector3 position) 
	{


		GameObject go = null;
		objectPool.TryGetNextObject(new Vector3(position.x , position.y, -1f), new Quaternion(), out go);

		//Debug.Log ("scaleX = " + scale.x.ToString() + "  scaleY = " + scale.y.ToString() + "  rx = " + rx + "  ry = " + ry);

		if (go != null) {

			GemObject objectScript = go.GetComponent<GemObject> ();


		}


	}

}
