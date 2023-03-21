using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace TopDownShooter
{
	[CreateAssetMenu(fileName = "StoreData", menuName = "StoreData")]
	public class StoreData : ScriptableObject
	{
		[System.Serializable]
		public class Product
		{
			public string id;
			public string androidId;
			public string iosId;
			public ProductType productType = ProductType.Consumable;

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
