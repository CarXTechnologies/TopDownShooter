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
		public int coins { private set; get; }
		public event System.Action<int> onCoinsChange;
		public int lastLevelIndex { set; get; } = 0;

		

		public void SpendCoins(int value)
		{
			coins = Mathf.Max(0, coins - value);
			onCoinsChange?.Invoke(coins);
		}

		public void LevelUp()
		{
			playerLevel++;
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
			this.playerLevel = data.playerLevel;
			this.coins = data.coins;
			this.audioOptions.fxVolume = data.audioOptions.fxVolume;
			this.audioOptions.musicVolume = data.audioOptions.musicVolume;
		}

		public string ToJson()
		{
			PlayerProfileData data = new PlayerProfileData();
			data.playerLevel = this.playerLevel;
			data.coins = this.coins;
			data.audioOptions = this.audioOptions;

			return JsonUtility.ToJson(data);
		}

		private void OnEnable()
		{
			Init(m_default);
		}
	}
}
