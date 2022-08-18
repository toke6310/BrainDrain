using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject MenuBar;
    [SerializeField] private GameObject HintText;


    public void ChooseMenu()
    {
        MenuBar.SetActive(true);
        Time.timeScale = 0f;
        FindObjectOfType<AudioManager>().Play("click");
    }

    public void ChooseContinue()
    {
        MenuBar.SetActive(false);
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().Play("click");
    }

    public void ChooseRestart()
    {
        FindObjectOfType<AudioManager>().Play("click");
        Time.timeScale = 1f;
        ScoreManager.instance.ReduceScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void ChooseExit()
    {
        FindObjectOfType<AudioManager>().Play("click");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    
    /*public void ChooseQuit()
    {
        Application.Quit();
    }*/
    
}
