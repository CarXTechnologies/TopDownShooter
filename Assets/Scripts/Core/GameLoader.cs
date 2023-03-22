using System;
using System.Collections;
using UnityEngine;

namespace TopDownShooter
{
	public class GameLoader : MonoBehaviour
	{
		private IEnumerator Start()
		{
			yield return new WaitUntil(()=> GameController.isInitialized);
			
			GameController.LoadScene("MainMenu");
		}
	}
}