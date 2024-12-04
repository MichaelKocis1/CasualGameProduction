using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void PlayLevelOne ()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

     public void PlayLevelTwo ()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Pancreas");
    }

    public void QuitGame ()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");    
    }


}
