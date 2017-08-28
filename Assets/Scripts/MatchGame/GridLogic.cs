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
			
			FillEmptyDiagnostic();
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

			//is empty?
			if(objectScript.NoGemAttached()) {
			
				Debug.Log("NoGemAttached");
				//get available gem
				GameObject gem = GemManager.Instance.QueryGetAvailableObject();


				if(gem != null) {


					int sindex = (int)Random.Range (0, count);

					//select gem type id, example: red gem 
					GemObject gemScript = gem.GetComponent<GemObject> ();
					gemScript.SetGemSprite(GemObjectList[sindex]);


				
					Debug.Log("gem != null");
					//attach to hex
					objectScript.AttachGem(gem);
				}



			}




			go = HexManager.Instance.QueryScanNextHex();
		}

	}

}
