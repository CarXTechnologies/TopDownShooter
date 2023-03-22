using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
	[CreateAssetMenu(menuName = "TopDownShooter/CharacterData", fileName = "CharacterData")]
	public class CharacterData : ScriptableObject
	{
		[System.Serializable]
		public class Stats
		{
			public float health = 100f;
			public float mana = 100f;
			public float speedRestoreMana = 10f;
			public float speedMove = 100f;
			public float baseDamage = 50f;
		}

		public Stats stats;
		public SkillData[] skills;
	}
}
