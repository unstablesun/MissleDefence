using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;


//------------------------------------------------------
/*
 				Canon Object Loaded
*/
//------------------------------------------------------
public partial class SpriteCanonController : MonoBehaviour 
{
	private void LoadCanonSprites()
	{
		for (int t = 0; t < objectPoolSize; t++) {

			GameObject _sfObj = Instantiate (Resources.Load ("Prefabs/SpriteCanonObject", typeof(GameObject))) as GameObject;

			if (_sfObj != null) {

				if (SpriteCanonObjectContainer != null) {
					_sfObj.transform.parent = SpriteCanonObjectContainer.transform;
				}
				_sfObj.name = "canonObj" + t.ToString ();

				//default storage location
				_sfObj.transform.position = new Vector2 (StoragePosition.transform.position.x, StoragePosition.transform.position.y);

				SpriteCanonObject objectScript = _sfObj.GetComponent<SpriteCanonObject> ();
				objectScript.ID = t;
				objectScript.velocity = 0f;
				objectScript.SetBaseSpriteScale (0.25f, 0.25f);

				SpriteCanonObjectList.Add (_sfObj);

			} else {

				Debug.Log ("Couldn't load super sprite prefab");
			}
		}
	}
}
