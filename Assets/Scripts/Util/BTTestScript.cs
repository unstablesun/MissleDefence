
using UnityEngine;
using NodeCanvas.BehaviourTrees;

public class BTTestScript : MonoBehaviour {

	public int count;
	public BehaviourTreeOwner agent;
	public BehaviourTree bt;

	void Start(){

		for (int i = 0; i < count; i++){
			var newAgent = (BehaviourTreeOwner)Instantiate(agent);
			newAgent.StartBehaviour(bt);
		}
	}
}
