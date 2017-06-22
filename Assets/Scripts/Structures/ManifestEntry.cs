using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.IO;
using System;

[System.Serializable]
public class ManifestEntry
{

	[SerializeField]
	private string _prefabName;
	[SerializeField]
	private int _numToLoad = 0;
	[SerializeField]
	private WayPointList _startingPoints;


	public string PrefabName
	{
		get { return _prefabName; }
		set { _prefabName = value; }
	}

	public int NumToLoad
	{
		get { return _numToLoad; }
		set { _numToLoad = value; }
	}

	public WayPointList StartingPoints
	{
		get { return _startingPoints; }
		set { _startingPoints = value; }
	}

}
