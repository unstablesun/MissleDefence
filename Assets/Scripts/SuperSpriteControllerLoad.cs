using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;



//------------------------------------------------------
/*
 				Super Sprite Controller Load
*/
//------------------------------------------------------
public partial class SuperSpriteController : MonoBehaviour 
{
	private void LoadSuperSprites()
	{
		for (int t = 0; t < objectPoolSize; t++) {

			GameObject _sfObj = Instantiate (Resources.Load ("Prefabs/SuperSpriteObject", typeof(GameObject))) as GameObject;

			if (_sfObj != null) {

				if (SuperSpriteObjectContainer != null) {
					_sfObj.transform.parent = SuperSpriteObjectContainer.transform;
				}
				_sfObj.name = "superObj" + t.ToString ();

				float spacePosX = (float)UnityEngine.Random.Range (-mFieldVariables.spaceDeltaX, mFieldVariables.spaceDeltaX);
				float spacePosY = (float)UnityEngine.Random.Range (-mFieldVariables.spaceDeltaY, mFieldVariables.spaceDeltaY);

				_sfObj.transform.position = new Vector2 (CenterPoint.transform.position.x + spacePosX, CenterPoint.transform.position.y +spacePosY);


				SuperSpriteObject objectScript = _sfObj.GetComponent<SuperSpriteObject> ();
				objectScript.ID = t;
				objectScript.velocity = 100f;
				objectScript.SetBaseSpriteScale (0.25f, 0.25f);


				SuperSpriteObjectList.Add (_sfObj);

			} else {

				Debug.Log ("Couldn't load super sprite prefab");
			}


		}
	}


}
	