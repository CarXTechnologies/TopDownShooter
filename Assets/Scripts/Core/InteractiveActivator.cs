using System;
using UnityEngine;

namespace TopDownShooter.Core
{
	public class InteractiveActivator : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			Debug.Log($"OnTriggerEnter {name} - {other.name}");
		}
	}
}