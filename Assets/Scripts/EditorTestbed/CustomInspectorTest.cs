using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.IO;
using System;

public class CustomInspectorTest : MonoBehaviour 
{
	//container only??

	/*
	[SerializeField]
	public int NumEntriesUsed = 0;

	public int Level;


	[SerializeField]
	private ColorSet mColorContainer;
	*/

	public ColorSet mColorContainer;


	public ProgramEntry ProgramEntryWave1;
	public ProgramEntry ProgramEntryWave2;
	public ProgramEntry ProgramEntryWave3;
	public ProgramEntry ProgramEntryWave4;
	public ProgramEntry ProgramEntryWave5;

	public ProgramEntry ProgramEntryWave6;
	public ProgramEntry ProgramEntryWave7;
	public ProgramEntry ProgramEntryWave8;
	public ProgramEntry ProgramEntryWave9;
	public ProgramEntry ProgramEntryWave10;

	public ProgramEntry ProgramEntryWave11;
	public ProgramEntry ProgramEntryWave12;
	public ProgramEntry ProgramEntryWave13;
	public ProgramEntry ProgramEntryWave14;
	public ProgramEntry ProgramEntryWave15;


	public ProgramEntry ProgramEntryWave16;
	public ProgramEntry ProgramEntryWave17;
	public ProgramEntry ProgramEntryWave18;
	public ProgramEntry ProgramEntryWave19;
	public ProgramEntry ProgramEntryWave20;

	public ProgramEntry ProgramEntryWave21;
	public ProgramEntry ProgramEntryWave22;
	public ProgramEntry ProgramEntryWave23;
	public ProgramEntry ProgramEntryWave24;
	public ProgramEntry ProgramEntryWave25;

	//[HideInInspector]
	public Vector3 mVectorTest;


	void Awake()
	{

	}


	public void SetTestVector(Vector3 v)
	{
		mVectorTest = new Vector3(v.x, v.y, v.z);
	}



}
