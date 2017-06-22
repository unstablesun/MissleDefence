using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.IO;
using System;

public class ManifestList : MonoBehaviour 
{
	
	[SerializeField]
	public string PrefabName = "ManifestListX";

	[SerializeField]
	public int NumEntriesUsed = 0;

	[SerializeField]
	public ManifestEntry[] mManifestEntries = null;


	public void InitList(int size)
	{
		mManifestEntries = new ManifestEntry[size];
	}
		
	public void AddManifestEntry(ManifestEntry me, int index)
	{
		mManifestEntries[index] = me;            
	}

	public ManifestEntry GetManifestEntryAtIndex(int index)
	{
		return( mManifestEntries[index] );            
	}
		
}
