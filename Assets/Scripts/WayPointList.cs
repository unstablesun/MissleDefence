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
	public bool HasData = false;

	[SerializeField]
	public Vector3[] mWayPointList = null;

	[SerializeField]
	public WayPoint[] mWayPointData = null;


	public void InitList(int size)
	{
		mWayPointList = new Vector3[size];

		mWayPointData = new WayPoint[size];
	}

	public void AddVector3(Vector3 vec, int index)
	{
		mWayPointList[index] = new Vector3(vec.x, vec.y, vec.z);            
	}

	public Vector3 GetVector3AtIndex(int index)
	{
		return( mWayPointList[index] );            
	}

	public void AddWayPointData(WayPoint wp, int index)
	{
		mWayPointData[index] = wp;            
	}

	public WayPoint GetWayPointDataAtIndex(int index)
	{
		return( mWayPointData[index] );            
	}



	public void AddOffsetToPointList(Vector3 vecAdd)
	{
		for (int i = 0; i < NumPointsUsed; i++) {

			Vector3 vecOld = mWayPointList [i];

			Vector3 vecNew = vecOld + vecAdd;
			mWayPointList [i] = new Vector3 (vecNew.x, vecNew.y, vecNew.z);
		}
	}



}
