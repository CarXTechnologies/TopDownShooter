using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Purchasing;

namespace TopDownShooter
{
	public class StoreManager : MonoBehaviour, IStoreListener
	{
		private IStoreController m_StoreController;

		public StoreData storeData;

		public bool isInitialized => m_StoreController != null;

		void Start()
		{
			var options = new InitializationOptions().SetEnvironmentName("test");
			try
			{
				UnityServices.InitializeAsync(options).ContinueWith(task => InitializePurchasing());
			}
			catch(System.Exception e)
			{
				Debug.LogException(e);
			}

			// InitializePurchasing();
		}

		void InitializePurchasing()
		{
			var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

			foreach (var data in storeData.products)
			{
				builder.AddProduct(data.GetStoreId(), data.productType);
			}

			UnityPurchasing.Initialize(this, builder);
		}

		public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
		{
			Debug.Log("In-App Purchasing successfully initialized");
			m_StoreController = controller;
		}

		public void OnInitializeFailed(InitializationFailureReason error)
		{
			OnInitializeFailed(error, null);
		}

		public void OnInitializeFailed(InitializationFailureReason error, string? message)
		{
			var errorMessage = $"Purchasing failed to initialize. Reason: {error}.";

			if (message != null)
			{
				errorMessage += $" More details: {message}";
			}

			Debug.LogError(errorMessage);
		}

		public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
		{
			Debug.LogError($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
			m_onFail?.Invoke();
		}

		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
		{
			//Retrieve the purchased product
			var product = args.purchasedProduct;

			m_onSuccess?.Invoke();

			Debug.Log($"Purchase Complete - Product: {product.definition.id}");

			//We return Complete, informing IAP that the processing on our side is done and the transaction can be closed.
			return PurchaseProcessingResult.Complete;
		}

		private System.Action m_onSuccess;
		private System.Action m_onFail;
		public void InitiatePurchase(string productID, System.Action onSuccess, System.Action onFail)
		{
			if (!isInitialized)
			{
				onFail?.Invoke();
				return;
			}

			var product = storeData.GetProduct(productID);
			if (product == null)
			{
				onFail?.Invoke();
				return;
			}

			m_StoreController.InitiatePurchase(product.GetStoreId());
			m_onSuccess = onSuccess;
			m_onFail = onFail;
		}
	}
}
