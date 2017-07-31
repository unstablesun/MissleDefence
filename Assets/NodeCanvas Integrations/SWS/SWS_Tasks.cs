using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SWS;

namespace NodeCanvas.Tasks.SWS{

	[Category("SWS")]
	[Icon("SWS", true)]
	public class StartMovement : ActionTask<Transform> {
		protected override void OnExecute(){
			agent.SendMessage("StartMove");
			EndAction();
		}
	}

	[Category("SWS")]
	[Icon("SWS", true)]
	public class StopMovement : ActionTask<Transform> {
		protected override void OnExecute(){
			agent.SendMessage("Stop");
			EndAction();
		}
	}

	[Category("SWS")]
	[Icon("SWS", true)]
	public class PauseMovement : ActionTask<Transform> {
		protected override void OnExecute(){
			agent.SendMessage("Pause");
			EndAction();
		}
	}

	[Category("SWS")]
	[Icon("SWS", true)]
	public class ResumeMovement : ActionTask<Transform> {
		protected override void OnExecute(){
			agent.SendMessage("Resume");
			EndAction();
		}
	}

	[Category("SWS")]
	[Icon("SWS", true)]
	public class ResetMovement : ActionTask<Transform> {
		protected override void OnExecute(){
			agent.SendMessage("ResetToStart");
			EndAction();
		}
	}

	[Category("SWS")]
	[Icon("SWS", true)]
	public class ChangeSpeed : ActionTask<Transform> {
		public BBParameter<float> newSpeed;
		protected override void OnExecute(){
			agent.SendMessage("ChangeSpeed", newSpeed.value);
			EndAction();
		}
	}

	[Category("SWS")]
	[Icon("SWS", true)]
	public class SetPath : ActionTask<Transform> {
		[RequiredField]
		public BBParameter<PathManager> path;
		protected override void OnExecute(){
			agent.SendMessage("SetPath", path.value);
			EndAction();
		}
	}

	[Category("SWS")]
	[Icon("SWS", true)]
	public class SetWaypointPosition : ActionTask {
		[RequiredField]
		public BBParameter<PathManager> path;
		public BBParameter<int> index;
		public BBParameter<Vector3> position;
		protected override void OnExecute(){
            
            if (index.value > path.value.waypoints.Length - 1){
                index.value = path.value.waypoints.Length - 1;
            }

			path.value.waypoints[index.value].position = position.value;
			EndAction();
		}
	}

	[Category("SWS")]
	[Icon("SWS", true)]
	public class GetPathWaypointObject : ActionTask{
		[RequiredField]
		public BBParameter<PathManager> path;
		public BBParameter<int> index;
		[BlackboardOnly]
		public BBParameter<GameObject> saveAs;
		protected override void OnExecute(){

            if (index.value > path.value.waypoints.Length - 1){
                index.value = path.value.waypoints.Length - 1;
            }

            saveAs.value = path.value is BezierPathManager? (path.value as BezierPathManager).bPoints[index.value].wp.gameObject : path.value.waypoints[index.value].gameObject;
            EndAction();
		}
	}
}