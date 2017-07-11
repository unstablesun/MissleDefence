using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongCurve : MonoBehaviour 
{

	private BezierCurve objectScript;

	private float _time = 0f;

	private float _scale = 0.5f;



	// Use this for initialization
	void Start () {

		objectScript = GetComponent<BezierCurve> ();
		SetTimeZero ();

		int count = objectScript.pointCount;

		//Debug.Log ("POINT COUNT = " + count);
	}

	void SetTimeZero () {
		_time = 0f;
	}
		
	
	// Update is called once per frame
	void Update () {

		_time += 0.001f;

		if (_time < 1f) {

			Vector3 vec = objectScript.GetPointAt (_time);

			//Debug.Log ("x = " + vec.x + "  y = " + vec.y + "  z = " + vec.z + "  _time = " + _time);

			transform.localPosition = new Vector3 (vec.x * _scale, vec.y * _scale, vec.z * _scale);
		}
		
	}
}
