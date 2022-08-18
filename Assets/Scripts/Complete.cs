using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Complete : MonoBehaviour
{
    [SerializeField] private Image star1;
    [SerializeField] private Image star2;
    [SerializeField] private Image star3;
    [SerializeField] private Image s1;
    [SerializeField] private Image s2;
    [SerializeField] private Image s3;
    
    public static Complete instance;
    
    public int star = 0;
    public int totalStar;

    void Start()
    {
        Time.timeScale = 0f;
        CountStar();
        ScoreManager.instance.TotalScore();
        CountdownController.instance.TimeToWin();
        CompleteLevel();
    }
    
    private void Awake()
    {
        instance = this;
    }

    public void NextLevel(int levelIndex)
    {
        FindObjectOfType<AudioManager>().Play("click");
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelIndex);
    }

    public void CompleteLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        
        PlayerPrefs.SetInt("clear", currentLevel );

        if (currentLevel == 1)
        {
            PlayerPrefs.DeleteKey("Score");
        }

        if (currentLevel >= PlayerPrefs.GetInt("levelIsUnlocked"))
        { 
            PlayerPrefs.SetInt("levelIsUnlocked",currentLevel + 1);
        }
        Debug.Log($"unlock Next Level! {currentLevel}");
    }


    public void Finished()
    {
        FindObjectOfType<AudioManager>().Play("click");
        Time.timeScale = 1;
        SceneManager.LoadScene("Leaderboard");
    }

    public void MainMenu()
    {
        FindObjectOfType<AudioManager>().Play("click");
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    
    public void Restart()
    {
        FindObjectOfType<AudioManager>().Play("click");
        Time.timeScale = 1f;
        ScoreManager.instance.ReduceScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void GameOver()
    {
        FindObjectOfType<AudioManager>().Play("click");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CountStar()
    {
        s1.GetComponent<Image>().color = star1.GetComponent<Image>().color;
        s2.GetComponent<Image>().color = star2.GetComponent<Image>().color;
        s3.GetComponent<Image>().color = star3.GetComponent<Image>().color;
        CheckStar();
    }

    public void CheckStar()
    {
        if (s1.color != Color.gray)
        {
            star++;
        }
        if (s2.color != Color.gray)
        {
            star++;
        }
        if (s3.color != Color.gray)
        {
            star++;
        }
        AllStar();
        
    }

    public void AllStar()
    {
        totalStar = star;
    }

}
