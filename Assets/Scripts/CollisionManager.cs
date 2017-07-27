using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour 
{
	//here we're comparing our bullet pool to enemy pools 
	//and also enemy pools to the city pool

	//only check enemy pools that are active
	//ex. meteors and bombs, etc...


	/// <summary>
	/// Enemy Systems
	/// </summary>
	[HideInInspector]
	public bool MeteorsActive { get; set; }
	public MeteorController _meteorConroller;



	/// <summary>
	/// Friendly Systems
	/// </summary>
	public CityController _cityController;
	public SpriteCanonController _canonController;

	void Awake () 
	{
		MeteorsActive = true;
	}


	void Start () 
	{
		
	}
	
	void Update ()
	{
		if (MeteorsActive == true){
			
			if (_meteorConroller != null) {

				List<GameObject> meteorList = _meteorConroller.GetObjectList ();

				foreach (var meteor in meteorList) {

					if (meteor.gameObject.activeSelf == true) {
					
						Vector3 eVec = meteor.transform.position;

						//cities
						MeteorSpriteObj meteorScript = meteor.GetComponent<MeteorSpriteObj> ();
						float eRad = meteorScript.CircleCollider.radius;

						if (meteorScript.ExplodePhase == true) {
							
							List<City> cityList = _cityController.GetCityList ();
							foreach (var city in cityList) {

								City c = city.GetComponent<City> ();

								if (c.CheckCircleCollision (eVec, eRad)) {
							
									Debug.Log ("City Hit");

									//apply damage
									c.ApplyDamage( meteorScript.Power );

								}
							}
						}
							
						//bullets

						List<GameObject> canonList = _canonController.GetObjectList ();
						foreach (var canonBall in canonList) {

							SpriteCanonObject ball = canonBall.GetComponent<SpriteCanonObject> ();

							if (ball._State == SpriteCanonObject.eState.Exploding) {
							
								if (ball.CheckCircleCollision (eVec, eRad)) {
								
									//apply damage to meteor
								
									meteorScript.ApplyDamage (1);
								}
							
							}

						}
							
					}
				}
			}
		}
	}
}
