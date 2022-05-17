using UnityEngine;
using UnityEngine.Advertisements;

public class ADManager : MonoBehaviour, IUnityAdsListener
{
    string playStoreID = "3592911";
    //I don't need the AppStoreID :)

    string interstitialAd = "video";
    string rewardedVideoAd = "rewardedVideo";

    public bool isTargetPlayStore;
    [Tooltip("Uncheck Only When Deploying / Building the Game")]
    public bool isTestAd;

    // Start is called before the first frame update
    void Start ()
    {
        Advertisement.AddListener (this);
        InitialiseAdvertisement ();
    }

    void InitialiseAdvertisement ()
    {
        if (isTargetPlayStore)
        {
            Advertisement.Initialize (playStoreID, isTestAd);
            return;
        }
    }

    public void PlayInterstitialAd ()
    {
        if (!Advertisement.IsReady (interstitialAd))
            return;

        Advertisement.Show (interstitialAd);
    }

    public void PlayRewardedVideoAd ()
    {
        if (!Advertisement.IsReady(rewardedVideoAd))
            return;

        Advertisement.Show (rewardedVideoAd);
    }

    public void OnUnityAdsReady(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        //throw new System.NotImplementedException();

        switch (showResult)
        {
            case ShowResult.Failed:
                break;

            case ShowResult.Skipped:
                break;

            case ShowResult.Finished:
                //------------------------------------//
                if (placementId == rewardedVideoAd)
                    print ("COINS GAINED!!!");

                else if (placementId == interstitialAd)
                    print ("FINISHED INTERSTITIAL AD");
                //----------------------------------//
                break;
        }
    }
}