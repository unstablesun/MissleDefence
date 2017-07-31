using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas;

public class TriggerActions : MonoBehaviour 
{

	void Start () 
	{
		GetComponent<ActionListPlayer> ().Play ();
	}
	
}
