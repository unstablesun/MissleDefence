using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SpriteCanonObject : MonoBehaviour 
{

	public enum eType 
	{
		projectile,
		clusterBomb, 
		nuke
	};
	public eType _Type = eType.projectile;

	public enum eState 
	{
		NoOp,
		Loaded,
		Fire,
		InFlight,
		Exploding,
		Dead
	};
	public eState _State = eState.NoOp;

	public enum eExplosionPhase 
	{
		ready,
		fire,
		fade 
	};
	public eExplosionPhase _ExplosionPhase = eExplosionPhase.ready;


	public GameObject baseSprite;//main body, also collision sprite?
	public GameObject secondarySprite;//addition animation support

	private int _id = 0;
	public int ID {
		get {return _id; } 
		set {_id = value; }
	}

	private float _velocity = 0f;
	public float velocity {
		get {return _velocity; } 
		set {_velocity = value; }
	}

	private Vector3 _storagePosition;
	public Vector3 StoragePosition {
		get {return _storagePosition; } 
		set {_storagePosition = value; }
	}

	private Vector3 _firingPosition;
	public Vector3 FiringPosition {
		get {return _firingPosition; } 
		set {_firingPosition = value; }
	}

	private Vector3 _destinationPosition;
	public Vector3 DestinationPosition {
		get {return _destinationPosition; } 
		set {_destinationPosition = value; }
	}

	float mRed = 255f;
	float mGreen = 255f;
	float mBlue = 255f;
	float mAlpha = 255f;

	private Vector3 _direction;
	private Vector3 _currentPosition;
	private float _fightMagnitude;

	private float _xScale, _yScale;
	private float _scaleFactor;

	private float _elaspedExplosionTime = 0f;
	private float _explosionTime = 1f;
	private float _fadeExplosionAlpha = 255f;
	private float _fadeFactor;



	void Awake()
	{
	}

	void Start() 
	{
	}

	public void SetBaseSpriteScale(float sx, float sy) 
	{
		baseSprite.transform.localScale = new Vector3 (sx, sy, 1f);
	}

	public void Reset() 
	{
		transform.localPosition = StoragePosition;

		_fightMagnitude = 0f;

		_xScale = 0.1f;
		_yScale = 0.1f;
		_scaleFactor = 1.1f;

		SetBaseSpriteScale (_xScale, _yScale);
		SetObjectColor (255, 255, 255, 255);

		_ExplosionPhase = eExplosionPhase.ready;
		_fadeExplosionAlpha = 64f;
		_fadeFactor = 50f;

	}

	void Update() 
	{
		//set a random set of rotations for cubes
		//transform.Rotate(Vector3.up, _velocity * Time.deltaTime);
		//transform.Rotate(Vector3.right, _velocity * Time.deltaTime);
		//transform.Rotate(Vector3.forward, _velocity * Time.deltaTime);

		switch (_State) 
		{

		case eState.NoOp:
		case eState.Loaded:
			Reset ();
			break;

		case eState.Fire:
			{
				CalculateFlightPath ();
			}
			break;

		case eState.InFlight:
			{
				UpdateFlightPath ();
			}
			break;

		case eState.Exploding:
			{
				UpdateExplosion ();
			}
			break;

		case eState.Dead:
			{
				_State = eState.Loaded;
			}
			break;
		}
			
	}
		
	public void SetLaunchParameters(Vector3 destination, eType type)
	{
		DestinationPosition = destination;

		_State = eState.Fire;
		_Type = type;

		switch (_Type) {

		case eType.projectile:
			_velocity = 4.0f;
			break;
		case eType.clusterBomb:
			_velocity = 2.75f;
			break;
		case eType.nuke:
			_velocity = 1.50f;
			break;

		}
	}

	private void CalculateFlightPath()
	{
		transform.localPosition = _firingPosition;

		_currentPosition = _firingPosition;
		_direction = _destinationPosition - _firingPosition;
		_fightMagnitude = _direction.magnitude;
		_direction.Normalize ();

		_State = eState.InFlight;

	}

	private void UpdateFlightPath()
	{
		Vector3 increment = _direction * (Time.deltaTime * _velocity);

		_currentPosition += increment;

		transform.localPosition = _currentPosition;

		Vector3 directionVec = _currentPosition - _firingPosition;
		float directionMag = directionVec.magnitude;

		if (directionMag > _fightMagnitude) {
			_ExplosionPhase = eExplosionPhase.fire;
			_elaspedExplosionTime = 0f;
			_State = eState.Exploding;
		}
	}

	private void UpdateExplosion()
	{
		if (_ExplosionPhase == eExplosionPhase.fire) {
			
			_xScale += Time.deltaTime * _scaleFactor;
			_yScale += Time.deltaTime * _scaleFactor;

			SetBaseSpriteScale (_xScale, _yScale);

			SetObjectColor (0, 255, 0, _fadeExplosionAlpha);

			_elaspedExplosionTime += Time.deltaTime;
			if (_elaspedExplosionTime >= _explosionTime) {
				_elaspedExplosionTime = 0;
				_ExplosionPhase = eExplosionPhase.fade;
			}

		} else if(_ExplosionPhase == eExplosionPhase.fade) {

			if (_fadeExplosionAlpha > 0f) {
				_fadeExplosionAlpha -= (Time.deltaTime * _fadeFactor);

				if (_fadeExplosionAlpha < 0f) {
					_fadeExplosionAlpha = 0f;

					_State = eState.Dead;

				}
			}
			SetObjectColor (0, 255, 0, _fadeExplosionAlpha);
		}
	}


	void OnCollisionEnter2D(Collision2D coll) 
	{
		Debug.Log ("OnCollisionEnter2D tag = " + coll.gameObject.tag.ToString() );

		if (coll.gameObject.tag == "Enemy") {
			coll.gameObject.SendMessage ("ApplyDamage", 10);
		}

	}

	void OnTriggerEnter2D(Collider2D coll) {

		if (coll.gameObject.tag == "Enemy") {
			Debug.Log ("OnTriggerEnter2D tag = " + coll.gameObject.tag.ToString() );
			coll.gameObject.SendMessage ("ApplyDamage", 10);
		}
	}



	/*
	 * --------------------------------------------------------
							Object Colors
	 * --------------------------------------------------------
	*/
	public void SetRandomObjectColor() 
	{
		if (baseSprite != null) {
			mRed = (float)Random.Range (0f, 255f);
			mGreen = (float)Random.Range (0f, 255f);
			mBlue = (float)Random.Range (0f, 255f);
			baseSprite.GetComponent<Renderer> ().material.color = new Color32 ((byte)mRed, (byte)mGreen, (byte)mBlue, (byte)mAlpha);
		}
	}


	public void SetObjectColor(float red, float green, float blue, float alpha) 
	{
		if (baseSprite != null) {
			mRed = red;
			mGreen = green;
			mBlue = blue;
			mAlpha = alpha;
			baseSprite.GetComponent<Renderer> ().material.color = new Color32 ((byte)mRed, (byte)mGreen, (byte)mBlue, (byte)mAlpha);
		}
	}



}

