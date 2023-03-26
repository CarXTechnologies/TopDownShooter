using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDownShooter.States
{
	public class GameplayState : GameStateBehavior
	{
		[SerializeField] private InputActionAsset m_inputAsset;

		protected override void OnEnter()
		{
			m_inputAsset.FindActionMap("Player").Enable();
		}

		protected override void OnExit()
		{
			m_inputAsset.FindActionMap("Player").Disable();
		}
	}
}