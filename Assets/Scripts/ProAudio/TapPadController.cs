using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapPadController : MonoBehaviour 
{
	bool touchDown = false;
	void Start () 
	{
		
	}
	
	void Update () 
	{
		#if UNITY_EDITOR


			if (Input.GetMouseButtonDown (0)) {
				Debug.Log ("Clicked Down");
				Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
				// RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
				if(hitInfo)
				{
					Debug.Log( "sprite = " + hitInfo.transform.gameObject.name );


					GameObject go = hitInfo.transform.gameObject;

					GameObject parent = go.transform.parent.gameObject;
					Debug.Log( "parent = " + parent.name );


					// Here you can check hitInfo to see which collider has been hit, and act appropriately.
				}

				touchDown = true;
			}


			if (Input.GetMouseButtonUp (0)) {
				Debug.Log ("Clicked Up");
				Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
				// RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
				if(hitInfo)
				{
					Debug.Log( "sprite = " + hitInfo.transform.gameObject.name );


					GameObject go = hitInfo.transform.gameObject;

					GameObject parent = go.transform.parent.gameObject;
					Debug.Log( "parent = " + parent.name );


					// Here you can check hitInfo to see which collider has been hit, and act appropriately.
				}

				touchDown = false;

			}

			if(touchDown == true) {
			
				Vector2 touchPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touchPos), Vector2.zero);

				if(hitInfo)
				{
					Debug.Log( "sprite = " + hitInfo.transform.gameObject.name );


					GameObject go = hitInfo.transform.gameObject;

					GameObject parent = go.transform.parent.gameObject;
					Debug.Log( "drag = " + parent.name );


					// Here you can check hitInfo to see which collider has been hit, and act appropriately.
				}

			}


		#endif

		#if UNITY_ANDROID || UNITY_IPHONE

		if (Input.touchCount != 1)
		{
			return;
		}


		Touch touch = Input.touches[0];
		Vector3 touchPos = touch.position;


		#endif



	}


	void OnMouseButtonDown ()
	{

	}
}
