using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace TopDownShooter
{
	[CreateAssetMenu(fileName = "StoreCatalog", menuName = "StoreCatalog")]
	public class StoreCatalog : ScriptableObject
	{
		[System.Serializable]
		public class Product
		{
			public string id;
			public string GooglePlay;
			public string AppleAppStore;
			public ProductType productType = ProductType.Consumable;
			public PayoutDefinition[] payouts;

			public string GetStoreId()
			{
#if UNITY_ANDROID
				return androidId;
#elif UNITY_IOS
				return iosId;
#endif
				return id;
			}
		}

		public List<Product> products;


		public Product GetProduct(string id)
		{
			return products.Find(x => x.id == id);
		}
	}
}
