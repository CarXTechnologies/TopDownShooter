using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace KartGame.KartSystems
{
	public class CarController : MonoBehaviour
	{
		[SerializeField] private CinemachineVirtualCamera m_vc;
		[SerializeField] private ArcadeKart m_car;

		public Transform detachPoint => m_car ? m_car.point : null;

		private void Awake()
		{
			Detach();
		}

		private void Start()
		{
			if (m_car)
			{
				Attach(m_car);
			}
		}

		public void Attach(ArcadeKart car)
		{
			m_car = car; 
			m_car.enabled = true;
			var carTransform = m_car.transform;
			m_vc.enabled = true;
			m_vc.Follow = carTransform;
			m_vc.LookAt = carTransform;
		}

		public void Detach()
		{
			m_vc.enabled = false;
			if (m_car)
			{
				m_car.enabled = false;
			}
		}
	}
}