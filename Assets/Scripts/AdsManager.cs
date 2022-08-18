using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    private string gameID = "4422230";
    public bool testMode = true;
    private string placement = "Rewarded_Android";
    
    public static AdsManager instance;

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, testMode);
    }
    
    private void Awake()
    {
        instance = this;
    }

    public void ShowAds()
    {
        Advertisement.Show(placement); //show ads when press button
    }

    public void OnUnityAdsReady(string placementId)
    {
       
    }

    public void OnUnityAdsDidError(string message)
    {
        
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        Debug.Log($"This {placementId} Finished!");

        if (showResult == ShowResult.Finished)
        {
            Debug.Log("Get Reward!!!!!!");
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("Skip****");
        }
        Time.timeScale = 1f;
    }
}
