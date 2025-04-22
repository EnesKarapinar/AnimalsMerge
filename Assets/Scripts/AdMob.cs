using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;
using System;

public class AdMob : MonoBehaviour
{
    // Banner ID
#if UNITY_ANDROID
    private string _adBannerID = "ca-app-pub-4411474865941018/4530969736";
#elif UNITY_IPHONE
  private string _adBannerID = "ca-app-pub-3940256099942544/6300978111";_adIntermId
#else
  private string _adBannerID = "unused";
#endif

    BannerView _bannerView;

    // Interstitial ID
#if UNITY_ANDROID
    private string _adInterstitialID = "ca-app-pub-4411474865941018/5977318316";
#elif UNITY_IPHONE
  private string _adInterstitialID = "ca-app-pub-3940256099942544/1033173712";
#else
  private string _adInterstitialID = "unused";
#endif

    InterstitialAd _interstitialAd;

    // Interm ID
//#if UNITY_ANDROID
//    private string _adIntermId = "ca-app-pub-4411474865941018/7644641639";
//#elif UNITY_IPHONE
//  private string _adIntermId = "ca-app-pub-4411474865941018/7644641639";
//#else
//  private string _adIntermId = "unused";
//#endif

//    private RewardedAd _IntermId;



    GameManager scriptGameManager;


    public void Awake()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            LoadAdBanner();
            LoadInterstitialAd();
            //LoadRewardedAd();
        });
    }

    private void Start()
    {
        scriptGameManager = gameObject.GetComponent<GameManager>();
    }

    //Banner AD
    public void CreateBannerView()
    {
        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyAdBanner();
        }

        // Create a 320x50 banner at top of the screen
        _bannerView = new BannerView(_adBannerID, AdSize.Banner, AdPosition.Bottom);
    }

    public void LoadAdBanner()
    {
        // create an instance of a banner view first.
        if (_bannerView == null)
        {
            CreateBannerView();
        }

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        _bannerView.LoadAd(adRequest);
    }

    public void DestroyAdBanner()
    {
        if (_bannerView != null)
        {
            _bannerView.Destroy();
            _bannerView = null;
        }
    }

    //Interstitial AD
    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(_adInterstitialID, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                _interstitialAd = ad;

                RegisterFailedHandlers(_interstitialAd);

            });
    }

    public void ShowInterstitialAd(string page)
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            RegisterEventHandlers(_interstitialAd, page);
            _interstitialAd.Show();
        }
        else
        {
            SceneManager.LoadScene(page);
        }
    }

    public void RegisterFailedHandlers(InterstitialAd interstitialAd)
    { // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            // Request New One
            LoadInterstitialAd();
        };
    }

    public void RegisterEventHandlers(InterstitialAd interstitialAd, string page)
    {
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            SceneManager.LoadScene(page);

            // Request New One
            LoadInterstitialAd();
        };

    }

    //Interm AD
    public void ShowBetweenAd()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            RegisterReloadHandler(_interstitialAd);
            _interstitialAd.Show();
        }
    }

    public void RegisterReloadHandler(InterstitialAd interstitialAd)
    {
        // Raised when the ad closed full screen content.
        //interstitialAd.OnAdFullScreenContentClosed += () =>
        //{

        //    // Request New One
        //    LoadInterstitialAd();
        //};
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            LoadInterstitialAd();
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            LoadInterstitialAd();
        };

    }
    //public void LoadRewardedAd()
    //{
    //    // Clean up the old ad before loading a new one.
    //    if (_IntermId != null)
    //    {
    //        _IntermId.Destroy();
    //        _IntermId = null;
    //    }

    //    Debug.LogWarning("Loading the rewarded ad.");

    //    // create our request used to load the ad.
    //    var adRequest = new AdRequest();

    //    // send the request to load the ad.
    //    RewardedAd.Load(_adIntermId, adRequest,
    //        (RewardedAd ad, LoadAdError error) =>
    //        {
    //            // if error is not null, the load request failed.
    //            if (error != null || ad == null)
    //            {
    //                Debug.LogError("Rewarded ad failed to load an ad " +
    //                               "with error : " + error);
    //                return;
    //            }

    //            Debug.Log("Rewarded ad loaded with response : "
    //                      + ad.GetResponseInfo());

    //            _IntermId = ad;
    //        });
    //}

    //public void ShowRewardedAd()
    //{
    //    const string rewardMsg =
    //        "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

    //    if (_IntermId != null && _IntermId.CanShowAd())
    //    {
    //        RegisterReloadHandler(_IntermId);
    //        _IntermId.Show((Reward reward) =>
    //        {

    //            // TODO: Reward the user.
    //            Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
    //        });
    //    }
    //}

    //private void RegisterReloadHandler(RewardedAd ad)
    //{
    //    // Raised when the ad closed full screen content.
    //    ad.OnAdFullScreenContentClosed += () =>
    //    {
    //        Debug.Log("Rewarded Ad full screen content closed.");

    //        // Reload the ad so that we can show another as soon as possible.
    //        LoadRewardedAd();
    //    };
    //    // Raised when the ad failed to open full screen content.
    //    ad.OnAdFullScreenContentFailed += (AdError error) =>
    //    {
    //        Debug.LogError("Rewarded ad failed to open full screen content " +
    //                       "with error : " + error);

    //        // Reload the ad so that we can show another as soon as possible.
    //        LoadRewardedAd();
    //    };
    //}
}
