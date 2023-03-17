using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter.States
{
	public class StateMachine
	{
		private readonly List<IGameState> m_states = new();

		private IGameState m_currentState;

		public StateMachine(IEnumerable<IGameState> states)
		{
			m_states.AddRange(states);
			m_states.ForEach(x=>x.Init(this));
		}

		public void Change<T>() where T : IGameState
		{
			Change(typeof(T));
		}

		public void Change(System.Type typeNextState)
		{
			var nextState = m_states.Find(x => x.GetType() == typeNextState);
			if (nextState != null && m_currentState != nextState)
			{
				if (m_currentState != null)
				{
					m_currentState.Exit();
					foreach (var view in m_currentState.GetViews())
					{
						view.SetActive(false);
					}
				}

				nextState.Enter();
				foreach (var view in nextState.GetViews())
				{
					view.SetActive(true);
				}
				m_currentState = nextState;
			}
		}

		public void Release()
		{
			if (m_currentState != null)
			{
				m_currentState.Exit();
				m_currentState = null;
			}
			
			m_states.Clear();
		}
	}
}