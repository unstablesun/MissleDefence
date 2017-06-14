using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.IO;
using System;

public class WayPointList : MonoBehaviour 
{
	[SerializeField]
	public string PrefabName = "WayPointX";

	[SerializeField]
	public int NumPointsUsed = 0;

	[SerializeField]
	public Vector3[] mWayPointList = null;

	public void InitList(int size)
	{
		mWayPointList = new Vector3[size];
	}

	public void AddVector3(Vector3 vec, int index)
	{
		mWayPointList[index] = new Vector3(vec.x, vec.y, vec.z);            
	}

	public Vector3 GetVector3AtIndex(int index)
	{
		return( mWayPointList[index] );            
	}


}
