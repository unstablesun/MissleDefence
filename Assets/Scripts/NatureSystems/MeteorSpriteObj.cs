
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

	public float Power = 1f;
	public float Defence = 10f;


	private Material _mat;

	[HideInInspector]
	public Color StartColor;
	[HideInInspector]
	public Color TravelColor;
	[HideInInspector]
	public Color ExplodeColor;



	[HideInInspector]
	public bool ExplodePhase { get; set; }

	private Sequence meteorSequence = null;

	void Start () 
	{
	}

	public void ApplyDamage (float damage) 
	{
		Defence -= damage;

		if (Defence <= 0f) {

			//kill sequence, set effects
			if (meteorSequence != null) {

				meteorSequence.Complete ();

			}
		}

	}



	public void StartAnim (float endx, float endy, float z, float speed) 
	{
		ExplodePhase = false;

		JetTrail.Clear();

		_mat = GetComponent<SpriteRenderer> ().material;
		_mat.color = StartColor;

		meteorSequence = DOTween.Sequence().SetLoops(1, LoopType.Restart);
		meteorSequence.Append(transform.DOMove(new Vector3(endx, endy, z), speed).OnComplete(TravelComplete));
		meteorSequence.Join (_mat.DOColor(TravelColor, speed));
		meteorSequence.Append(transform.DOScale(new Vector3(4, 4, 1), 2));
		meteorSequence.Join (_mat.DOColor(ExplodeColor, 0.5f));
		meteorSequence.Join (_mat.DOFade(0,2));
		meteorSequence.Append(transform.DOScale(new Vector3(1, 1, 1), 0.2f)).OnComplete(SequenceComplete);

	}


	// Update is called once per frame
	void Update () 
	{

	}

	void TravelComplete () 
	{
		//Debug.Log ("TRAVEL COMPLETE!");

		ExplodePhase = true;
	}

	void SequenceComplete () 
	{
		//Debug.Log ("SEQUENCE COMPLETE!");
		ExplodePhase = false;

		gameObject.SetActive(false);
	}


}

