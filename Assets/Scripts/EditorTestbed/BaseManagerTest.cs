using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;
using DG.Tweening;

public class BaseManagerTest : MonoBehaviour 
{
	public EZObjectPool objectPool;

	public GameObject StartPosition;
	public GameObject EndPosition;


	void Awake ()
	{
		DOTween.Init(false, true, LogBehaviour.ErrorsOnly);

		DOTween.defaultEaseType = Ease.Linear;
		DOTween.defaultEasePeriod = 0f;
	}


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		GameObject go = null;

		float rx = (float)Random.Range (-14f, -24f);
		float ry = (float)Random.Range (6f, 10f);
		float rz = (int)Random.Range (0f, -4f);

		objectPool.TryGetNextObject(new Vector3(rx, ry, rz), new Quaternion(), out go);

	
		if (go != null) {

			BaseSpriteObj objectScript = go.GetComponent<BaseSpriteObj> ();
			objectScript.StartAnimTest (rx, ry, rz);

			float grey = (float)Random.Range (32f, 160f);
			float alpha = (float)Random.Range (32f, 200f);
			go.GetComponent<Renderer> ().material.color = new Color32 ((byte)grey, (byte)grey, (byte)grey, (byte)alpha);




		}

		
	}
}
