using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;

namespace TopDownShooter
{
	public class StoreManager : MonoBehaviour, IStoreListener
	{
		private IStoreController m_StoreController;
		private CrossPlatformValidator m_Validator = null;
		[SerializeField] private bool m_UseAppleStoreKitTestCertificate = false;
		[SerializeField] private StoreCatalog m_storeCatalog;

		public bool isInitialized => m_StoreController != null;

		public void InitializePurchasing()
		{
			var module = StandardPurchasingModule.Instance();
			module.useFakeStoreUIMode = FakeStoreUIMode.StandardUser;
			var builder = ConfigurationBuilder.Instance(module);

			// IAPConfigurationHelper.PopulateConfigurationBuilder(ref builder, ProductCatalog.LoadDefaultCatalog());
			foreach (var data in m_storeCatalog.products)
			{
				var ids = new IDs();
				ids.Add(data.GooglePlay, "GooglePlay");
				ids.Add(data.AppleAppStore, "AppleAppStore");
				builder.AddProduct(data.id, data.productType, ids, data.payouts);
			}

			UnityPurchasing.Initialize(this, builder);
		}

		void IStoreListener.OnInitialized(IStoreController controller, IExtensionProvider extensions)
		{
			Debug.Log("In-App Purchasing successfully initialized");
			m_StoreController = controller;

			InitializeValidator();
		}

		void IStoreListener.OnInitializeFailed(InitializationFailureReason error)
		{
			Debug.LogError($"Purchasing failed to initialize. Reason: {error}.");
		}

		void IStoreListener.OnInitializeFailed(InitializationFailureReason error, string? message)
		{
			var errorMessage = $"Purchasing failed to initialize. Reason: {error}.";

			if (message != null)
			{
				errorMessage += $" More details: {message}";
			}

			Debug.LogError(errorMessage);
		}

		void IStoreListener.OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
		{
			Debug.LogError($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
			PurchaseFail(product);
		}

		PurchaseProcessingResult IStoreListener.ProcessPurchase(PurchaseEventArgs args)
		{
			//Retrieve the purchased product
			var product = args.purchasedProduct;

			if (IsPurchaseValid(product))
			{
				Debug.Log($"Purchase Complete - Product: {product.definition.id}");
				PurchaseSuccess(product);
			}
			else
			{
				Debug.LogError($"Invalid receipt - Product: {product.definition.id}");
				PurchaseFail(product);
			}


			//We return Complete, informing IAP that the processing on our side is done and the transaction can be closed.
			return PurchaseProcessingResult.Complete;
		}
		
		private bool IsPurchaseValid(Product product)
		{
			//If we the validator doesn't support the current store, we assume the purchase is valid
			if (IsCurrentStoreSupportedByValidator())
			{
				try
				{
					var result = m_Validator.Validate(product.receipt);
					//The validator returns parsed receipts.
					LogReceipts(result);
				}
				//If the purchase is deemed invalid, the validator throws an IAPSecurityException.
				catch (IAPSecurityException reason)
				{
					Debug.Log($"Invalid receipt: {reason}");
					return false;
				}
			}

			return true;
		}
		
		private static void LogReceipts(IEnumerable<IPurchaseReceipt> receipts)
		{
			Debug.Log("Receipt is valid. Contents:");
			foreach (var receipt in receipts)
			{
				LogReceipt(receipt);
			}
		}
		
		private static void LogReceipt(IPurchaseReceipt receipt)
		{
			Debug.Log($"Product ID: {receipt.productID}\n" +
			          $"Purchase Date: {receipt.purchaseDate}\n" +
			          $"Transaction ID: {receipt.transactionID}");

			if (receipt is GooglePlayReceipt googleReceipt)
			{
				Debug.Log($"Purchase State: {googleReceipt.purchaseState}\n" +
				          $"Purchase Token: {googleReceipt.purchaseToken}");
			}

			if (receipt is AppleInAppPurchaseReceipt appleReceipt)
			{
				Debug.Log($"Original Transaction ID: {appleReceipt.originalTransactionIdentifier}\n" +
				          $"Subscription Expiration Date: {appleReceipt.subscriptionExpirationDate}\n" +
				          $"Cancellation Date: {appleReceipt.cancellationDate}\n" +
				          $"Quantity: {appleReceipt.quantity}");
			}
		}

		private void InitializeValidator()
		{
			if (IsCurrentStoreSupportedByValidator())
			{
#if !UNITY_EDITOR
				var appleTangleData = m_UseAppleStoreKitTestCertificate ? AppleStoreKitTestTangle.Data() : AppleTangle.Data();
                m_Validator = new CrossPlatformValidator(GooglePlayTangle.Data(), appleTangleData, Application.identifier);
#endif
			}
			else
			{
				var currentAppStore = StandardPurchasingModule.Instance().appStore;
				var warningMsg = $"The cross-platform validator is not implemented for the currently selected store: {currentAppStore}. \n" +
				                 "Build the project for Android, iOS, macOS, or tvOS and use the Google Play Store or Apple App Store. See README for more information.";
				Debug.LogWarning(warningMsg);
			}
		}

		public static bool IsCurrentStoreSupportedByValidator()
		{
			//The CrossPlatform validator only supports the GooglePlayStore and Apple's App Stores.
			return IsGooglePlayStoreSelected() || IsAppleAppStoreSelected();
		}

		public static bool IsGooglePlayStoreSelected()
		{
			var currentAppStore = StandardPurchasingModule.Instance().appStore;
			return currentAppStore == AppStore.GooglePlay;
		}

		public static bool IsAppleAppStoreSelected()
		{
			var currentAppStore = StandardPurchasingModule.Instance().appStore;
			return currentAppStore == AppStore.AppleAppStore ||
			       currentAppStore == AppStore.MacAppStore;
		}
		
		private void PurchaseFail(Product product)
		{
			var request = m_requests.Find(x => x.productID == product.definition.id);
			if (request != null)
			{
				request.onFail?.Invoke();
				m_requests.Remove(request);
			}
		}

		private void PurchaseSuccess(Product product)
		{
			var request = m_requests.Find(x => x.productID == product.definition.id);
			if (request != null)
			{
				request.onSuccess?.Invoke();
				m_requests.Remove(request);
			}
		}
		
		public Product GetProduct(string productID)
		{
			if (isInitialized && !string.IsNullOrEmpty(productID))
			{
				return m_StoreController.products.WithID(productID);
			}

			Debug.LogError("StoreManager attempted to get unknown product " + productID);
			return null;
		}

		public void InitiatePurchase(string productID, System.Action onSuccess, System.Action onFail)
		{
			if (!isInitialized || GetProduct(productID) == null)
			{
				onFail?.Invoke();
				return;
			}

			PurchaseRequest request = new()
			{
				productID = productID,
				onSuccess = onSuccess,
				onFail = onFail,
			};
			m_requests.Add(request);

			m_StoreController.InitiatePurchase(request.productID);
		}

		private readonly List<PurchaseRequest> m_requests = new();

		class PurchaseRequest
		{
			public string productID;
			public System.Action onSuccess;
			public System.Action onFail;
		}
	}
}