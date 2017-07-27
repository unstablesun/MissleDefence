
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;
using DG.Tweening;

public class MeteorController : MonoBehaviour 
{
	public EZObjectPool objectPool;

	public GameObject StartPosition;
	public GameObject EndPosition;

	public GameObject StartArea;
	public GameObject EndArea;

	public float WindSpeed;
	public float SpeedRange;

	public float MeteorSpawnRate = 1f;

	public Color StartColor;
	public Color TravelColor;
	public Color ExplodeColor;

	public float[] ZLevels = null;

	public bool ShowDebugRects = true;

	[HideInInspector]
	public float StartAreaX { get; set; }

	[HideInInspector]
	public float StartAreaY { get; set; }

	[HideInInspector]
	public float EndAreaX { get; set; }

	[HideInInspector]
	public float EndAreaY { get; set; }

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


		scale = Vector3.one;
		if (EndArea != null) {
			scale = EndArea.transform.localScale;
		}

		EndAreaX = scale.x;
		EndAreaY = scale.y;


		if (ShowDebugRects == false) {
			StartPosition.SetActive (false);
			EndPosition.SetActive (false);
			StartArea.SetActive (false);
			EndArea.SetActive (false);
		}

		//debug
		InsertMeteor ();
	}

	void Update () 
	{
		_elaspedTime += Time.deltaTime;
		if (_elaspedTime >= MeteorSpawnRate) {
			_elaspedTime = 0;

			InsertMeteor ();
		}

	}

	private void InsertMeteor () 
	{
		Vector3 position = StartPosition.transform.position;

		float rx = (float)Random.Range (-(StartAreaX/3), StartAreaX/3);
		float ry = (float)Random.Range (-(StartAreaY/3), StartAreaY/3);

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

			MeteorSpriteObj objectScript = go.GetComponent<MeteorSpriteObj> ();

			float speedRange = (float)Random.Range (-SpeedRange, SpeedRange);
			float speed = WindSpeed + speedRange;

			float ex = EndPosition.transform.position.x;
			float ey = EndPosition.transform.position.y;

			float offsetx = (float)Random.Range (-(EndAreaX/3f), EndAreaX/3f);
			float offsety = (float)Random.Range (-(EndAreaY/3f), EndAreaY/3f);

			objectScript.StartColor = StartColor;
			objectScript.TravelColor = TravelColor;
			objectScript.ExplodeColor = ExplodeColor;

			objectScript.StartAnim (ex + offsetx, ey + offsety, rz, speed);


			//float endPointX = ex + offsetx;
			//float endPointY = ey + offsety;
			//Debug.Log ("offsetx = " + offsetx + "  offsety = " + offsety + "  endPointX = " + endPointX + "  endPointY = " + endPointY);


		}


	}

	public List<GameObject> GetObjectList()
	{
		return objectPool.ObjectList;
	}


}


