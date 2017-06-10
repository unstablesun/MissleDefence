using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class CanonBallCollision : MonoBehaviour 
{

	void OnTriggerEnter2D(Collider2D coll) 
	{
		if (coll.gameObject.tag == "Enemy") 
		{
			Debug.Log ("OnTriggerEnter2D tag = " + coll.gameObject.tag.ToString ());

			SendMessageUpwards("CalculateDamage", coll);
		}
	}

	void OnTriggerStay2D(Collider2D coll) 
	{

		if (coll.gameObject.tag == "Enemy") {
			Debug.Log ("OnTriggerStay2D tag = " + coll.gameObject.tag.ToString() );

			//add exposure rate?


			SendMessageUpwards("CalculateDamage", coll);
		}
	}

	void OnTriggerExit2D(Collider2D coll) 
	{

		if (coll.gameObject.tag == "Enemy") {
			Debug.Log ("OnTriggerExit2D tag = " + coll.gameObject.tag.ToString() );

			//coll.gameObject.SendMessage ("ApplyDamage", 10);
		}
	}


}
