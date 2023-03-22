using UnityEngine.Purchasing;

namespace TopDownShooter.States
{
	public class StoreState : GameStateBehavior
	{
		public void TryBuyCoins()
		{
			var store = GameController.instance.store;
			store.InitiatePurchase("coins_1000", OnBuyCoins, null);
		}

		private void OnBuyCoins()
		{
			var profile = GameController.instance.playerProfile;
			profile.AddCoins(1000);
		}
	}
}