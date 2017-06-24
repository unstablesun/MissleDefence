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
		GetWayPoint,
		CalculatePath,
		WaitAtPoint,
		MoveToPoint,
		ReachedPoint,
		OnEndPoint,
		EndMove,
		Dead
	};
	public eState _State = eState.NoOp;


	public GameObject primarySprite = null;
	public GameObject secondarySprite = null;
	public GameObject tertiarySprite = null;

	//private Vector3 startingPosition;

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

	private WayPointList mWayPointList = null;
	private int mCurrentWayPoint = 0;
	private int mMaxWayPointCount;
	private bool mOnEndPoint;
	private Vector3 WayPointOffset;


	private ColorSet mColorSet = null;


	float mRed = 128f;
	float mGreen = 128f;
	float mBlue = 128f;
	float mAlpha = 255f;

	private Vector3 _direction;
	private Vector3 _currentPosition;
	private float _fightMagnitude;




	private float _waitTime = 0f;
	private float _acceleration = 0f;
	private float _decelAtPercentOfMag = 0f;
	private float _decelaration = 0f;
	private float _minVelocity = 0f;
	private float _chanceToReverse = 0f;
	private float _chanceToSkip = 0f;


	private float _elaspedWaitTime = 0f;


	//private float _elaspedTime = 0f;

	void Awake()
	{


	}

	void Start() 
	{
		//startingPosition = _storagePosition;
		transform.localPosition = _startPosition;

	}
	public void Reset() 
	{
		//transform.localPosition = _storagePosition;

		Vector3 testWayPoint = mWayPointList.GetVector3AtIndex (1);
		transform.localPosition = testWayPoint;

		//transform.localPosition = _startPosition;


		mCurrentWayPoint = 0;
		mOnEndPoint = false;


		_State = eState.GetWayPoint;


	}

	public void AttachModuleData(AlienModuleData amd) 
	{
		if (mModuleData == null) {
			mModuleData = new AlienModuleData ();
		}

		mModuleData = amd;
	}

	public void FixUp()
	{
		SetPrimarySprite (mModuleData.PrimarySprite);


		//----------------------Way Points---------------------------
		if (mWayPointList == null) {
			mWayPointList = new WayPointList ();
		}
			
		mWayPointList = mModuleData.WayPointList;

		mMaxWayPointCount = mWayPointList.NumPointsUsed;

		//mWayPointList.DebugPrintList ("####  BEFORE  ####");

		Vector3 startingWayPoint = mWayPointList.GetVector3AtIndex (0);
		WayPointOffset = _startPosition - startingWayPoint;

		//debug
		//startingOffset.x = -2f;
		//startingOffset.y = -2f;

		//startingOffset.z = 0f;
		//mWayPointList.AddOffsetToPointList (startingOffset);

		//mWayPointList.DebugPrintList ("####  AFTER  ####");




		//------------------------Color-----------------------
		if (mColorSet == null) {
			mColorSet = new ColorSet ();
		}

		mColorSet = mModuleData.ColorSet;
		SetPrimarySpriteColor (mColorSet.PrimaryColour);

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
			break;

		case eState.Ready:
			Reset ();
			break;


		case eState.GetWayPoint:
			{
				GetWayPointData ();
			}
			break;

		case eState.CalculatePath:
			{
				CalculateFlightPath ();
			}
			break;

		case eState.WaitAtPoint:
			{
				WaitingAtPoint ();
			}
			break;

		case eState.MoveToPoint:
			{
				UpdateFlightPath ();
			}
			break;

		case eState.ReachedPoint:
			{
				GetWayPointData ();
			}
			break;

		case eState.OnEndPoint:
			{
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

		_State = eState.WaitAtPoint;

	}

	private void UpdateFlightPath()
	{
		Vector3 increment = _direction * (Time.deltaTime * _velocity);

		_currentPosition += increment;

		transform.localPosition = _currentPosition;

		Vector3 directionVec = _currentPosition - _startPosition;
		float directionMag = directionVec.magnitude;

		Debug.Log ("directionMag = " + directionMag.ToString() + " _fightMagnitude = " + _fightMagnitude.ToString());
		if (directionMag > _fightMagnitude) {
			_State = eState.ReachedPoint;
		}
	}


	private void GetWayPointData()
	{
		int wpIndex = mCurrentWayPoint;

		//mWayPointList = mModuleData.WayPointList;

		_startPosition = mWayPointList.GetVector3AtIndex (wpIndex);

		_startPosition += WayPointOffset;

		if (wpIndex < mMaxWayPointCount - 1) {
		
			_destinationPosition = mWayPointList.GetVector3AtIndex (wpIndex + 1);
			_destinationPosition += WayPointOffset;

		} else {

			//arrived at end point
			mOnEndPoint = true;
		}
			
		WayPoint wp = mWayPointList.GetWayPointDataAtIndex (wpIndex);
		_waitTime = wp.WaitTime;

		//_velocity = wp.Velocity;
		_velocity = 1f;

		_acceleration = wp.Acceleration;
		_decelAtPercentOfMag = wp.DecelAtPercentOfMag;
		_decelaration = wp.Decelaration;
		_minVelocity = wp.MinVelocity;
		_chanceToReverse = wp.ChanceToReverse;
		_chanceToSkip = wp.ChanceToSkip;

		_elaspedWaitTime = 0f;

		mCurrentWayPoint++;

		_State = eState.CalculatePath;

	}


	private void WaitingAtPoint()
	{
		_elaspedWaitTime += Time.deltaTime;
		if (_elaspedWaitTime >= _waitTime) {
			_elaspedWaitTime = 0;

			if (mOnEndPoint == true) {
				_State = eState.OnEndPoint;
			} else {
				_State = eState.MoveToPoint;
			}

		} else {
			
			//do other things

		}
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

	public void SetRandomPrimarySpriteColor(int type) 
	{
		if (primarySprite != null) {
			mRed = (float)Random.Range (0f, 255f);
			mGreen = (float)Random.Range (0f, 255f);
			mBlue = (float)Random.Range (0f, 255f);
			primarySprite.GetComponent<Renderer> ().material.color = new Color32 ((byte)mRed, (byte)mGreen, (byte)mBlue, (byte)mAlpha);
		}
	}

	public void SetPrimarySpriteColor(float red, float green, float blue, float alpha) 
	{
		if (primarySprite != null) {
			mRed = red;
			mGreen = green;
			mBlue = blue;
			mAlpha = alpha;
			primarySprite.GetComponent<Renderer> ().material.color = new Color32 ((byte)mRed, (byte)mGreen, (byte)mBlue, (byte)mAlpha);
		}
	}		

	public void SetPrimarySpriteColor(Color32 c32) 
	{
		if (primarySprite != null) {
			primarySprite.GetComponent<Renderer> ().material.color = c32;
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

