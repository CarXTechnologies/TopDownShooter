using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
		static int MoveXId = Animator.StringToHash("MoveX");
		static int MoveZId = Animator.StringToHash("MoveZ");
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
			var normalizeSpeed = Mathf.Clamp01(m_moving.velocity.magnitude / m_moving.maxSpeed);
			m_animator.SetFloat(SpeedMoveId, normalizeSpeed);

			var moveDir = m_moving.velocity.normalized;
			var velocity = m_moving.transform.InverseTransformDirection(moveDir) * normalizeSpeed;
			m_animator.SetFloat(MoveXId, velocity.x);
			m_animator.SetFloat(MoveZId, velocity.z);
		}
	}
}