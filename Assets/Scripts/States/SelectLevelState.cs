using UnityEngine;
using System.Collections.Generic;
namespace TopDownShooter.States
{
	public class SelectLevelState : GameStateBehavior
	{
		[SerializeField] private PlayerProfileSO m_playerProfile;
		[SerializeField] private LevelsInfo m_levelsInfo;
		[SerializeField] private UILevelInfo m_uiLevelInfo;

		private IReadOnlyList<LevelsInfo.Data> m_levelsData;

		protected override void OnEnter()
		{
			m_levelsData = m_levelsInfo.GetLevels();
			if (!TrySelectLevel(m_playerProfile.lastLevelIndex))
			{
				TrySelectLevel(0);
			}
		}

		public void NextLevel()
		{
			if (m_playerProfile.lastLevelIndex < m_levelsData.Count - 1)
			{
				TrySelectLevel(m_playerProfile.lastLevelIndex + 1);
			}
		}

		public void PrevLevel()
		{
			if (m_playerProfile.lastLevelIndex > 0)
			{
				TrySelectLevel(m_playerProfile.lastLevelIndex - 1);
			}
		}

		private bool TrySelectLevel(int index)
		{
			if (index >= 0 && index < m_levelsData.Count)
			{
				m_uiLevelInfo.SetInfo(m_levelsData[index]);
				m_playerProfile.lastLevelIndex = index;
				return true;
			}
			return false;
		}
	}
}