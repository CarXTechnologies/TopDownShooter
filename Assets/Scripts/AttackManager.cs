using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
	public class AttackManager : MonoBehaviour
	{
		[SerializeField] private Transform m_skillsSlot;

		private List<AttackComponent> m_skills = new();

		private AttackComponent m_currentSkill;

		public float needMana => m_currentSkill.mana;

		public float attackDistance => m_currentSkill.attackDistance;

		public bool canAttack => m_currentSkill.canAttack;

		private void Awake()
		{
			if (m_skillsSlot == null)
			{
				m_skillsSlot = transform;
			}

			m_skillsSlot.GetComponentsInChildren(true, m_skills);

			m_skills.ForEach(x => x.SetActive(false));
		}

		public void Init(AttackComponent[] skillsPrefab)
		{
			m_skills.ForEach(x => Destroy(x));
			m_skills.Clear();

			foreach (AttackComponent skillPrefab in skillsPrefab)
			{
				var skill = Instantiate(skillPrefab, m_skillsSlot);
				skill.SetActive(false);
				m_skills.Add(skill);
			}

			SelectSkill(0);
		}

		private void Start()
		{
			SelectSkill(0);
		}

		public void NextSkill()
		{
			var index = m_skills.IndexOf(m_currentSkill) + 1;
			if (index >= m_skills.Count)
			{
				index = 0;
			}
			SelectSkill(index);
		}

		public void SelectSkill(int index)
		{
			if (index >= 0 && index < m_skills.Count)
			{
				if (m_currentSkill != null)
				{
					m_currentSkill.SetActive(false);
				}

				m_currentSkill = m_skills[index];
				m_currentSkill.SetActive(true);
			}
		}

		public void Attack(Transform target)
		{
			m_currentSkill.Attack(target);
		}
	}
}