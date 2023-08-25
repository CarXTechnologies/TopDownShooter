using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		public InputActionAsset inputAsset;

		private InputAction m_moveAction;
		private InputAction m_lookAction;
		private InputAction m_jumpAction;
		private InputAction m_sprintAction;

		private void Awake()
		{
			m_moveAction = inputAsset.FindAction("Move");
			m_lookAction = inputAsset.FindAction("Look");
			m_jumpAction = inputAsset.FindAction("Jump");
			m_sprintAction = inputAsset.FindAction("Sprint");
		}

		private void OnEnable()
		{
			move = Vector2.zero;
			look = Vector2.zero;
			jump = false;
			sprint = false;
			m_moveAction.Enable();
			m_lookAction.Enable();
			m_jumpAction.Enable();
			m_sprintAction.Enable();
		}

		private void Update()
		{
			move = m_moveAction.ReadValue<Vector2>();
			look = m_lookAction.ReadValue<Vector2>();
			jump = m_jumpAction.WasPerformedThisFrame();
			sprint = m_sprintAction.IsPressed();
			
		}

		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}