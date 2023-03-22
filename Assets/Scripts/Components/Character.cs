using UnityEngine;

namespace TopDownShooter
{
	public class Character : MonoBehaviour
	{
		[SerializeField] private CharacterData m_data;
		[HideInInspector] public HealthComponent health;
		[HideInInspector] public ManaComponent mana;
		[HideInInspector] public MovingComponent moving;
		[HideInInspector] public AttackManager attackManager;

		private void Start()
		{
			var stats = m_data.stats;
			
			if (TryGetComponent(out health))
			{
				health.Init(stats.health);
			}

			if (TryGetComponent(out mana))
			{
				mana.Init(stats.mana, stats.speedRestoreMana);
			}

			if (TryGetComponent(out moving))
			{
				moving.Init(stats.speedMove);
			}

			if (TryGetComponent(out attackManager))
			{
				attackManager.Init(m_data.skills, stats.baseDamage);
			}
		}
	}
}