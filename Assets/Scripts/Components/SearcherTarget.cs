using UnityEngine;

namespace TopDownShooter
{
	public class SearcherTarget : MonoBehaviour
	{
		[SerializeField] private LayerMask m_layerMask;
		[SerializeField] private float m_radius = 10;

		public Transform Serach()
		{
			return PhysicsHelper.SearchNearest(transform.position, m_radius, m_layerMask);
		}
	}
}