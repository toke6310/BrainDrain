using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;




public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] private TextMeshProUGUI totalScore;
    [SerializeField] private TextMeshProUGUI winScore;
    [SerializeField] private GameObject noScoreText;
    [SerializeField] private Button addTimeButton;
    [SerializeField] private GameObject adsTimePanel;
    [SerializeField] private TextMeshProUGUI textPanelAdd;
    [SerializeField] private int pointUseAddTime;
    
    public int bonusStar;
    public Complete star;
    
    public static ScoreManager instance;
    private int countPressAddTime;

    public int lastScore;
    
    int currScore = 0;

    private void Start()
    {
        Load();
        totalScore.text = $"Score: {currScore}";
        textPanelAdd.text = $"Use {pointUseAddTime} score";
    }

    private void Awake()
    {
        instance = this;
    }

    public void AddScore()
    {
        currScore += 50;
        totalScore.text = $"Score: {currScore}";
    }
    
    public void BonusScore()
    {
        bonusStar = star.totalStar;
        if (bonusStar == 1)
        {
            currScore += (currScore * 5/100);
        }
        else if (bonusStar == 2)
        {
            currScore += (currScore * 10/100);
        }
        else if (bonusStar == 3)
        {
            currScore += (currScore * 20/100);
        }
    }
    
    public void TotalScore()
    {
        BonusScore();
        winScore.text = $"SCORE: {currScore}(+Bonus)";
        Save();
        LastScore();
    }

    public void UseAddTime()
    {
        FindObjectOfType<AudioManager>().Play("click");
        if (currScore <= 0)
        {
            currScore = 0;
            noScoreText.SetActive(true);
            StartCoroutine(CountSecond());
        }
        
        else if (currScore > 0)
        {
            countPressAddTime++;
            if (countPressAddTime == 1)
            {
                 if (currScore >= pointUseAddTime)
                 {
                     currScore -= pointUseAddTime;
                     CountdownController.instance.AddTime();
                     StartCoroutine(CountSecond());
                 }
                                     
                 totalScore.text = $"Score: {currScore}";
                 textPanelAdd.text = $"Ads";
            }
            else if (countPressAddTime == 2)
            {
                adsTimePanel.SetActive(true);
            }
        }
    }

    IEnumerator CountSecond()
    {
        yield return new WaitForSeconds(2f);
        noScoreText.SetActive(false);
    }
    

    public void WatchAds()
    {
        Time.timeScale = 0f;
        FindObjectOfType<AudioManager>().Play("click");
        adsTimePanel.SetActive(false);
        AdsManager.instance.ShowAds();
        CountdownController.instance.AdsAddTime();
        addTimeButton.image.color = Color.gray; 
        addTimeButton.enabled = false;
        textPanelAdd.text = $"None";
    }

    public void CloseAdsMenu()
    {
        FindObjectOfType<AudioManager>().Play("click");
        adsTimePanel.SetActive(false);
        countPressAddTime--;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Score", currScore);
    }

    public void Load()
    {
        currScore = PlayerPrefs.GetInt("Score", currScore);
    }

    public void LastScore()
    {
        lastScore = currScore;
    }
    
    
    public void ReduceScore()
    {
        currScore -= 150;
        currScore -= (currScore * 50 / 100);
        if (currScore < 0)
        {
            currScore = 0;
        }
        totalScore.text = $"Score: {currScore}";
        Save();
    }
    
    public void ResetScoreForLevel1()
    {
        FindObjectOfType<AudioManager>().Play("click");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        currScore = 0;
        totalScore.text = $"Score: {currScore}";
        Save();
    }

}