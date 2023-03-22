using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
	[CreateAssetMenu(menuName = "TopDownShooter/UpgradeData", fileName = "UpgradeData")]
	public class UpgradeData : ScriptableObject
	{
		[System.Serializable]
		public class PlayerUpgradeData
		{
			public int cost;
			public CharacterData.Stats stats;
		}

		[System.Serializable]
		public class SkillUpgradeData
		{
			public int cost;
			public SkillData.Stats stats;
		}
		
		[SerializeField] private List<PlayerUpgradeData> player = new();
		[SerializeField] private List<SkillUpgradeData> skill1 = new();
		[SerializeField] private List<SkillUpgradeData> skill2 = new();
	}
}