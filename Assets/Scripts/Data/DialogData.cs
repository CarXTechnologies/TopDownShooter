using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
	[CreateAssetMenu(fileName = "DialogData", menuName = "DialogData")]
	public class DialogData : ScriptableObject
	{
		public List<string> textList;
	}
}
