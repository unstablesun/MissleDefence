using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class AlienDataManager : MonoBehaviour 
{

	public static AlienDataManager Instance;

	void Awake()
	{
		Instance = this;
	}

	//void Start() 
	//{}

	//void Update() 
	//{}


	//----------------------------------------------------
	/*
	 * 					Color Tables
	*/
	//----------------------------------------------------

	public enum eColorOrder 
	{
		Primary,
		Secondary,
		Tertiary, 
		Quaternary,
		Quinary
	};

	public enum eColorShade 
	{
		black,
		blue,
		green, 
		red,
		yellow
	};



	[SerializeField]
	public ColorSet[] mColorThemesRed = null;
	[SerializeField]
	public ColorSet[] mColorThemesGreen = null;
	[SerializeField]
	public ColorSet[] mColorThemesBlue = null;
	[SerializeField]
	public ColorSet[] mColorThemesYellow = null;
	[SerializeField]
	public ColorSet[] mColorThemesPurple = null;
	[SerializeField]
	public ColorSet[] mColorThemesContrast = null;

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

	public ColorSet FindColorSetFromShadeArray(eColorShade shade)
	{

		if (shade == eColorShade.blue) {
			int shadeArrayLength = mColorThemesBlue.GetLength (0);
			int shadeIndex = (int)UnityEngine.Random.Range (0, shadeArrayLength);
			return mColorThemesBlue [shadeIndex];
		} else if(shade == eColorShade.green) {
			int shadeArrayLength = mColorThemesGreen.GetLength (0);
			int shadeIndex = (int)UnityEngine.Random.Range (0, shadeArrayLength);
			return mColorThemesGreen [shadeIndex];
		} else if(shade == eColorShade.red) {
			int shadeArrayLength = mColorThemesRed.GetLength (0);
			int shadeIndex = (int)UnityEngine.Random.Range (0, shadeArrayLength);
			return mColorThemesRed [shadeIndex];
		}
			
		return null;
	}










	public enum eModuleStatType 
	{
		drone,
		crusier,
		battleShip, 
		bomb,
		missle
	};



	[SerializeField]
	public ModuleData[] mAlienModuleData = null;

	[System.Serializable]
	public class ModuleData
	{
		[SerializeField]
		private eModuleStatType mModuleStatType = eModuleStatType.drone;
		[SerializeField]
		private float mHitPoints = 0f;
		[SerializeField]
		private float mInteralPower = 0f;
		[SerializeField]
		private GameObject mPrimarySprite = null;
		[SerializeField]
		private GameObject mSecondarySprite = null;
		[SerializeField]
		private GameObject mTertiarySprite = null;


		public eModuleStatType ModuleStatType
		{
			get { return mModuleStatType; }
			set { mModuleStatType = value; }
		}

		public float HitPoints
		{
			get { return mHitPoints; }
			set { mHitPoints = value; }
		}

		public float InteralPower
		{
			get { return mInteralPower; }
			set { mInteralPower = value; }
		}

		public GameObject PrimarySprite
		{
			get { return mPrimarySprite; }
			set { mPrimarySprite = value; }
		}

		public GameObject SecondarySprite
		{
			get { return mSecondarySprite; }
			set { mSecondarySprite = value; }
		}

		public GameObject TertiarySprite
		{
			get { return mTertiarySprite; }
			set { mTertiarySprite = value; }
		}


	}




}