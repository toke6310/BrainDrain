using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
    public void Pass(int levelIndex)
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (currentLevel >= PlayerPrefs.GetInt("levelIsUnlocked"))
        {
            PlayerPrefs.SetInt("levelIsUnlocked",currentLevel + 1);
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(levelIndex);

    }
    
}
