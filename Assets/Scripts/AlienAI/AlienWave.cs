using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

[System.Serializable]
public class AlienWave 
{
	[SerializeField]
	public string _waveName = "WaveX";

	[SerializeField]
	public float _launchTime;

	[SerializeField]
	public GameObject[] _attackArray;

}
