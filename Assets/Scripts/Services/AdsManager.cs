using System;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
	private string GAME_ID = "5212214"; //replace with your gameID from dashboard. note: will be different for each platform.

	private const string REWARDED_VIDEO_PLACEMENT = "Rewarded_Android";

	[SerializeField] private bool testMode = true;

	private bool m_videoLoaded = false;

	public bool HasRewardVideo => m_videoLoaded;
	private System.Action onRewardAdComplete;

	public void Initialize()
	{
		if (Advertisement.isSupported)
		{
			DebugLog(Application.platform + " supported by Advertisement");
		}
		Advertisement.Initialize(GAME_ID, testMode, this);
	}

	public void LoadRewardedAd()
	{
		Advertisement.Load(REWARDED_VIDEO_PLACEMENT, this);
	}

	public void ShowRewardedAd(System.Action callback)
	{
		onRewardAdComplete = callback;
		Advertisement.Show(REWARDED_VIDEO_PLACEMENT, this);
	}

	#region Interface Implementations
	public void OnInitializationComplete()
	{
		DebugLog("Init Success");
		LoadRewardedAd();
	}

	public void OnInitializationFailed(UnityAdsInitializationError error, string message)
	{
		DebugLog($"Init Failed: [{error}]: {message}");
	}

	public void OnUnityAdsAdLoaded(string placementId)
	{
		DebugLog($"Load Success: {placementId}");
		m_videoLoaded = true;
	}

	public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
	{
		DebugLog($"Load Failed: [{error}:{placementId}] {message}");
	}

	public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
	{
		Release();
		DebugLog($"OnUnityAdsShowFailure: [{error}]: {message}");
		LoadRewardedAd();
	}

	public void OnUnityAdsShowStart(string placementId)
	{
		DebugLog($"OnUnityAdsShowStart: {placementId}");
	}

	public void OnUnityAdsShowClick(string placementId)
	{
		DebugLog($"OnUnityAdsShowClick: {placementId}");
	}

	public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
	{
		if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
		{
			onRewardAdComplete?.Invoke();
		}
		Release();
		LoadRewardedAd();
		DebugLog($"OnUnityAdsShowComplete: [{showCompletionState}]: {placementId}");
	}
	#endregion

	public void OnGameIDFieldChanged(string newInput)
	{
		GAME_ID = newInput;
	}

	public void ToggleTestMode(bool isOn)
	{
		testMode = isOn;
	}

	//wrapper around debug.log to allow broadcasting log strings to the UI
	void DebugLog(string msg)
	{
		Debug.Log(msg);
	}

	private void Release()
	{
		onRewardAdComplete = null;
		m_videoLoaded = false;
	}
}
