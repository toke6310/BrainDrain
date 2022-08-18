using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;
using TMPro;
using UnityEngine.SceneManagement;

public class LeaderBoardController : MonoBehaviour
{
   [SerializeField] private TMP_InputField userName;
   [SerializeField] private TextMeshProUGUI playerName;
   [SerializeField] private TextMeshProUGUI playerScore;

   [SerializeField] private GameObject panelName;
   public int ID;
   private int maxScore = 10;
   public TextMeshProUGUI[] entries;
   public TextMeshProUGUI[] entries2;
   public TextMeshProUGUI[] entries3;
   public int ls;
   
   
   public static LeaderBoardController instance;
   
   private void Start()
   {

      LootLockerSDKManager.StartSession("Player", (respones) =>
      {
         if (respones.success)
         {
            Debug.Log("Success");
         }
         else
         {
            Debug.Log("Failed");
         }
      });

   }

   private void Awake()
   {
      instance = this;
      
   }

   public void SubmitScore()
   {
      ls = ScoreManager.instance.lastScore.GetHashCode();

      LootLockerSDKManager.SubmitScore(userName.text, ls, ID, (respones) =>
      {
         if (respones.success)
         {
            Debug.Log("Success");
         }
         else
         {
            Debug.Log("Failed");
         }
      });
      playerName.text = userName.text;
      playerScore.text = ls.ToString();
      panelName.SetActive(false);
      ShowScore();
   }
   
   public void ShowScore()
   {
      LootLockerSDKManager.GetScoreList(ID, maxScore, (respones) =>
      {
         if (respones.success)
         {
            LootLockerLeaderboardMember[] scores = respones.items;
            
            
            for (int i = 0; i < scores.Length; i++)
            {
               entries[i].text = $"{scores[i].rank}";
               entries2[i].text = $"{scores[i].member_id}";
               entries3[i].text = $"{scores[i].score} ";
            }
            

            if (scores.Length < maxScore)
            {
               for (int i = scores.Length; i < maxScore; i++)
               {
                  entries[i].text = $"{(i + 1).ToString()}";
               }
            }
         }
         else
         {
            Debug.Log("Failed");
         }
      });
   }
   
   public void ChooseBack()
   {
      FindObjectOfType<AudioManager>().Play("click");
      SceneManager.LoadScene("MainMenu");
      PlayerPrefs.DeleteKey("Score");
   }
   
   public void Refresh()
   {
      FindObjectOfType<AudioManager>().Play("click");
      ShowScore();
   }
}
