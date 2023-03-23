using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
	[CreateAssetMenu(menuName = "TopDownShooter/UpgradeData", fileName = "UpgradeData")]
	public class UpgradeData : ScriptableObject
	{
		[System.Serializable]
		public class Upgrade<TStats>
		{
			public int cost;
			public TStats stats;
		}

		[System.Serializable]
		public class SkillUpgrade
		{
			public string name;
			public SkillData skill;
			public List<Upgrade<SkillData.Stats>> levels = new();
		}

		[SerializeField] private CharacterData m_characterData;
		[SerializeField] private List<Upgrade<CharacterData.Stats>> m_player = new();
		[SerializeField] private List<SkillUpgrade> m_skills = new();

		public int playerMaxLevel => m_player.Count;

		public int GetSkillMaxLevel(int index)
		{
			return m_skills[index].levels.Count;
		}
		
		public SkillUpgrade GetSkillUpgrade(int index)
		{
			return m_skills[index];
		}

		public Upgrade<CharacterData.Stats> GetPlayerUpgrade(int level)
		{
			return m_player[level];
		}
		
		public Upgrade<SkillData.Stats> GetSkillUpgradeStats(int index, int level)
		{
			return m_skills[index].levels[level];
		}

		public CharacterData GetCharacterData(int level, IReadOnlyList<int> skillsLevel)
		{
			var playerData = ScriptableObject.Instantiate(m_characterData);
			var upgradeData = m_player[level];
			playerData.stats = upgradeData.stats;
			
			for (int i = 0; i < skillsLevel.Count; ++i)
			{
				var skillLevel = skillsLevel[i];
				var skillData = Instantiate(playerData.skills[i]);
				skillData.stats = m_skills[i].levels[skillLevel].stats;
				playerData.skills[i] = skillData;
			}
			
			return playerData;
		}
	}
}