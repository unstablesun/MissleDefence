using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.IO;
using System;

[System.Serializable]
public class WayPoint
{

	[SerializeField]
	private float _waitTime = 0f;
	[SerializeField]
	private float _velocity = 0f;
	[SerializeField]
	private float _acceleration = 0f;
	[SerializeField]
	private float _decelAtPercentOfMag = 0f;
	[SerializeField]
	public float _decelaration = 0f;
	[SerializeField]
	public float _minVelocity = 0f;
	[SerializeField]
	public float _chanceToReverse = 0f;
	[SerializeField]
	public float _chanceToSkip = 0f;


	public float WaitTime
	{
		get { return _waitTime; }
		set { _waitTime = value; }
	}

	public float Velocity
	{
		get { return _velocity; }
		set { _velocity = value; }
	}

	public float Acceleration
	{
		get { return _acceleration; }
		set { _acceleration = value; }
	}

	public float DecelAtPercentOfMag
	{
		get { return _decelAtPercentOfMag; }
		set { _decelAtPercentOfMag = value; }
	}

	public float Decelaration
	{
		get { return _decelaration; }
		set { _decelaration = value; }
	}

	public float MinVelocity
	{
		get { return _minVelocity; }
		set { _minVelocity = value; }
	}

	public float ChanceToReverse
	{
		get { return _chanceToReverse; }
		set { _chanceToReverse = value; }
	}

	public float ChanceToSkip
	{
		get { return _chanceToSkip; }
		set { _chanceToSkip = value; }
	}

}
