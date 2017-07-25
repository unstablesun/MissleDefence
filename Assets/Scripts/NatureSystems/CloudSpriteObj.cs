using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;
using DG.Tweening;

using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;


public class CloudSpriteObj : PooledObject 
{

	// Use this for initialization
	void Start () 
	{

	}

	public void Init () 
	{

	}

	public void StartAnim (float endx, float endy, float z, float offsetx, float speed) 
	{
		transform.DOMove(new Vector3(endx + offsetx, endy, z), speed).SetLoops(-1, LoopType.Restart);
	}


	// Update is called once per frame
	void Update () 
	{

	}
}
