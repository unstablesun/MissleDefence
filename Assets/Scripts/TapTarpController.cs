using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;



//------------------------------------------------------
/*
 				Tap Tarp Controller
*/
//------------------------------------------------------

public class TapTarpController : MonoBehaviour 
{

	//private List <GameObject> TarpObjectList = null;

	//private GameObject TarpObjectContainer;

	public float LaunchCoolDown = 1f;

	private bool _mouseDown = false;
	private float _elaspedTime = 0f;

	//void Awake()
	//{}

	void Start()
	{
		_elaspedTime = 0f;
	}

	//void Update()
	//{}

	void OnMouseDown()
	{
		if(Input.GetMouseButtonDown(0)) {
		
			Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			target.z = 0;
			//gameObject.transform.position = target; 

			_mouseDown = true;
			_elaspedTime = 0f;

			SpriteCanonController.Instance.LaunchProjectile (target, SpriteCanonObject.eType.projectile);
		}
	}

	void OnMouseDrag() 
	{
		if(_mouseDown == true) {

			_elaspedTime += Time.deltaTime;
			if (_elaspedTime >= LaunchCoolDown) {

				_elaspedTime = 0f;
				
				Vector3 target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				target.z = 0;

				SpriteCanonController.Instance.LaunchProjectile (target, SpriteCanonObject.eType.projectile);
			}
		}
	}
		

	void OnMouseUp()
	{
		if(Input.GetMouseButtonUp(0)) {
			_mouseDown = false;
		}
	}

}

