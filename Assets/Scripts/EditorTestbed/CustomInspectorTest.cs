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

	//[HideInInspector]
	public Vector3 mVectorTest;




	public void SetTestVector(Vector3 v)
	{
		mVectorTest = new Vector3(v.x, v.y, v.z);
	}



}
