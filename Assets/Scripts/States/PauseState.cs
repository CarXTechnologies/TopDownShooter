using UnityEngine;

namespace TopDownShooter.States
{
	public class PauseState : GameStateBehavior
	{
		protected override void OnEnter()
		{
			Time.timeScale = 0f;
		}

		protected override void OnExit()
		{
			Time.timeScale = 1f;
		}
	}
}