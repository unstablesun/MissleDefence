
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;
using DG.Tweening;

using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;


public class MeteorSpriteObj : PooledObject 
{
	private Material _mat;

	[HideInInspector]
	public Color StartColor;
	[HideInInspector]
	public Color TravelColor;
	[HideInInspector]
	public Color ExplodeColor;

	// Use this for initialization
	void Start () 
	{

	}

	public void Init () 
	{

	}

	public void StartAnim (float endx, float endy, float z, float speed) 
	{
		//GetComponent<Renderer> ().material.color = new Color32 ((byte)grey, (byte)grey, (byte)grey, (byte)alpha);

		_mat = GetComponent<SpriteRenderer> ().material;
		_mat.color = StartColor;

		Sequence mySequence = DOTween.Sequence().SetLoops(-1, LoopType.Restart);
		mySequence.Append(transform.DOMove(new Vector3(endx, endy, z), speed));
		mySequence.Join (_mat.DOColor(TravelColor, speed));
		mySequence.Append(transform.DOScale(new Vector3(4, 4, 1), 2));
		mySequence.Join (_mat.DOColor(ExplodeColor, 0.5f));
		mySequence.Join (_mat.DOFade(0,2));


	}


	// Update is called once per frame
	void Update () 
	{

	}
}

