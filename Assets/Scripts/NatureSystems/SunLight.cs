using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SunLight : MonoBehaviour 
{
	public Color[] ColorList = null;



	void Start () 
	{
		Light lite = GetComponent<Light> ();

		lite.color = ColorList [0];
		Sequence mySequence = DOTween.Sequence().SetLoops(-1, LoopType.Yoyo);

		mySequence.Append (lite.DOColor (ColorList [1], 5));
		mySequence.Append (lite.DOColor (ColorList [2], 5));

	}
	
	void Update () 
	{
		
	}
}
