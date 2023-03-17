using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TopDownShooter
{
	public class HealthComponent : MonoBehaviour
	{
		[SerializeField] private float m_health = 100;
		[SerializeField] private float m_healthMax = 100;

		public float health => m_health;
		public bool isDead => m_health <= 0;
		public float percent => m_health / m_healthMax;

		public event System.Action onDead;
		public event System.Action<float> onTakeDamage;

		public void Init(float healthMax)
		{
			m_health = m_healthMax = healthMax;
		}

		public void TakeDamage(float damage)
		{
			if (isDead)
			{
				return;
			}

			damage = Mathf.Clamp(damage, 0f, m_health);
			m_health -= damage;
			onTakeDamage?.Invoke(damage);

			if (isDead)
			{
				onDead?.Invoke();
			}
		}
	}
}