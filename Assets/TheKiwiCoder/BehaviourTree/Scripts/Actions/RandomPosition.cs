using UnityEngine;
using TheKiwiCoder;
using UnityEngine.AI;

[System.Serializable]
public class RandomPosition : ActionNode
{
	public float range = 10;

	protected override void OnStart()
	{
	}

	protected override void OnStop()
	{
	}
	
	private bool RandomPoint(Vector3 center, float range, out Vector3 result)
	{
		for (int i = 0; i < 30; i++)
		{
			Vector3 randomPoint = center + Random.insideUnitSphere * range;
			NavMeshHit hit;
			if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
			{
				result = hit.position;
				return true;
			}
		}
		result = Vector3.zero;
		return false;
	}

	protected override State OnUpdate()
	{
		if (RandomPoint(context.transform.position, range, out var moveToPosition))
		{
			blackboard.moveToPosition = moveToPosition;
			return State.Success;
		}
		return State.Failure;
	}
}
