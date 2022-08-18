using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownController : MonoBehaviour
{
    
    public TextMeshProUGUI countdownDisplay;
    [SerializeField] private int countdownTime;
    [SerializeField] private TextMeshProUGUI winTime;
    [SerializeField] private GameObject timeOut;
    [SerializeField] private Image star1;
    [SerializeField] private Image star2;
    [SerializeField] private Image star3;
    [SerializeField] private int timestar1;
    [SerializeField] private int timestar2;
    [SerializeField] private int timestar3;
    [SerializeField] private GameObject addTimeText;
    [SerializeField] private GameObject adsAddTimeText;
    [SerializeField] private GameObject hintText;
    [SerializeField] private GameObject adsHintText;
    [SerializeField] private GameObject adsHintPanel;
    [SerializeField] private Button hintButton;
    [SerializeField] private int timeUseHint;
    [SerializeField] private TextMeshProUGUI textPanelHint;

    private int countWinTime;
    private int maxTime;
    private int countPressHint;


    public static CountdownController instance;

        private void Start()
        { 
            maxTime = countdownTime; 
            StartCoroutine(CountdownToStart()); 
            StartCoroutine(CountTimeToWin());
            textPanelHint.text = $"Use {timeUseHint}s";
        }

    
        private void Awake()
        {
            instance = this;
        }
       

        IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            countdownDisplay.text += "s";

            yield return new WaitForSeconds(1f);

            countdownTime--;

            RemoveStar();
            
        }
        countdownDisplay.text = "Time Up";

        yield return new WaitForSeconds(1f);
        
        countdownDisplay.gameObject.SetActive(false);
        
        timeOut.SetActive(true);
        FindObjectOfType<AudioManager>().Play("fail");
        
    }

        IEnumerator CountTimeToWin()
        {
            while (countWinTime >= 0)
            {

                yield return new WaitForSeconds(1f);

                countWinTime++;
                
            }
        }
        
        public void TimeToWin()
        {
            winTime.text = $"Time:{countWinTime}s";
        }

        public void UseHint()
        {
            FindObjectOfType<AudioManager>().Play("click");
            countPressHint++;
            if (countPressHint == 1)
            {
                countdownTime -= timeUseHint;
                hintText.gameObject.SetActive(true);
                StartCoroutine(CountSecond());
                textPanelHint.text = $"Ads";
            }
            else if (countPressHint == 2)
            {
                adsHintPanel.SetActive(true);
            }

        }

        public void AddTime()
        {
            countdownTime += 15;
            addTimeText.SetActive(true);
            if (countdownTime > maxTime)
            {
                countdownTime = maxTime;
            }
            StartCoroutine(CountSecond2());
        }
        
        public void AdsAddTime()
        {
            countdownTime += 10;
            adsAddTimeText.SetActive(true);
            if (countdownTime > maxTime)
            {
                countdownTime = maxTime;
            }
            StartCoroutine(CountSecond2());
        }
        
        public void WatchAds()
        {
            Time.timeScale = 0f;
            FindObjectOfType<AudioManager>().Play("click");
            adsHintPanel.SetActive(false);
            AdsManager.instance.ShowAds();
            adsHintText.gameObject.SetActive(true);
            StartCoroutine(CountSecond());
            textPanelHint.text = $"None";
            hintButton.image.color = Color.gray;
            hintButton.enabled = false;
        }

        public void CloseAdsMenu()
        {
            FindObjectOfType<AudioManager>().Play("click");
            adsHintPanel.SetActive(false);
            countPressHint--;
        }
        

        IEnumerator CountSecond()
        {
            yield return new WaitForSeconds(5f);
            hintText.gameObject.SetActive(false);
            adsHintText.SetActive(false);
        }
        
        IEnumerator CountSecond2()
        {
            yield return new WaitForSeconds(3f);
            addTimeText.SetActive(false);
            adsAddTimeText.SetActive(false);
        }
        
    

        public void RemoveStar()
        {
            if (countdownTime < timestar3)
            {
                star3.color = Color.gray;
            }
            if (countdownTime < timestar2)
            {
                star2.color = Color.gray;
            }
            if (countdownTime < timestar1)
            {
                star1.color = Color.gray;
            }
            
        }


}
