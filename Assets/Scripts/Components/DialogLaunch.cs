using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class DialogLaunch : MonoBehaviour
    {
		public UIDialogPanel panel;
		public DialogData dialogData;

		private bool m_isShowed = false;

		public void Trigger()
		{
			if (!m_isShowed)
			{
				panel.ShowDialog(dialogData);
				m_isShowed = true;
			}
		}
	}
}
