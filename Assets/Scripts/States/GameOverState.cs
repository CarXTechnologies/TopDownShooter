using System.Collections;
using UnityEngine;

namespace TopDownShooter.States
{
	public class GameOverState : GameStateBehavior
	{
		protected override void OnEnter()
		{
			StartCoroutine(ExitToMainMenu());
		}

		private IEnumerator ExitToMainMenu()
		{
			yield return new WaitForSecondsRealtime(3f);
			
			GameController.LoadScene("MainMenu");
		}
	}
}