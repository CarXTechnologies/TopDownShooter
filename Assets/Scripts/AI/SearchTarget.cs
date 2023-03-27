using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

namespace TopDownShooter.AI
{
	[System.Serializable]
	public class SearchTarget : ActionNode
	{
		private SearcherTarget m_searcherTarget;

		protected override void OnStart()
		{
			if (m_searcherTarget == null)
			{
				m_searcherTarget = context.gameObject.GetComponent<SearcherTarget>();
			}
		}

		protected override void OnStop()
		{

		}

		protected override State OnUpdate()
		{
			var target = m_searcherTarget.Serach();

			blackboard.target = target;

			return target ? State.Success : State.Failure;
		}
	}
}
