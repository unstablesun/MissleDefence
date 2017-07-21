using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NextGenSprites;

public class ModularAnimController : MonoBehaviour 
{
	public GameObject TargetSprite;
	public float TargetValue;
	public ShaderFloat FloatProperty;


	private Material _mat;

	void Start () 
	{
		if (TargetSprite != null) {

			_mat = TargetSprite.GetComponent<SpriteRenderer> ().material;
		}
	}


	public void ChangeFloatValue () 
	{

		_mat.SetFloat (FloatProperty.ToString (), TargetValue);

	}
		
	void Update () 
	{
	}
}
