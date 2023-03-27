using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownShooter
{
	public class EnemyHUD : MonoBehaviour
	{
		public GameObject bar;
		public HealthComponent health;
		public Image fillImage;

		private Transform m_cameraTransform;


		private void Awake()
		{
			m_cameraTransform = Camera.main.transform;
		}


		private void Update()
		{
			fillImage.fillAmount = health.percent;

			bar.SetActive(health.percent < 1f && health.percent > 0f);

			var cameraPosition = m_cameraTransform.position;
			cameraPosition.x = transform.position.x;
			transform.LookAt(cameraPosition);
			transform.Rotate(0f, 180f, 0f);
		}
	}
}
