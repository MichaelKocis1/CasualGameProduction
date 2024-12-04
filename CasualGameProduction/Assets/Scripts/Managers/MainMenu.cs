using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        //Keeps main menu in portrait mode 
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.orientation = ScreenOrientation.AutoRotation;
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
        Application.Quit();
    }
}
