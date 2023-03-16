using UnityEngine;

namespace TopDownShooter.States
{
	public class MainMenuGameMode : GameModeBehavior
	{
		[SerializeField] private PlayerProfileSO m_playerProfile;
		[SerializeField] private LevelsInfo m_levelsInfo;

		public void GotoSelectLevel()
		{
			ChangeState<SelectLevelState>();
		}
		
		public void GotoUpgrade()
		{
			ChangeState<CharacterUpgradeState>();
		}

		public void LoadLevel()
		{
			var levelInfo = m_levelsInfo.GetLevel(m_playerProfile.lastLevelIndex);
			GameController.LoadScene(levelInfo.sceneName);
		}
	}
}