using UnityEngine;

namespace TopDownShooter
{
	public static class PhysicsHelper
	{
		private static Collider[] m_result = new Collider[16];
		public static Transform SearchNearest(Vector3 position, float radius, LayerMask mask)
		{
			var count = Physics.OverlapSphereNonAlloc(position, radius, m_result, mask, QueryTriggerInteraction.Ignore);
			if (count == 0)
			{
				return null;
			}
			
			Transform target = null;
			float minDistance = float.MaxValue;
			for (int i = 0; i < count; i++)
			{
				var tr = m_result[i].transform; 
				var distance = Vector3.Distance(tr.position, position); 
				if (distance < minDistance)
				{
					minDistance = distance;
					target = tr;
				}
			}

			return target;
		}
	}
}