using System;
using System.Collections;
using UnityEngine;

namespace TopDownShooter
{
	public class MeleeDamager : MonoBehaviour, IDamagerComponent
	{
		[SerializeField] private ParticleSystem m_particleSystem;

		private void Start()
		{
			m_particleSystem.Stop();
		}

		public void Attack(Transform target, float damage, float delay)
		{
			if (target.TryGetComponent(out HealthComponent hp))
			{
				StartCoroutine(Damage(hp, damage, delay));
			}
		}

		private IEnumerator Damage(HealthComponent hp, float damage, float delay)
		{
			m_particleSystem.Play();
			
			yield return new WaitForSeconds(delay);
			
			if (hp)
			{
				hp.TakeDamage(damage);
			}
			
			yield return new WaitForSeconds(0.5f);
			
			m_particleSystem.Stop();
		}
	}
}
