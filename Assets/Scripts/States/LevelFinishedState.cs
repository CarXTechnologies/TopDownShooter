using System.Collections;
using System.Collections.Generic;
using TopDownShooter.States;
using UnityEngine;

namespace TopDownShooter
{
	public class LevelFinishedState : GameStateBehavior
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