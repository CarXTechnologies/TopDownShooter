using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
	[System.Serializable]
	public class PlayerProfileData
	{
		public AudioOptions audioOptions = new();
		public int playerLevel;
		public int[] skillsLevel = new int[2];
		public int coins;
	}


	[System.Serializable]
	public class AudioOptions
	{
		public int musicVolume;
		public int fxVolume;
	}
}
