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
		Loaded,
		InFlight,
		Exploding,
		DiveBomb,
		Dead
	};
	public eState _State = eState.NoOp;


	private GameObject primarySprite = null;
	private GameObject secondarySprite = null;
	private GameObject tertiarySprite = null;

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


	//private float _elaspedTime = 0f;

	void Awake()
	{


	}

	void Start() 
	{
		startingPosition = transform.localPosition;
	}
	public void Reset() 
	{
		transform.localPosition = startingPosition;
	}

	public void SetBaseSpriteScale(float sx, float sy) 
	{
		primarySprite.transform.localScale = new Vector3 (sx, sy, 1f);
	}


	void Update() 
	{

		//set a random set of rotations for cubes
		//transform.Rotate(Vector3.up, _velocity * Time.deltaTime);
		//transform.Rotate(Vector3.right, _velocity * Time.deltaTime);
		//transform.Rotate(Vector3.forward, _velocity * Time.deltaTime);

		LineDrawerUpdate();


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

	public void SetObjectColor(int type) 
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

