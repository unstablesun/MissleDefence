using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class CanonBallCollision : MonoBehaviour 
{
	public bool useDebugDraw = true;

	void OnTriggerEnter2D(Collider2D coll) 
	{
		if (coll.gameObject.tag == "Enemy") {
			Debug.Log ("OnTriggerEnter2D tag = " + coll.gameObject.tag.ToString ());
			SendMessageUpwards("CalculateDamageBlast", coll);
		}
	}

	void OnTriggerStay2D(Collider2D coll) 
	{
		if (coll.gameObject.tag == "Enemy") {
			Debug.Log ("OnTriggerStay2D tag = " + coll.gameObject.tag.ToString() );
			SendMessageUpwards("CalculateDamageBurn", coll);
		}
	}

	void OnTriggerExit2D(Collider2D coll) 
	{
		if (coll.gameObject.tag == "Enemy") {
			Debug.Log ("OnTriggerExit2D tag = " + coll.gameObject.tag.ToString() );
		}
	}


	void Update() 
	{
		LineDrawerUpdate();
	}






	/*
	 * --------------------------------------------------------
							Debug Draw
	 * --------------------------------------------------------
	*/

	LineRenderer lineRenderer = null;
	void LineDrawerUpdate ()
	{    
		if(useDebugDraw == true) {
			lineRenderer = GetComponent<LineRenderer>();
			if(lineRenderer != null) {

				Collider2D collider2D = gameObject.GetComponent<Collider2D>();

				Vector3 colSize = collider2D.bounds.size;

				float centerX = gameObject.transform.position.x;
				float centerY = gameObject.transform.position.y;

				//Vector3 scale = gameObject.transform.localScale;
				//float radius = scale.x;

				float radius = colSize.x;

				Vector3 pos = new Vector3(centerX - radius, centerY - radius, 1);
				lineRenderer.SetPosition(0, pos);

				pos = new Vector3(centerX - radius, centerY + radius, 1);
				lineRenderer.SetPosition(1, pos);

				pos = new Vector3(centerX + radius, centerY + radius, 1);
				lineRenderer.SetPosition(2, pos);

				pos = new Vector3(centerX + radius, centerY - radius, 1);
				lineRenderer.SetPosition(3, pos);
			}
		}
	}

}
