using UnityEngine;

namespace TopDownShooter
{
	[RequireComponent(typeof(CharacterController))]
	public class CharacterControllerMoving : MovingComponent
	{
		private CharacterController m_characterController;
		[SerializeField] private float m_speed = 5f;

		private void Awake()
		{
			m_characterController = GetComponent<CharacterController>();
			m_characterController.detectCollisions = true;
		}

		public override void Move(Vector3 dir)
		{
			m_characterController.SimpleMove(dir * m_speed);
		}

		public override void MoveTo(Vector3 position)
		{
			throw new System.NotImplementedException();
		}

		public override float maxSpeed => m_speed;
		public override Vector3 velocity => m_characterController.velocity;

		public override void Init(float speed)
		{
			m_speed = speed;
		}
	}
}
