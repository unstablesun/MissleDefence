using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class City : MonoBehaviour 
{
	public CircleCollider2D CircleCollider;

	public float Defence = 50f;

	void Awake()
	{
		
	}

	void Start() 
	{
		
	}

	void Update() 
	{
		
	}

	public void ApplyDamage(float damage) 
	{
		if (Defence > 0f) {

			Defence -= damage;
		}
	}



	public bool CheckCircleCollision(Vector3 vec, float radius)
	{

		//Debug.Log ("City Check - eVec x = " + vec.x + " y = " + vec.y + " r = " + radius);



		if (CircleCollider != null) {

			Vector3 localVec = transform.position;
			float localRadius = CircleCollider.radius;

			Vector3 vec2 = new Vector3 (vec.x, vec.y, 0f);
			Vector3 localVec2 = new Vector3 (localVec.x, localVec.y, 0f);
			Vector3 cordVec = vec2 - localVec2;
			float cord = radius + localRadius;
			float mag = cordVec.magnitude;

			//Debug.Log ("City Check - cord = " + cord + " mag = " + mag);

			if (cord > mag) {

				//hit something
				return true;
			}

		}

		return false;


	}

}