using com.unity3d.mediation;
using System.Threading.Tasks;
using UnityEngine;


public class LevelPlayAds : MonoBehaviour
{
    [SerializeField] private int _numberOfDeathToShowAd;
    
    private LevelPlayInterstitialAd _interstitialAd;
    private int _counterDeaths = 0;


    private void Start()
    {
        Debug.Log("unity-script: IronSource.Agent.validateIntegration");
        IronSource.Agent.validateIntegration();
        IronSource.Agent.setMetaData("UnityAds_coppa", "true");

        Debug.Log("unity-script: unity version" + IronSource.unityVersion());

        LevelPlay.Init("20cf97315", null, new[] { LevelPlayAdFormat.INTERSTITIAL });
    }


    private void OnEnable()
    {
        LevelPlay.OnInitSuccess += SdkInitializationCompletedEvent;
        LevelPlay.OnInitFailed += SdkInitializationFailedEvent;
    }


    private void OnDisable()
    {
        DestroyInterstitialAd();
        LevelPlay.OnInitSuccess -= SdkInitializationCompletedEvent;
        LevelPlay.OnInitFailed -= SdkInitializationFailedEvent;
    }


    private void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }


    private async void LoadAdAsync()
    {
        await Task.Run(() =>
        {
            _interstitialAd.LoadAd();
        });
    }


    private void CreateInterstitialAd()
    {
        _interstitialAd = new LevelPlayInterstitialAd("zeuqutzjrnklhm5q");

        _interstitialAd.OnAdLoaded += InterstitialOnAdLoadedEvent;
        _interstitialAd.OnAdLoadFailed += InterstitialOnAdLoadFailedEvent;
        _interstitialAd.OnAdDisplayed += InterstitialOnAdDisplayedEvent;
        _interstitialAd.OnAdDisplayFailed += InterstitialOnAdDisplayFailedEvent;
        _interstitialAd.OnAdClicked += InterstitialOnAdClickedEvent;
        _interstitialAd.OnAdClosed += InterstitialOnAdClosedEvent;
        _interstitialAd.OnAdInfoChanged += InterstitialOnAdInfoChangedEvent;

        LoadAdAsync();
    }


    private void DestroyInterstitialAd()
    {
        _interstitialAd.OnAdLoaded -= InterstitialOnAdLoadedEvent;
        _interstitialAd.OnAdLoadFailed -= InterstitialOnAdLoadFailedEvent;
        _interstitialAd.OnAdDisplayed -= InterstitialOnAdDisplayedEvent;
        _interstitialAd.OnAdDisplayFailed -= InterstitialOnAdDisplayFailedEvent;
        _interstitialAd.OnAdClicked -= InterstitialOnAdClickedEvent;
        _interstitialAd.OnAdClosed -= InterstitialOnAdClosedEvent;
        _interstitialAd.OnAdInfoChanged -= InterstitialOnAdInfoChangedEvent;

        _interstitialAd?.DestroyAd();
    }


    #region AdInfo Interstitial

    private void InterstitialOnAdLoadedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdLoadedEvent With AdInfo " + adInfo);
    }

    private void InterstitialOnAdLoadFailedEvent(LevelPlayAdError error)
    {
        Debug.Log("unity-script: I got InterstitialOnAdLoadFailedEvent With Error " + error);
    }

    private void InterstitialOnAdDisplayedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdDisplayedEvent With AdInfo " + adInfo);
    }

    private void InterstitialOnAdDisplayFailedEvent(LevelPlayAdDisplayInfoError infoError)
    {
        Debug.Log("unity-script: I got InterstitialOnAdDisplayFailedEvent With InfoError " + infoError);
    }

    private void InterstitialOnAdClickedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdClickedEvent With AdInfo " + adInfo);
    }

    private void InterstitialOnAdClosedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdClosedEvent With AdInfo " + adInfo);
    }

    private void InterstitialOnAdInfoChangedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdInfoChangedEvent With AdInfo " + adInfo);
    }

    #endregion


    private void SdkInitializationCompletedEvent(LevelPlayConfiguration config)
    {
        Debug.Log("unity-script: I got SdkInitializationCompletedEvent with config: " + config);
        CreateInterstitialAd();
    }


    private void SdkInitializationFailedEvent(LevelPlayInitError error)
    {
        Debug.Log("unity-script: I got SdkInitializationFailedEvent with error: " + error);
    }


    public void OnPlayerDeath()
    {
        _counterDeaths++;
    }


    public void ShowAd()
    {
        if (_interstitialAd.IsAdReady() == false && _counterDeaths < _numberOfDeathToShowAd)
            return;

        _interstitialAd.ShowAd();
        LoadAdAsync();
        _counterDeaths = 0;
    }
}