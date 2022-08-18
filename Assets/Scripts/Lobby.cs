using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{

    [SerializeField] private GameObject selectLevel;
    
    // Start is called before the first frame update
    

    public void ChoosePlay()
    {
        FindObjectOfType<AudioManager>().Play("click");
        selectLevel.SetActive(true);
    }

    public void ChooseHowtoPlay()
    {
        FindObjectOfType<AudioManager>().Play("click");
        SceneManager.LoadScene("HowToPlay");
    }

    public void ChooseBack()
    {
        FindObjectOfType<AudioManager>().Play("click");
        SceneManager.LoadScene("MainMenu");
    }
    
    public void ChooseClose()
    {
        FindObjectOfType<AudioManager>().Play("click");
        selectLevel.SetActive(false);
    }
    
    
}
