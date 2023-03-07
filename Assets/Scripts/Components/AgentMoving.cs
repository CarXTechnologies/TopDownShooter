using UnityEngine;
using UnityEngine.AI;

namespace TopDownShooter
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class AgentMoving : MovingComponent
	{
		private NavMeshAgent m_agent;

		private void Awake()
		{
			m_agent = GetComponent<NavMeshAgent>();
		}

		public override void Move(Vector3 dir)
		{

			throw new System.NotImplementedException();
		}

		public override void MoveTo(Vector3 position)
		{
			m_agent.SetDestination(position);
		}

		public override float speed => m_agent.speed;
		public override float velocity => m_agent.velocity.magnitude;

		public override void Init(float speed)
		{
			m_agent.speed = speed;
		}
	}
}
