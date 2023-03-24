using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
	public class CharacterAnimation : MonoBehaviour
	{
		private Animator m_animator;
		private AttackManager m_attackManager;
		private MovingComponent m_moving;
		private HealthComponent m_health;

		static int SpeedMoveId = Animator.StringToHash("SpeedMove");
		static int AttackId = Animator.StringToHash("Attack");
		static int DieId = Animator.StringToHash("Die");

		private Vector3 m_lastPosition;


		private void Awake()
		{
			m_animator = GetComponent<Animator>();
			m_attackManager = GetComponentInParent<AttackManager>();
			m_moving = GetComponentInParent<MovingComponent>();
			m_health = GetComponentInParent<HealthComponent>();
		}

		private void OnEnable()
		{
			m_attackManager.onAttack.AddListener(OnAttackHandler);
			m_health.onDead += OnDead;
		}

		private void OnDisable()
		{
			m_health.onDead -= OnDead;
			m_attackManager.onAttack.RemoveListener(OnAttackHandler);
		}

		private void OnDead()
		{
			m_animator.SetTrigger(DieId);
		}

		private void OnAttackHandler()
		{
			m_animator.SetTrigger(AttackId);
		}

		private void LateUpdate()
		{
			var velocity = Mathf.Clamp01(m_moving.velocity / m_moving.speed);
			m_animator.SetFloat(SpeedMoveId, velocity);
		}
	}
}