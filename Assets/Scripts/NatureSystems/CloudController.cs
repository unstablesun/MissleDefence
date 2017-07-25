
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;
using DG.Tweening;

public class CloudController : MonoBehaviour 
{
	public EZObjectPool objectPool;

	public GameObject StartPosition;
	public GameObject EndPosition;

	public GameObject StartArea;
	public GameObject EndArea;

	public float WindSpeed;
	public float SpeedRange;

	public float CloudSpawnRate = 1f;

	public float[] ZLevels = null;


	[HideInInspector]
	public float StartAreaX { get; set; }

	[HideInInspector]
	public float StartAreaY { get; set; }

	private float _elaspedTime;

	void Awake ()
	{
		DOTween.Init(false, true, LogBehaviour.ErrorsOnly);

		DOTween.defaultEaseType = Ease.Linear;
		DOTween.defaultEasePeriod = 0f;
	}


	void Start () 
	{
		_elaspedTime = 0f;

		Vector3 scale = Vector3.one;
		if (StartArea != null) {
			scale = StartArea.transform.localScale;
		}

		StartAreaX = scale.x;
		StartAreaY = scale.y;
	}

	void Update () 
	{
		_elaspedTime += Time.deltaTime;
		if (_elaspedTime >= CloudSpawnRate) {
			_elaspedTime = 0;

			InsertCloud ();
		}

	}

	private void InsertCloud () 
	{
		Vector3 position = StartPosition.transform.position;

		float rx = (float)Random.Range (-(StartAreaX/2), StartAreaX/2);
		float ry = (float)Random.Range (-(StartAreaY/2), StartAreaY/2);

		float rz = 0f;
		if (ZLevels != null) {
			
			int len = ZLevels.Length;
			int index = (int)Random.Range (0, len);
			rz = ZLevels[index];
		}


		GameObject go = null;
		objectPool.TryGetNextObject(new Vector3(position.x + rx, position.y + ry, rz), new Quaternion(), out go);

		//Debug.Log ("scaleX = " + scale.x.ToString() + "  scaleY = " + scale.y.ToString() + "  rx = " + rx + "  ry = " + ry);

		if (go != null) {

			CloudSpriteObj objectScript = go.GetComponent<CloudSpriteObj> ();

			float speedRange = (float)Random.Range (-SpeedRange, SpeedRange);
			float speed = WindSpeed + speedRange;

			float ex = EndPosition.transform.position.x;
			float ey = EndPosition.transform.position.y;

			float rangeX = EndPosition.transform.localScale.x;
			float offsetx = (float)Random.Range (-(rangeX/2), rangeX/2);

			objectScript.StartAnim (ex, ey, rz, offsetx, speed);

			float grey = (float)Random.Range (32f, 160f);
			float alpha = (float)Random.Range (32f, 200f);
			go.GetComponent<Renderer> ().material.color = new Color32 ((byte)grey, (byte)grey, (byte)grey, (byte)alpha);

		}


	}

}

