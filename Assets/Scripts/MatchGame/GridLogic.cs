using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLogic : MonoBehaviour 
{
	public enum eGridState 
	{
		NoOp,
		Init,
		Wait,
		Diagnostic,
		EmptyScan,
		Calc,
		Dead
	};
	public eGridState _State = eGridState.NoOp;

	public List <GameObject> GemObjectList = null;

	public static GridLogic Instance;

	void Awake () 
	{
		Instance = this;

	}

	void Start () 
	{
		_State = eGridState.Init;

	}
	
	void Update () 
	{
		switch( _State )
		{
		case eGridState.NoOp:
			break;
		case eGridState.Init:
			_State = eGridState.Diagnostic;
			break;
		case eGridState.Wait:
			break;
		case eGridState.Diagnostic:
			
			//FillEmptyDiagnostic ();

			FillPreconfigDiagnostic ();

			TryMatchDiagnostic ();

			_State = eGridState.Wait;

			break;
		case eGridState.EmptyScan:
			break;
		case eGridState.Calc:
			break;
		case eGridState.Dead:
			break;
		}
		
	}


	private void FillEmptyDiagnostic () 
	{
		int count = GemObjectList.Count;

		Debug.Log("FillEmptyDiagnostic");
		HexManager.Instance.SetScanSetting(0);

		GameObject go = HexManager.Instance.QueryScanNextHex();

		while(go != null)
		{

			HexObject objectScript = go.GetComponent<HexObject> ();

			if(objectScript.NoGemAttached()) {
			
				GameObject gem = GemManager.Instance.QueryGetAvailableObject();

				if(gem != null) {

					int colorType = (int)Random.Range (0, count);

					GemObject gemScript = gem.GetComponent<GemObject> ();
					gemScript.SetGemSprite(GemObjectList[colorType], (GemObject.eColorType) colorType);

					objectScript.AttachGem(gem);
				}
			}
				
			go = HexManager.Instance.QueryScanNextHex();
		}

	}



	int[] PreConfigBoard1 = 
	{ 
		0, 1, 2, 3, 4, 
		4, 3, 2, 1, 0, 
		0, 1, 2, 3, 4, 
		4, 3, 2, 1, 0, 
		0, 1, 2, 3, 4 
	};

	int[] PreConfigBoard2 = 
	{ 
		4, 0, 2, 0, 0, 
		0, 3, 0, 4, 3, 
		1, 2, 1, 3, 1, 
		0, 2, 2, 0, 0, 
		1, 1, 0, 0, 1 
	};

	private void FillPreconfigDiagnostic () 
	{
		int count = GemObjectList.Count;

		Debug.Log("FillEmptyDiagnostic");
		HexManager.Instance.SetScanSetting(0);

		GameObject go = HexManager.Instance.QueryScanNextHex();

		int index = 0;
		while(go != null)
		{

			HexObject objectScript = go.GetComponent<HexObject> ();

			if(objectScript._Type == HexObject.eType.Main && objectScript.NoGemAttached()) {

				GameObject gem = GemManager.Instance.QueryGetAvailableObject();

				if(gem != null) {

					int colorType = PreConfigBoard2[index++];

					GemObject gemScript = gem.GetComponent<GemObject> ();
					gemScript.SetGemSprite(GemObjectList[colorType], (GemObject.eColorType) colorType);

					objectScript.AttachGem(gem);
				}
			}

			go = HexManager.Instance.QueryScanNextHex();
		}

	}



	private void TryMatchDiagnostic ()
	{
		HexManager.Instance.QueryScanAndMark ();

		HexManager.Instance.QueryShowMarkedHexes ();
	}
		

}
