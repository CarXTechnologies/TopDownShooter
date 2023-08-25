using System;
using KartGame.KartSystems;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDownShooter.Core
{
	public class Gameplay : MonoBehaviour
	{
		public static Gameplay instance;

		[SerializeField] private FPSController m_fps;
		[SerializeField] private CarController m_car;
		[SerializeField] private InputActionReference m_interactiveActionReference;
		

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			ActivateFPS();

			m_interactiveActionReference.action.Enable();
			m_interactiveActionReference.action.performed += TryUseInteractive;
		}

		private void TryUseInteractive(InputAction.CallbackContext obj)
		{
			if (m_fps.isActive)
			{
				if (m_fps.targetCar != null)
				{
					ActivateCar(m_fps.targetCar);
				}
			}
			else if (m_car.isActive)
			{
				ActivateFPS();
			}
		}

		public void ActivateCar(ArcadeKart car)
		{
			m_fps.Deactive();
			m_fps.persona.SetParent(car.transform);
			m_car.Attach(car);
		}
		
		public void ActivateFPS()
		{
			m_fps.Activate();
			m_car.Detach();
		}
	}
}