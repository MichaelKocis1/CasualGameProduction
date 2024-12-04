using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    public void PlayLevelOne()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void PlayLevelTwo()
    {
        SceneManager.LoadScene("Pancreas");
    }
}