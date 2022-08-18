using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class MatchManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] private TextMeshProUGUI totalMatch;
    [SerializeField] private GameObject over;

    public static MatchManager instance;

     int maxMatch = 5; 
     int countMatch = 0;

    private void Start()
    {
        totalMatch.text = $"Match: 0/5";
        over.SetActive(false);
    }
    
    private void Awake()
    {
        instance = this;
    }

    public void CountMatch()
    {
        countMatch += 1;
        if (countMatch < maxMatch+1)
        {
            totalMatch.text = $"Match: {countMatch}/5";
        }
        else if (countMatch >= maxMatch)
        {
            over.SetActive(true);
            totalMatch.text = $"Match: {maxMatch}/5";
        }
    }
    
}
