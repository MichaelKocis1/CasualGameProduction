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
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame ()
    {
        Application.Quit();
    }
}
