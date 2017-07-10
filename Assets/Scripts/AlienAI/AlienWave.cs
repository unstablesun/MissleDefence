using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;


public class AlienWave : MonoBehaviour
{
	public string PrefabName;
	public int NumWaves;
	public int NumSquadsPer;


	public ProgramEntry[] mProgramEntries = null;

	private int mEntryCount = 0;

	void Awake()
	{
	}

	void Start()
	{
	}


	public void InitProgramEntries(int size) 
	{
		mProgramEntries = new ProgramEntry[size];
		mEntryCount = 0;
	}

	public void AddProgramEntry(ProgramEntry entry) 
	{

		mProgramEntries [mEntryCount] = entry;
		ProgramEntry pe = mProgramEntries [mEntryCount];
		mEntryCount++;

		int size = pe.mLaunchEntry.entry.Length;

		for (int e = 0; e < size; e++) {

			pe.mLaunchEntry.entry [e].startingPointV3 = pe.mLaunchEntry.entry [e].startingPointRaw.transform.position;
		}
			
	}

	public ProgramEntry GetProgramEntryAtIndex(int index)
	{
		return( mProgramEntries[index] );            
	}

}
