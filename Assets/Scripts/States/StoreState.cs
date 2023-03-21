using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter.States
{
	public class StoreState : GameStateBehavior
	{
		public void TryBuyCoins()
		{
			var store = GameController.instance.store;
			store.InitiatePurchase("coins", OnBuyCoins, null);
		}

		private void OnBuyCoins()
		{
			var profile = GameController.instance.playerProfile;
			profile.AddCoins(100);
		}
	}
}