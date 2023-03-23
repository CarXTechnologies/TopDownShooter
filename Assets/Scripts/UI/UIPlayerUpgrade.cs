using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownShooter
{
	public class UIPlayerUpgrade : MonoBehaviour
	{
		public TextMeshProUGUI levelText;
		public TextMeshProUGUI costText;
		private int m_maxLevel;
		public event System.Action onTryUpgrade;

		public void OnUpgradeClick()
		{
			onTryUpgrade?.Invoke();
		}

		public void Init(int maxLevel)
		{
			m_maxLevel = maxLevel;
		}

		public void Refresh(int level, int cost)
		{
			if (level >= m_maxLevel)
			{
				levelText.text = $"Max Level {level}";
			}
			else
			{
				levelText.text = $"Level {level}";
			}
			
			costText.text = cost.ToString();
			costText.gameObject.SetActive(level < m_maxLevel);
		}
	}
}