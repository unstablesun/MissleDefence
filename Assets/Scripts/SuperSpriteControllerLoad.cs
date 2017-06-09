using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;



//------------------------------------------------------
/*
 				Cube Controller
*/
//------------------------------------------------------
public partial class SuperSpriteController : MonoBehaviour 
{
	private void LoadSuperSprites()
	{
		for (int t = 0; t < mFieldVariables.numSprites; t++) {

			GameObject _sfObj = Instantiate (Resources.Load ("Prefabs/SuperSpriteObject", typeof(GameObject))) as GameObject;

			if (_sfObj != null) {

				if (SuperSpriteObjectContainer != null) {
					_sfObj.transform.parent = SuperSpriteObjectContainer.transform;
				}
				_sfObj.name = "superObj" + t.ToString ();

				float spacePosX = (float)UnityEngine.Random.Range (-mFieldVariables.spaceDeltaX, mFieldVariables.spaceDeltaX);
				float spacePosY = (float)UnityEngine.Random.Range (-mFieldVariables.spaceDeltaY, mFieldVariables.spaceDeltaY);

				_sfObj.transform.position = new Vector2 (spacePosX, spacePosY);

				float scaleX = (float)UnityEngine.Random.Range (0.1f, 2f);
				float scaleY = (float)UnityEngine.Random.Range (0.1f, 2f);
				_sfObj.transform.localScale += new Vector3 (scaleX, scaleY, 1f);


				//_sfObj.transform.rotation = Quaternion.Euler(0, 0 + (float)t * 2, 0);

				SuperSpriteObject superSpriteObjectScript = _sfObj.GetComponent<SuperSpriteObject> ();
				superSpriteObjectScript.ID = t;
				superSpriteObjectScript.velocity = 100f;


				SuperSpriteObjectList.Add (_sfObj);
			} else {

				Debug.Log ("Couldn't load super sprite prefab");
			}


		}
	}


}
	