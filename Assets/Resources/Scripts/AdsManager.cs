using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdsManager : MonoBehaviour
{
    // private string appID = "ca-app-pub-9555394069851847~9682330316"; // test
    // private string bannerID = "ca-app-pub-3940256099942544/6300978111"; //test
    private string appID = "ca-app-pub-9555394069851847~9682330316";
    private string bannerID = "ca-app-pub-9555394069851847/8177676953";
    private BannerView bannerView;
    
    private string rewardAdID = "ca-app-pub-9555394069851847/1291629979";
    private RewardBasedVideoAd rewardAd;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Ads Initialized");
        MobileAds.Initialize(appID);
        rewardAd = RewardBasedVideoAd.Instance;

        RequestBanner();
        RequestRewardedAd();
    }

    private void RequestBanner()
    {
        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    public void RequestRewardedAd() {
        AdRequest request = new AdRequest.Builder().Build();

        rewardAd.LoadAd(request, rewardAdID);
    }

    public void ShowRewardedAd(){
        if (rewardAd.IsLoaded()) {
            rewardAd.Show();
        } else {
            Debug.Log("Rewarded Ad not Loaded");
        }
    }

    //Banner
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }


    //RewardedAd
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdFailedLoad event received with message: "
                            + args.Message);
    }

    public void HandleRewardedAdVideoRewarded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdVideoRewarded event received");

        GameManager.Instance.ContinueGame();
    }

    public void HandleRewardedAdVideoClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdVideoClosed event received");

        RequestRewardedAd();
    }
}
