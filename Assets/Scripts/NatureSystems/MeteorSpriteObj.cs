﻿
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
	public TrailRenderer JetTrail;

	private Material _mat;

	[HideInInspector]
	public Color StartColor;
	[HideInInspector]
	public Color TravelColor;
	[HideInInspector]
	public Color ExplodeColor;


	void Start () 
	{

	}


	public void StartAnim (float endx, float endy, float z, float speed) 
	{
		JetTrail.Clear();

		_mat = GetComponent<SpriteRenderer> ().material;
		_mat.color = StartColor;

		Sequence mySequence = DOTween.Sequence().SetLoops(1, LoopType.Restart);
		mySequence.Append(transform.DOMove(new Vector3(endx, endy, z), speed));
		mySequence.Join (_mat.DOColor(TravelColor, speed));
		mySequence.Append(transform.DOScale(new Vector3(4, 4, 1), 2));
		mySequence.Join (_mat.DOColor(ExplodeColor, 0.5f));
		mySequence.Join (_mat.DOFade(0,2));
		mySequence.Append(transform.DOScale(new Vector3(1, 1, 1), 0.2f)).OnComplete(SequenceComplete);

	}


	// Update is called once per frame
	void Update () 
	{

	}


	void SequenceComplete () 
	{
		//Debug.Log ("SEQUENCE COMPLETE!");


		gameObject.SetActive(false);
	}

}

