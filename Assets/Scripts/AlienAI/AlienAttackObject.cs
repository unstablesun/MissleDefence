using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class AlienAttackObject : MonoBehaviour 
{
	public bool useDebugDraw = true;

	public enum eType 
	{
		ship,
		shipBomb, 
		missle,
		nuke
	};
	public eType _type = eType.missle;



	public enum eState 
	{
		NoOp,
		Ready,
		CalculatePath,
		MoveToPoint,
		ReachedPoint,
		EndMove,
		Dead
	};
	public eState _State = eState.NoOp;


	public GameObject primarySprite = null;
	public GameObject secondarySprite = null;
	public GameObject tertiarySprite = null;

	private Vector3 startingPosition;

	private int _id = 0;
	public int ID {
		get {return _id; } 
		set {_id = value; }
	}

	private float _velocity = 10.0f;
	public float velocity {
		get {return _velocity; } 
		set {_velocity = value; }
	}

	private Vector3 _storagePosition;
	public Vector3 StoragePosition {
		get {return _storagePosition; } 
		set {_storagePosition = value; }
	}

	private Vector3 _startPosition;
	public Vector3 StartPosition {
		get {return _startPosition; } 
		set {_startPosition = value; }
	}

	private Vector3 _destinationPosition;
	public Vector3 DestinationPosition {
		get {return _destinationPosition; } 
		set {_destinationPosition = value; }
	}



	private AlienModuleData mModuleData = null;
	public AlienModuleData ModuleData
	{
		get { return mModuleData; }
		set { mModuleData = value; }
	}


	float mRed = 128f;
	float mGreen = 128f;
	float mBlue = 128f;
	float mAlpha = 255f;

	private Vector3 _direction;
	private Vector3 _currentPosition;
	private float _fightMagnitude;

	//private float _elaspedTime = 0f;

	void Awake()
	{


	}

	void Start() 
	{
		startingPosition = _storagePosition;
	}
	public void Reset() 
	{
		transform.localPosition = _storagePosition;
	}

	public void SetPrimarySprite(Sprite _sprite) 
	{
		primarySprite.GetComponent<SpriteRenderer>().sprite = _sprite;
	}
	public void SetSecondarySprite(Sprite _sprite) 
	{
		secondarySprite.GetComponent<SpriteRenderer>().sprite = _sprite;
	}
	public void SetTertiarySprite(Sprite _sprite) 
	{
		tertiarySprite.GetComponent<SpriteRenderer>().sprite = _sprite;
	}

	public void SetBaseSpriteScale(float sx, float sy) 
	{
		primarySprite.transform.localScale = new Vector3 (sx, sy, 1f);
	}


	void Update() 
	{

		switch (_State) 
		{

		case eState.NoOp:
		case eState.Ready:
			Reset ();
			break;

		case eState.CalculatePath:
			{
				CalculateFlightPath ();
			}
			break;

		case eState.MoveToPoint:
			{
				UpdateFlightPath ();
			}
			break;

		case eState.ReachedPoint:
			{
				FindNextWayPoint ();
			}
			break;

		case eState.Dead:
			{
				_State = eState.Ready;
			}
			break;
		}

		LineDrawerUpdate();


	}


	private void CalculateFlightPath()
	{
		transform.localPosition = _startPosition;

		_currentPosition = _startPosition;
		_direction = _destinationPosition - _startPosition;
		_fightMagnitude = _direction.magnitude;
		_direction.Normalize ();

		_State = eState.MoveToPoint;

	}

	private void UpdateFlightPath()
	{
		Vector3 increment = _direction * (Time.deltaTime * _velocity);

		_currentPosition += increment;

		transform.localPosition = _currentPosition;

		Vector3 directionVec = _currentPosition - _startPosition;
		float directionMag = directionVec.magnitude;

		if (directionMag > _fightMagnitude) {
			_State = eState.ReachedPoint;
		}
	}


	private void FindNextWayPoint()
	{
		
	}



	public void ApplyDamage(int amount)
	{
		Debug.Log ("ApplyDamage amount = " + amount.ToString());
	}







	/*
	 * --------------------------------------------------------
							Colors
	 * --------------------------------------------------------
	*/

	public void SetRandomObjectColor(int type) 
	{
		if (primarySprite != null) {
			mRed = (float)Random.Range (0f, 255f);
			mGreen = (float)Random.Range (0f, 255f);
			mBlue = (float)Random.Range (0f, 255f);
			primarySprite.GetComponent<Renderer> ().material.color = new Color32 ((byte)mRed, (byte)mGreen, (byte)mBlue, (byte)mAlpha);
		}
	}

	public void SetObjectColor(float red, float green, float blue, float alpha) 
	{
		if (primarySprite != null) {
			mRed = red;
			mGreen = green;
			mBlue = blue;
			mAlpha = alpha;
			primarySprite.GetComponent<Renderer> ().material.color = new Color32 ((byte)mRed, (byte)mGreen, (byte)mBlue, (byte)mAlpha);
		}
	}		








	/*
	 * --------------------------------------------------------
							Debug
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

