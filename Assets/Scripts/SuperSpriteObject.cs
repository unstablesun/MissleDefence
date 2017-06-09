using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SuperSpriteObject : MonoBehaviour 
{

	public enum eType 
	{
		quad,
		cube, 
		composite
	};
	public eType _type = eType.quad;

	public enum eTask 
	{
		NoOp,
		TransCamera, 
		OpenCanvas
	};
	public eTask _task = eTask.NoOp;

	public GameObject baseSprite;

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

	void Update() 
	{

		//set a random set of rotations for cubes
		//transform.Rotate(Vector3.up, _velocity * Time.deltaTime);
		//transform.Rotate(Vector3.right, _velocity * Time.deltaTime);
		transform.Rotate(Vector3.forward, _velocity * Time.deltaTime);

			

	}







		
	/*
	 * --------------------------------------------------------
							Colors
	 * --------------------------------------------------------
	*/
	public void SetVertexColors(int type) 
	{

		if (baseSprite != null) {
			Debug.Log ("SetVertexColors");

			Mesh mesh = baseSprite.GetComponent<MeshFilter> ().mesh;
			Vector3[] vertices = mesh.vertices;

			// create new colors array where the colors will be created.
			Color[] colors = new Color[vertices.Length];

			for (int i = 0; i < vertices.Length; i++) {

				//colors [i] = Color.Lerp (Color.red, Color.green, vertices [i].y);
				byte red = (byte)Random.Range (0f, 255f);
				byte green = (byte)Random.Range (0f, 255f);
				byte blue = (byte)Random.Range (0f, 255f);
				colors [i] = new Color32 (red, green, blue, 255);
			}
			// assign the array of colors to the Mesh.
			mesh.colors = colors;
		}
	}

	public void SetObjectColor(int type) 
	{
		if (baseSprite != null) {
			mRed = (float)Random.Range (0f, 255f);
			mGreen = (float)Random.Range (0f, 255f);
			mBlue = (float)Random.Range (0f, 255f);
			baseSprite.GetComponent<Renderer> ().material.color = new Color32 ((byte)mRed, (byte)mGreen, (byte)mBlue, (byte)mAlpha);
		}
	}


}

