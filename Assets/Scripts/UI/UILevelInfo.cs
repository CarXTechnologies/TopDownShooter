using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownShooter
{
	public class UILevelInfo : MonoBehaviour
	{
		public TMPro.TextMeshProUGUI levelNameText;
		public Image previewImage;

		public void SetInfo(LevelsInfo.Data data)
		{
			levelNameText.text = data.name;
			previewImage.sprite = data.icon;
		}
	}
}
