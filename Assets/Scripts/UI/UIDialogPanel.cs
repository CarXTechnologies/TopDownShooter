using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
	public class UIDialogPanel : MonoBehaviour
	{
		[SerializeField] private TMPro.TextMeshProUGUI m_text;
		private DialogData m_dialogData;
		private int m_index;

		public void ShowDialog(DialogData dialogData)
		{
			m_dialogData = dialogData;
			m_index = 0;
			gameObject.SetActive(true);
			NextText();
		}

		public void OnNextClick()
		{
			if (++m_index < m_dialogData.textList.Count)
			{
				NextText();
			}
			else
			{
				gameObject.SetActive(false);
			}
			
		}

		private void NextText()
		{
			m_text.text = m_dialogData.textList[m_index];
		}
	}
}
