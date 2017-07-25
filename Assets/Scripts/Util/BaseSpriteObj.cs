using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;
using DG.Tweening;

using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;


public class BaseSpriteObj : PooledObject 
{

	// Use this for initialization
	void Start () 
	{
		
	}

	public void Init () 
	{

	}

	public void StartAnimTest (float dx, float dy, float dz) 
	{
		float offsetx = (float)Random.Range (0f, 8f);
		float speed = (float)Random.Range (12f, 20f);

		transform.DOMove(new Vector3(26f + offsetx, 0, 0), speed).SetRelative().SetLoops(-1, LoopType.Restart);
	}


	// Update is called once per frame
	void Update () 
	{
		
	}
}
