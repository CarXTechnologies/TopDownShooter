using System;
using KartGame.KartSystems;
using UnityEngine;

namespace TopDownShooter.Core
{
	public class Gameplay : MonoBehaviour
	{
		public static Gameplay instance;

		[SerializeField] private FPSController m_fps;
		[SerializeField] private CarController m_car;

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			ActivateFPS();
		}

		public void ActivateCar(ArcadeKart car)
		{
			m_fps.Deactive();
			m_car.Attach(car);
		}
		
		public void ActivateFPS()
		{
			m_fps.Activate(m_car.detachPoint);
			m_car.Detach();
		}
	}
}