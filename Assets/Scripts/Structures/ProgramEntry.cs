using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.IO;
using System;

[System.Serializable]
public class ProgramEntry
{
	public enum AttackModule
	{
		None = 0,
		Drone1,
		Drone2,
		Ship1,
		Ship2,
		Ship3,
		Ship4,
		Scout,
		Bomb,
	};

	[System.Serializable]
	public class AttackEntry
	{
		public float delay = 0f;
		//public string id = "shipnameX";
		public AttackModule AttackID = AttackModule.Drone1;
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
