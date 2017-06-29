using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.IO;
using System;

[System.Serializable]
public class ProgramEntry
{
	[System.Serializable]
	public class AttackEntry
	{
		public float delay = 0f;
		public string id = "shipnameX";
		public GameObject startingPointRaw;
		public Vector3 startingPointV3;
	}

	[System.Serializable]
	public class LaunchEntry
	{
		public float durration = 8f; //till next entry triggers
		public AttackEntry[] entry;
	}

	[SerializeField]
	public LaunchEntry mLaunchEntry;



}
