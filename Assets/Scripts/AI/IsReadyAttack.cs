using TheKiwiCoder;
using UnityEngine;

namespace TopDownShooter
{
	public class IsReadyAttack : DecoratorNode
	{
		private HealthComponent m_health;
		protected override void OnStart()
		{
			if (blackboard.target)
			{
				m_health = blackboard.target.GetComponent<HealthComponent>();
			}
		}

		protected override void OnStop()
		{
			m_health = null;
		}

		protected override State OnUpdate()
		{
			if (m_health == null || m_health.isDead)
			{
				return State.Failure;
			}
			
			var sqrMagnitude = Vector3.SqrMagnitude(blackboard.target.position - context.transform.position);
			var attackDistance = context.attackManager.attackDistance;
			if (sqrMagnitude > attackDistance * attackDistance)
			{
				return State.Failure;
			}

			return child.Update();
		}
	}
}
