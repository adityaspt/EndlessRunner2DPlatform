using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
//using SNLMain;

public class UnityAdsManager : MonoBehaviour, IUnityAdsListener
{
    public static UnityAdsManager UnityAds;
    [SerializeField]
    string bannerAdID = "BannerAd";//"SnlMainBanner";

    [SerializeField]
    string GPgameId = "3707893"; //"3622205";
    [SerializeField]
    bool testMode = true;
    [SerializeField]
    private string RewardPlacementId = "rewardedVideo"; //"SnlMainReward";
    [SerializeField]
    public int frequencyOfInterstitial;
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("InterstitialOccured"))
        {
            PlayerPrefs.SetInt("InterstitialOccured", 0);
        }
        //else
        //{
        //    PlayerPrefs.SetInt("InterstitialOccured", PlayerPrefs.GetInt("InterstitialOccured")+1);
        //}

        if (UnityAds != null)
        {
            Destroy(this.gameObject);

        }
        UnityAds = this;
        DontDestroyOnLoad(this.gameObject);
    }



    #region Interstitial
    public void ShowInter()
    {
        //i = PlayerPrefs.GetInt("NoAd");
        //if (i == 0)
        //{
        PlayerPrefs.SetInt("InterstitialOccured", PlayerPrefs.GetInt("InterstitialOccured") + 1);
        print(PlayerPrefs.GetInt("InterstitialOccured") + " PlayerPrefs.GetIntInterstitialOccured");
        if (PlayerPrefs.GetInt("InterstitialOccured") == frequencyOfInterstitial)
        {
            PlayerPrefs.SetInt("InterstitialOccured", 0);
            Advertisement.Show();

        }


        // }
    }
    #endregion



    void Start()
    {
        //Advertisement.AddListener(this);
        // Initialize the Ads service:
        Advertisement.Initialize(GPgameId, testMode);
        ShowBannerAd();
        //  StartCoroutine(ShowBannerWhenReady());
    }

    #region Banner
    public void ShowBannerAd()
    {
        StartCoroutine(ShowBannerWhenReady());
    }
    IEnumerator ShowBannerWhenReady()
    {
        //i = PlayerPrefs.GetInt("NoAd");
        //if (i == 0)
        //{
        while (!Advertisement.IsReady(bannerAdID))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(bannerAdID);

        // }
    }
    #endregion

    #region rewardAd
    public void ShowRewardAd()
    {
        Advertisement.AddListener(this);
        Advertisement.Show(RewardPlacementId);
    }
    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.

            Debug.Log("Reward give");

            //if (PlayerPrefs.GetInt("Lifes") > 0)
            //{
            //    GameManager.gameManagerInstance.deathscreen.transform.GetChild(4).gameObject.SetActive(false);
            //}
            MoveSquare.mainInstance.RespawnPlayer();
            GameManager.gameManagerInstance.WatchAdScreen.SetActive(false);
            Advertisement.RemoveListener(this);
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("Reward dont give");
            // Do not reward the user for skipping the ad.
            Advertisement.RemoveListener(this);
        }
        else if (showResult == ShowResult.Failed)
        {
            GameManager.gameManagerInstance.WatchAdScreen.transform.GetChild(3).gameObject.SetActive(true);
            Debug.LogWarning("The ad did not finish due to an error.");
            Advertisement.RemoveListener(this);
        }
    }

    public void OnUnityAdsReady(string placementId)
    {

        // If the ready Placement is rewarded, show the ad:
        if (placementId == RewardPlacementId)
        {
            Advertisement.Show(RewardPlacementId);
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    #endregion

    // Update is called once per frame
    void Update()
    {

    }
}
