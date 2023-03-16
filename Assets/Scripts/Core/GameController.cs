using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TopDownShooter
{
	public class GameController : MonoBehaviour
	{
		public static GameController instance { get; private set; }

		[SerializeField] private PlayerProfileSO m_playerProfile;
		[SerializeField] private GameObject m_loader;

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

		private void Start()
		{
			LoadPlayerProfile();
		}

		private void OnApplicationQuit()
		{
			SavePlayerProfile();
		}

		private void OnApplicationPause(bool pauseStatus)
		{
			if (pauseStatus)
			{

			}
		}

		private void LoadPlayerProfile()
		{
			var json = PlayerPrefs.GetString("PlayerProfile");
			Debug.Log($">>> load {json}");
			m_playerProfile.LoadFromJson(json);
		}

		private void SavePlayerProfile()
		{
			var json = m_playerProfile.ToJson();
			Debug.Log($">>> save {json}");
			PlayerPrefs.SetString("PlayerProfile", json);
		}


		public static void LoadScene(string sceneName)
		{
			instance.StartCoroutine(instance.LoadSceneAsync(sceneName));
		}

		private IEnumerator LoadSceneAsync(string sceneName)
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