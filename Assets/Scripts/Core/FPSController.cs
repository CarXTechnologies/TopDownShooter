using System;
using Cinemachine;
using KartGame.KartSystems;
using StarterAssets;
using UnityEngine;

namespace TopDownShooter.Core
{
	public class FPSController : MonoBehaviour
	{
		[SerializeField] private GameObject m_person;
		[SerializeField] private GameObject m_camera;

		public Transform persona => m_person.transform;

		public ArcadeKart targetCar => m_car;

		public bool isActive => m_person.gameObject.activeSelf;

		private void Awake()
		{
			Deactive();
		}

		public void Activate()
		{
			m_person.SetActive(true);
			m_person.transform.SetParent(this.transform);

			m_camera.SetActive(true);
		}
		
		public void Deactive()
		{
			m_person.SetActive(false);
			m_camera.SetActive(false);
		}

		private ArcadeKart m_car;

		public void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Car"))
			{
				m_car = other.GetComponentInParent<ArcadeKart>();
			}
		}

		public void OnTriggerExit(Collider other)
		{
			if (other.CompareTag("Car"))
			{
				var car = other.GetComponentInParent<ArcadeKart>();
				if (car == m_car)
				{
					m_car = null;
				}
			}
		}
	}
}