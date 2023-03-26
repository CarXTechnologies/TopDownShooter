using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
	public class LifeTimeDelay : MonoBehaviour
	{
		[SerializeField] private float m_delay = 1f;

		private void Start()
		{
			Destroy(gameObject, m_delay);
		}
	}
}