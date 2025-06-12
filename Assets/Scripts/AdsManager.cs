
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleMobileAds.Api;

using System;

public class AdsManager : MonoBehaviour
{

    //private string _adUnitId = "ca-app-pub-3940256099942544/6300978111";  //TEST
#if RELEASE_MODE
    private string _adUnitId = "ca-app-pub-4803684126798138/7713333861"; //REAL 
#elif DEBUG_MODE
    private string _adUnitId = "ca-app-pub-3940256099942544/6300978111";  //TEST
#endif
    private static AdsManager instance = null;
    private static bool Exists;

    BannerView _bannerView;
    private float Banner_regenTimer = 120.0f;
    private float Banner_TimerVal = 120.0f;
    private bool Banner_adsshow = false;
    // Start is called before the first frame update
    public static AdsManager Instance
    {
        get
        {
            if (instance == null)
                instance = new AdsManager();

            return instance;
        }
    }
    public void Awake()
    {
        if (instance == null)
            instance = this;

    }
    void Start()
    {
        if (!Exists)
        {
            Exists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        //banner_LoadAd();

    }

    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyBannerView();
        }

        // Use the AdSize argument to set a custom size for the ad.
        //AdSize adSize = new AdSize(320, 50);
        // Create a 320x50 banner at top of the screen //AdSize.Banner
        //AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        _bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Top);  // 원점은 화면의 왼쪽 상단입니다.
        //_bannerView = new BannerView(_adUnitId, adSize, AdPosition.Top);
    }

    public void banner_LoadAd()
    {
        // create an instance of a banner view first.
        if (_bannerView == null)
        {
            CreateBannerView();
        }

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        _bannerView.LoadAd(adRequest);
        ListenToAdEvents_banner();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Adsonoff") == 0) //0 ads on 
        {
            Banner_regenTimer -= Time.deltaTime;
            if (Banner_regenTimer <= 0)
            {

                //           admgr.playbannerad();
                Banner_regenTimer = Banner_TimerVal;
                Debug.Log("destroy ads");
                DestroyBannerView();
                Banner_adsshow = false;
            }
            else if (Banner_regenTimer >= 1 && Banner_regenTimer <= 110) // 배너 시간 
            {
                if (!Banner_adsshow)
                {
                    Debug.Log("request banner ads");
                    banner_LoadAd();
                    Banner_adsshow = true;

                }

            }
        }
        // if (Application.isPlaying)
    }
    // <summary>
    /// Destroys the banner view.
    /// </summary>
    public void DestroyBannerView()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner view.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }

    /// <summary>
    /// listen to events the banner view may raise.
    /// </summary>
    private void ListenToAdEvents_banner()
    {
        // Raised when an ad is loaded into the banner view.
        _bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + _bannerView.GetResponseInfo());
        };
        // Raised when an ad fails to load into the banner view.
        _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);
        };
        // Raised when the ad is estimated to have earned money.
        _bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        _bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        _bannerView.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
            Time.timeScale = 0.0f;
        };
        // Raised when an ad opened full screen content.
        _bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
            Time.timeScale = 0.0f;
        };
        // Raised when the ad closed full screen content.
        _bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
            Time.timeScale = 1.0f;
        };
    }

}
