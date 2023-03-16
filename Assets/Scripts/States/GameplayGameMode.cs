using System;
using UnityEngine.SceneManagement;

namespace TopDownShooter.States
{
	public class GameplayGameMode : GameModeBehavior
	{
		public void GotoGameplay()
		{
			ChangeState<GameplayState>();
		}
		
		public void GotoPasuse()
		{
			ChangeState<PauseState>();
		}

		public void GotoMainMenu()
		{
			GameController.LoadScene("MainMenu");
		}

		public void ReloadLevel()
		{
			var scene = SceneManager.GetActiveScene();
			GameController.LoadScene(scene.name);
		}
	}
}