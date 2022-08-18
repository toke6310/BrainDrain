using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int levelIsUnloked;
    private int clearLevel;

    public Button[] buttons;
    public Image[] lockImages;
    public Image[] clearImages;
    
    // Start is called before the first frame update
    void Start()
    {

        levelIsUnloked = PlayerPrefs.GetInt("levelIsUnlocked", 1);
        clearLevel = PlayerPrefs.GetInt("clear", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        
        for (int i = 0; i < levelIsUnloked; i++)
        {
            buttons[i].interactable = true;
            lockImages[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < clearLevel; i++)
        {
            if (i == 0)
            {
                buttons[i].interactable = true;
            }
            else
            {
                clearImages[i].gameObject.SetActive(true);
                buttons[i].interactable = false;
            }
            
        }
    }
    

    public void LoadLevel(int levelIndex)
    {
        FindObjectOfType<AudioManager>().Play("click");
        SceneManager.LoadScene(levelIndex);
    }

    public void ResetAllLevel()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

}
