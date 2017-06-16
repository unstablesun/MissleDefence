using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;


[System.Serializable]
public class ColorSet
{
	[SerializeField]
	private Color mPrimaryColor = Color.white;
	[SerializeField]
	private Color mSecondaryColor = Color.black;
	[SerializeField]
	private Color mTertiaryColor = Color.white;
	[SerializeField]
	private Color mQuaternaryColor = Color.black;
	[SerializeField]
	private Color mQuinaryColor = Color.white;

	public Color PrimaryColour
	{
		get { return mPrimaryColor; }
		set { mPrimaryColor = value; }
	}

	public Color SecondaryColour
	{
		get { return mSecondaryColor; }
		set { mSecondaryColor = value; }
	}

	public Color TertiaryColour
	{
		get { return mTertiaryColor; }
		set { mTertiaryColor = value; }
	}

	public Color QuaternaryColor
	{
		get { return mQuaternaryColor; }
		set { mQuaternaryColor = value; }
	}

	public Color QuinaryColor
	{
		get { return mQuinaryColor; }
		set { mQuinaryColor = value; }
	}
}
