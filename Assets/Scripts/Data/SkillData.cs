using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace TopDownShooter
{
	[CreateAssetMenu(menuName = "TopDownShooter/SkillData", fileName = "SkillData")]
	public class SkillData : ScriptableObject
	{
		[System.Serializable]
		public class Stats
		{
			public float mana;
			public float attackDistance;
			public float cooldownTime;
			public float damage;
			public float flightDistance;
			public float flightSpeed;
		}

		public AttackComponent prefab;
		public Stats stats;
	}
}
