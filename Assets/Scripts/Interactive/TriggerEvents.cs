using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace TopDownShooter
{
	public class TriggerEvents : MonoBehaviour
	{
		[SerializeField] private UnityEvent<Collider> onTriggerEnter;
		[SerializeField] private UnityEvent<Collider> onTriggerExit;
		[SerializeField] private LayerMask m_layerMask = int.MaxValue;
		
		private void OnTriggerEnter(Collider other)
		{
			if ((m_layerMask.value & (1 << other.gameObject.layer)) > 0)
			{
				onTriggerEnter.Invoke(other);
			}
		}

		public void OnTriggerExit(Collider other)
		{
			if ((m_layerMask.value & (1 << other.gameObject.layer)) > 0)
			{
				onTriggerExit.Invoke(other);
			}
		}
	}
}