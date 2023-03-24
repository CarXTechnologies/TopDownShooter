using System.Collections;
using UnityEngine;

namespace TopDownShooter.States
{
	public class GameOverState : GameStateBehavior
	{
		[SerializeField] private GameplayGameMode m_gameMode;

		protected override void OnEnter()
		{
			StartCoroutine(ExitToMainMenu());
		}

		private IEnumerator ExitToMainMenu()
		{
			yield return new WaitForSecondsRealtime(3f);

			m_gameMode.GotoMainMenu();
		}
	}
}