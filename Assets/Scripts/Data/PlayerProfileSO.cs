using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
	[CreateAssetMenu(fileName = "PlayerProfileSO", menuName = "PlayerProfileSO")]
	public class PlayerProfileSO : ScriptableObject
	{
		[SerializeField] private PlayerProfileData m_default;
		
		public AudioOptions audioOptions { get; private set; } = new AudioOptions();
		public int playerLevel { private set; get; }
		private int[] m_skillsLevel = new int[2];
		public IReadOnlyList<int> skillsLevel => m_skillsLevel;
		
		public int coins { private set; get; }
		public event System.Action<int> onCoinsChange;
		public int lastLevelIndex { set; get; } = 0;

		public int GetSkillLevel(int index)
		{
			if (index >= 0 && index < m_skillsLevel.Length)
			{
				return m_skillsLevel[index];
			}

			return -1;
		}

		public void AddCoins(int value)
		{
			coins = Mathf.Max(0, coins + value);
			onCoinsChange?.Invoke(coins);
		}

		public bool TrySpendCoins(int value)
		{
			if (value > 0 && coins >= value)
			{
				coins = Mathf.Max(0, coins - value);
				onCoinsChange?.Invoke(coins);
				return true;
			}

			return false;
		}

		public void LevelUp()
		{
			playerLevel++;
		}
		
		public void LevelUpSkill(int index)
		{
			if (index >= 0 && index < m_skillsLevel.Length)
			{
				m_skillsLevel[index]++;
			}
		}

		public void LoadFromJson(string json)
		{
			if (!string.IsNullOrEmpty(json))
			{
				var data = JsonUtility.FromJson<PlayerProfileData>(json);
				if (data != null)
				{
					Init(data);
				}
			}
		}

		private void Init(PlayerProfileData data)
		{
			playerLevel = data.playerLevel;
			coins = data.coins;
			audioOptions.fxVolume = data.audioOptions.fxVolume;
			audioOptions.musicVolume = data.audioOptions.musicVolume;
			int i_max = Mathf.Min(data.skillsLevel.Length, m_skillsLevel.Length);
			for (int i = 0; i < i_max; i++)
			{
				m_skillsLevel[i] = data.skillsLevel[i];
			}
		}

		public string ToJson()
		{
			PlayerProfileData data = new PlayerProfileData();
			data.playerLevel = playerLevel;
			data.skillsLevel = m_skillsLevel;
			data.coins = coins;
			data.audioOptions = audioOptions;

			return JsonUtility.ToJson(data);
		}

		private void OnEnable()
		{
			m_skillsLevel = new int[2];
			Init(m_default);
		}
	}
}
