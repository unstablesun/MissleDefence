using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.IO;
using System;

public class ProgramList : MonoBehaviour 
{
	[SerializeField]
	public string PrefabName = "ProgramListX";

	[SerializeField]
	public int NumEntriesUsed = 0;

	[System.Serializable]
	public class WaveEntry
	{
		public ProgramEntry[] mProgramList = null;
	}

	[SerializeField]
	public WaveEntry[] mWaveList = null;

	public void InitList(int size)
	{

		mWaveList = new WaveEntry[size];
	}


	public void AddProgramData(WaveEntry we, int index)
	{
		//fix up vectors
		int wSize = mWaveList.Length;

		for (int w = 0; w < wSize; w++) {

			int eSize = we.mProgramList [w].mLaunchEntry.entry.Length;

			for (int e = 0; e < eSize; e++) {
				
				we.mProgramList [w].mLaunchEntry.entry [e].startingPointV3 = we.mProgramList [w].mLaunchEntry.entry [e].startingPointRaw.transform.position;
			}
		}

		mWaveList[index] = we;            
	}

	public WaveEntry GetWaveEntryDataAtIndex(int index)
	{
		return( mWaveList[index] );            
	}



	/*
	public void DebugPrintList(string label)
	{
		DebugPrintBuffer.Instance.addToDPrintBuffer ("...");
		DebugPrintBuffer.Instance.addToDPrintBuffer (label);
		for (int i = 0; i < NumEntriesUsed; i++) {

			Vector3 vec = mWayPointList [i];
			string log = "vec #" + i.ToString () + "                         X = " + vec.x.ToString () + "    Y = " + vec.y.ToString ();
			DebugPrintBuffer.Instance.addToDPrintBuffer (log);

		}
		DebugPrintBuffer.Instance.flushDPrintBuffer ();
	}
	*/

}
