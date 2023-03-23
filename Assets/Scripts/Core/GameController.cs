using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

namespace TopDownShooter
{
	public class GameController : MonoBehaviour
	{
		public static GameController instance { get; private set; }

		[SerializeField] private PlayerProfileSO m_playerProfile;
		[SerializeField] private GameObject m_loader;
		[SerializeField] private StoreManager m_store;
		[SerializeField] private AdsManager m_ads;
		public StoreManager store => m_store;
		public AdsManager ads => m_ads;
		public PlayerProfileSO playerProfile => m_playerProfile;

		private bool m_isInitialized;
		public static bool isInitialized => instance && instance.m_isInitialized;

		private void Awake()
		{
			if (instance != null)
			{
				Debug.LogWarning("instance not null");
				Destroy(gameObject);
			}

			instance = this;
			DontDestroyOnLoad(gameObject);
			
			m_loader.SetActive(false);
		}

		private async void Start()
		{
			LoadPlayerProfile();

			await Initialize();
		}

		private async Task Initialize()
		{
			const string kEnvironment = "production";
			try
			{
				var options = new InitializationOptions().SetEnvironmentName(kEnvironment);
				await UnityServices.InitializeAsync(options);
				Debug.Log($"UnityServices.Initialize state: {UnityServices.State} - ExternalUserId: {UnityServices.ExternalUserId}");
				
				await AnalyticsService.Instance.CheckForRequiredConsents();
				Debug.Log($"Started AnalyticsUserID: {AnalyticsService.Instance.GetAnalyticsUserID()}");

				m_ads.Initialize();
			}
			catch (System.Exception e)
			{
				Debug.LogException(e);
			}
			
			store.InitializePurchasing();
			
			m_isInitialized = true;
		}

		private void OnApplicationQuit()
		{
			Debug.Log($"[GameController] OnApplicationQuit()", this);
			SavePlayerProfile();
		}

		private void OnApplicationPause(bool pauseStatus)
		{
			Debug.Log($"[GameController] OnApplicationPause({pauseStatus})", this);
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			Debug.Log($"[GameController] OnApplicationFocus({hasFocus})", this);
		}

		private void LoadPlayerProfile()
		{
			var json = PlayerPrefs.GetString("PlayerProfile");
			Debug.Log($"[GameController] LoadPlayerProfile() - {json}", this);
			m_playerProfile.LoadFromJson(json);
		}

		private void SavePlayerProfile()
		{
			var json = m_playerProfile.ToJson();
			Debug.Log($"[GameController] SavePlayerProfile() - {json}", this);
			PlayerPrefs.SetString("PlayerProfile", json);
		}


		public static void LoadScene(string sceneName)
		{
			instance.StartCoroutine(instance.LoadSceneAsync(sceneName));
		}

		public IEnumerator LoadSceneAsync(string sceneName)
		{
			m_loader.SetActive(true);
			
			yield return SceneManager.LoadSceneAsync("Empty");
			
			System.GC.Collect();
			Resources.UnloadUnusedAssets();

			yield return SceneManager.LoadSceneAsync(sceneName);
			
			m_loader.SetActive(false);
		}
	}
}