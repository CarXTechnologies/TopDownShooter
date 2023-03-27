using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TopDownShooter
{
	public class AutoBuildNavMesh : MonoBehaviour
	{
		private void Awake()
		{
			var surfaces = GetComponents<NavMeshSurface>();
			foreach (var surface in surfaces)
			{
				surface.BuildNavMesh();
			}
		}
	}
}