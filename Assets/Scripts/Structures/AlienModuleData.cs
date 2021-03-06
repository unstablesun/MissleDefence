using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.IO;
using System;

[System.Serializable]
public class AlienModuleData
{
	public enum eModuleStatType 
	{
		drone,
		crusier,
		battleShip, 
		bomb,
		missle
	};

	[SerializeField]
	public string _prefabName = "AlienModuleX";
	[SerializeField]
	private eModuleStatType mModuleStatType = eModuleStatType.drone;
	[SerializeField]
	private float mHitPoints = 0f;
	[SerializeField]
	private float mInteralPower = 0f;
	[SerializeField]
	private Sprite mPrimarySprite = null;
	[SerializeField]
	private Sprite mSecondarySprite = null;
	[SerializeField]
	private Sprite mTertiarySprite = null;
	[SerializeField]
	public WayPointList _wayPointList;
	[SerializeField]
	public ColorSet _colorSet;

	public string PrefabName
	{
		get { return _prefabName; }
		set { _prefabName = value; }
	}

	public eModuleStatType ModuleStatType
	{
		get { return mModuleStatType; }
		set { mModuleStatType = value; }
	}

	public float HitPoints
	{
		get { return mHitPoints; }
		set { mHitPoints = value; }
	}

	public float InteralPower
	{
		get { return mInteralPower; }
		set { mInteralPower = value; }
	}

	public Sprite PrimarySprite
	{
		get { return mPrimarySprite; }
		set { mPrimarySprite = value; }
	}

	public Sprite SecondarySprite
	{
		get { return mSecondarySprite; }
		set { mSecondarySprite = value; }
	}

	public Sprite TertiarySprite
	{
		get { return mTertiarySprite; }
		set { mTertiarySprite = value; }
	}

	public WayPointList WayPointList
	{
		get { return _wayPointList; }
		set { _wayPointList = value; }
	}

	public ColorSet ColorSet
	{
		get { return _colorSet; }
		set { _colorSet = value; }
	}
}
