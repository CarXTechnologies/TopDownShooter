using System;
using Cinemachine;
using StarterAssets;
using UnityEngine;

namespace TopDownShooter.Core
{
	public class FPSController : MonoBehaviour
	{
		[SerializeField] private GameObject m_person;
		[SerializeField] private GameObject m_camera;

		private void Awake()
		{
			Deactive();
		}

		public void Activate(Transform point)
		{
			m_person.SetActive(true);
			if (point)
			{
				m_person.transform.SetPositionAndRotation(point.position, point.rotation);
			}

			m_camera.SetActive(true);
		}
		
		public void Deactive()
		{
			m_person.SetActive(false);
			m_camera.SetActive(false);
		}
	}
}