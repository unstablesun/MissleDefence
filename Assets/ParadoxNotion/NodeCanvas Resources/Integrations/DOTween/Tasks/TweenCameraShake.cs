using DG.Tweening;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions{

	[Category("Tween")]
	[Icon("DOTTween", true)]
	public class TweenCameraShake : ActionTask<Camera> {

		public BBParameter<float>   duration;
		public BBParameter<float>   strength = 3f;
		public BBParameter<int>   	vibrato = 10;
		public BBParameter<float>   randomness = 90f;
		public BBParameter<bool>   	fadeout = true;
		public BBParameter<float>   delay = 0f;
		public Ease                 easeType = Ease.Linear;
		public bool                 waitActionFinish = true;

		private string id;

		protected override void OnExecute() {

			var tween = agent.DOShakePosition(duration.value, strength.value, vibrato.value, randomness.value, fadeout.value);
			tween.SetDelay(delay.value);
			tween.SetEase(easeType);
			id = System.Guid.NewGuid().ToString();
			tween.SetId(id);

			if (!waitActionFinish) EndAction();


		}	

		protected override void OnUpdate() {
			if (elapsedTime >= duration.value + delay.value){
				EndAction();
			}
		}

		protected override void OnStop(){
			if (waitActionFinish){
				DG.Tweening.DOTween.Kill(id);
			}
		}

	}
}