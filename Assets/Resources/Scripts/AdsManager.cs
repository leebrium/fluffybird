using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdsManager : MonoBehaviour
{
    // private string bannerID = "ca-app-pub-3940256099942544/6300978111"; //test
    // private string rewardAdID = "ca-app-pub-3940256099942544/5224354917"; //test
    
    private string bannerID = "ca-app-pub-9555394069851847/6478269436";
    private string rewardAdID = "ca-app-pub-9555394069851847/6238596285";
    private BannerView bannerView;
    private RewardedAd rewardAd;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });

        RequestBanner();
        RequestRewardedAd();
    }

    private void RequestBanner()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
            bannerView = null;
        }

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    public void RequestRewardedAd() {
        if (rewardAd != null)
        {
            rewardAd.Destroy();
            rewardAd = null;
        }

        AdRequest request = new AdRequest();

        RewardedAd.Load(rewardAdID, request, 
        (RewardedAd ad, LoadAdError error) => {
            rewardAd = ad;
        });

        this.rewardAd.OnAdFullScreenContentClosed += () => {
            RequestRewardedAd();
        };

        this.rewardAd.OnAdFullScreenContentFailed += (AdError error) => {
            RequestRewardedAd();
        };
    }

    public void ShowRewardedAd(){
        if (rewardAd != null && rewardAd.CanShowAd()) {
            rewardAd.Show((Reward reward) => {
                GameManager.Instance.ContinueGame();
            });
        } else {
            RequestRewardedAd();
        }
    }
}
