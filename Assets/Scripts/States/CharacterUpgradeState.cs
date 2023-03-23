using System;
using UnityEngine;

namespace TopDownShooter.States
{
	public class CharacterUpgradeState : GameStateBehavior
	{
		[SerializeField] private MainMenuGameMode m_gameModeBehavior;
		[SerializeField] private PlayerProfileSO m_playerProfile;
		[SerializeField] private UpgradeData m_upgradeData;
		[SerializeField] private UISkillUpgrade[] m_uiSkillsUpgrade;
		[SerializeField] private UIPlayerUpgrade m_uiPlayerUpgrade;

		private void Awake()
		{
			for (int i = 0; i < m_uiSkillsUpgrade.Length; ++i)
			{
				InitSkillUpgrade(m_uiSkillsUpgrade[i], i);
			}

			m_uiPlayerUpgrade.onTryUpgrade += OnTryPlayerUpgrade;
			m_uiPlayerUpgrade.Init(m_upgradeData.playerMaxLevel);
			RefreshPlayerUI();
		}

		private void OnTryPlayerUpgrade()
		{
			var level = m_playerProfile.playerLevel + 1;
			var data = m_upgradeData.GetPlayerUpgrade(level);

			if (m_playerProfile.TrySpendCoins(data.cost))
			{
				m_playerProfile.LevelUp();
				RefreshPlayerUI();
			}
			else
			{
				m_gameModeBehavior.GotoStore();
			}
		}

		private void InitSkillUpgrade(UISkillUpgrade item, int skillIndex)
		{
			item.onTryUpgrade += () => TryUpgradeSkill(skillIndex);
			var skillUpgrade = m_upgradeData.GetSkillUpgrade(skillIndex);
			item.Init(skillUpgrade.skill.icon, m_upgradeData.GetSkillMaxLevel(skillIndex));
		}

		private bool TryUpgradeSkill(int skillIndex)
		{
			var skillLevel = m_playerProfile.GetSkillLevel(skillIndex) + 1;
			var skillUpgrade = m_upgradeData.GetSkillUpgrade(skillIndex);
			var data = skillUpgrade.levels[skillLevel];

			if (m_playerProfile.TrySpendCoins(data.cost))
			{
				m_playerProfile.LevelUpSkill(skillIndex);
				RefreshSkillUI(skillIndex);
				return true;
			}
			else
			{
				m_gameModeBehavior.GotoStore();
			}

			return false;
		}

		protected override void OnEnter()
		{
			for (int i = 0; i < m_uiSkillsUpgrade.Length; ++i)
			{
				RefreshSkillUI(i);
			}
		}

		private void RefreshPlayerUI()
		{
			var level = m_playerProfile.playerLevel + 1;
			
			int cost = 0;
			if (level < m_upgradeData.playerMaxLevel)
			{
				var data = m_upgradeData.GetPlayerUpgrade(level);
				cost = data.cost;
			}
			m_uiPlayerUpgrade.Refresh(level, cost);
		}

		private void RefreshSkillUI(int skillIndex)
		{
			int skillLevel = m_playerProfile.GetSkillLevel(skillIndex) + 1;
			var skillUpgrade = m_upgradeData.GetSkillUpgrade(skillIndex);

			int cost = 0;
			if (skillLevel < skillUpgrade.levels.Count)
			{
				var data = skillUpgrade.levels[skillLevel];
				cost = data.cost;
			}

			m_uiSkillsUpgrade[skillIndex].Refresh(skillLevel, cost);
		}
	}
}