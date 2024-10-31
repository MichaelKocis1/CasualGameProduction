using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private BallsRemaining ballsRemaining;
    private VirusRemaining virusRemaining;

    GameObject[] ballsOnScreen;
    public int numBallsOnScreen;
    [SerializeField] GameOverManager gameOverManager;
    [SerializeField] WinManager winManager;
    

    void Start() {
        GameObject ballsTextObject = GameObject.FindWithTag("ballsText");
        GameObject virusTextObject = GameObject.FindWithTag("virusText");

        if (ballsTextObject != null) {
            ballsRemaining = ballsTextObject.GetComponent<BallsRemaining>();
        }

        if (virusTextObject != null) {
            virusRemaining = virusTextObject.GetComponent<VirusRemaining>();
        }
    }

    void Update()
    {
        // If all Virus pegs are removed, restart
        if (virusRemaining.getNumVirusRemaining() == 0) {
            
            winManager.SetWin();

            Destroy(gameObject);
        }

        // If no more balls are remaining...
        if (ballsRemaining.getNumBallsRemaining() == 0)
        {
            // check how many balls are left on screen. If none, then restart
            ballsOnScreen = GameObject.FindGameObjectsWithTag("Player");
            numBallsOnScreen = ballsOnScreen.Length;

            if (numBallsOnScreen == 0)
            {
               gameOverManager.SetGameOver();

               Destroy(gameObject);
            }
        }
    }

    public IEnumerator WaitTime() {
        // Starts timer, then restarts scene
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
